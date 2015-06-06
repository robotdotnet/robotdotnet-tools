using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplatePatcher
{
    class VSIXManager
    {
        private string[] vsixFile;

        private int index = -1;

        public string FilePath;

        public VSIXManager(string filePath)
        {
            this.FilePath = filePath;
            vsixFile = File.ReadAllLines(filePath);
            for (int i = 0; i < vsixFile.Length; i++)
            {
                if (vsixFile[i].Contains("<Identity Id=\"FRC_Robot_Templates\""))
                {
                    index = i;
                    break;
                }
            }
        }

        public void ReplaceVersion(string ver)
        {
            if (index != -1)
            {
                vsixFile[index] =
                    string.Format(
                        "    <Identity Id=\"FRC_Robot_Templates\" Version=\"{0}\" Language=\"en-US\" Publisher=\"RobotDotNet\" />",
                        ver);
            }
        }


        public void WriteFile()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
            File.WriteAllLines(FilePath, vsixFile);
        }
    }
}
