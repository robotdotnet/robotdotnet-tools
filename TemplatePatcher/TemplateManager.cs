using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TemplatePatcher
{
    class TemplateManager
    {
        private string[] templateFile;
        private int wpiIndex = -1;
        private int ntIndex = -1;
        private bool found = false;

        public string FilePath;

        public TemplateManager(string filePath)
        {
            this.FilePath = filePath;
            templateFile = File.ReadAllLines(filePath);
            for (int i = 0; i < templateFile.Length; i++)
            {
                if (templateFile[i].Contains("package id=\"WPILib\""))
                {
                    wpiIndex = i;
                }
                if (templateFile[i].Contains("<package id=\"NetworkTablesDotNet\""))
                {
                    ntIndex = i;
                }
            }
            if (wpiIndex != -1 && ntIndex != -1)
            {
                found = true;
            }
        }


        public void UpdateWPI(string ver)
        {
            if (found)
            {
                templateFile[wpiIndex] = string.Format("       <package id=\"WPILib\" version=\"{0}\"/>", ver);
            }
        }

        public void UpdateNT(string ver)
        {
            if (found)
            {
                templateFile[ntIndex] = string.Format("       <package id=\"NetworkTablesDotNet\" version=\"{0}\"/>", ver);
            }
        }

        public void WriteFile()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
            File.WriteAllLines(FilePath, templateFile);
        }


    }
}
