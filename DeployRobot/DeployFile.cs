using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DeployRobot
{
    public class DeployFile
    {
        private string m_fileName;
        private bool m_deploy;
        private bool m_required;

        private bool m_robotFile;

        public string FileNamePath
        {
            get { return m_fileName; }
        }

        public string FileName
        {
            get 
            {
                return Path.GetFileName(m_fileName);
            }
        }

        public bool Deploy
        {
            get { return m_deploy; }
            set { m_deploy = value; }
        }

        public bool Required
        {
            get { return m_required; }
        }

        public bool RobotFile
        {
            get { return m_robotFile; }
        }

        public DeployFile(string fileName, bool required, bool robotFile)
        {
            m_fileName = fileName;
            m_deploy = true;
            m_required = required;
            m_robotFile = robotFile;
        }
    }
}
