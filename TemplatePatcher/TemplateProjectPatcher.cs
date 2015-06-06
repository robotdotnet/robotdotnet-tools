using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatePatcher
{
    class TemplateProjectPatcher
    {
        private string[] file;

        private int WPIIndex = -1;
        private int NTIndex = -1;

        private bool found = false;

        public string FilePath = "";

        public TemplateProjectPatcher(string fileName)
        {
            FilePath = fileName;
            file = File.ReadAllLines(fileName);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains("<Content Include=\"packages\\WPILib"))
                {
                    WPIIndex = i;
                }
                if (file[i].Contains("<Content Include=\"packages\\NetworkTablesDotNet"))
                {
                    NTIndex = i;
                }
            }
            if (WPIIndex != -1 && NTIndex != -1)
            {
                found = true;
            }
        }

        public void UpdateWPI(string ver)
        {
            if (found)
            {
                file[WPIIndex] = string.Format("    <Content Include=\"packages\\WPILib.{0}.nupkg\">", ver);
            }
        }

        public void UpdateNT(string ver)
        {
            if (found)
            {
                file[NTIndex] = string.Format("    <Content Include=\"packages\\NetworkTablesDotNet.{0}.nupkg\">", ver);
            }
        }

        public void WriteFile()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
            File.WriteAllLines(FilePath, file);
        }
    }
}
