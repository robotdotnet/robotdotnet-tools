using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace hal_generator
{
    class CppFile
    {
        private string m_fileName;

        private List<int> m_externLocations = new List<int>();

        private string[] m_fileRead;

        public CppFile(string fileName)
        {
            m_fileName = fileName;

            if (File.Exists(m_fileName))
                m_fileRead = File.ReadAllLines(m_fileName);
            int count = 0;
            for (int i = 0; i < m_fileRead.Length; i++)
            {
                if (m_fileRead[i].Contains("extern \"C\""))
                {
                    m_externLocations.Add(i);
                    //break;
                }
            }
        }

        public List<Function> func;

        public bool CreateFunctions()
        {
            func = new List<Function>();

            for (int i = 0; i < m_fileRead.Length; i++)
            {
                if (m_externLocations.Contains(i))
                {
                    i++;
                    for (; i < m_fileRead.Length; i++)
                    {
                        if (m_fileRead[i].Contains("{"))
                            continue;
                        if (m_fileRead[i].Contains("}"))
                            break;
                        if (m_fileRead[i].Contains("extern const"))
                            continue;
                        if (m_fileRead[i].Contains("typedef void (*InterruptHandlerFunction)"))
                            continue;
                        if (m_fileRead[i].Trim() == "")
                            continue;
                        if (m_fileRead[i].Contains("/") && !m_fileRead[i].Contains(";"))
                            continue;
                        if (!m_fileRead[i].Contains(";"))
                        {
                            string s = "";
                            i--;
                            while (!s.Contains(";"))
                            {
                                i++;
                                s = s +  m_fileRead[i];
                                i++;
                                s = s + m_fileRead[i];
                                //i++;
                            }
                            func.Add(new Function(s));
                            continue;
                        }
                        func.Add(new Function(m_fileRead[i]));
                    }
                }
            }
            return true;
        }
    }
}
