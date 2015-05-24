using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Xml;

namespace DeployRobot
{
    class SSHController
    {
        string m_hostname = "";
        string winBins = "win32";

        string m_cfgFilename;
        string m_username;
        string m_password;
        bool m_allowMitm;

        bool m_isWindows;
        

        public string Username
        {
            get { return m_username; }
        }

        public string Password
        {
            get { return m_password; }
        }

        public string Hostname
        {
            get { return m_hostname; }
        }



        public SSHController(string hostname, string username, string password, bool allowMitm = false)
        {
            m_hostname = hostname;
            m_username = username;
            m_password = password;
            m_allowMitm = allowMitm;
            m_isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
        //InitConfig not needed.

        //DoConfig Not needed

        public void SSH(string[] args, bool getOutput = false)
        {
            string sshArgs = m_username + "@" + m_hostname;
            foreach (string s in args)
            {
                sshArgs = sshArgs + s;
            }

            string cmd;
            if (m_isWindows)
            {
                cmd = winBins + "plink.exe";

                sshArgs = "-pw" + m_password + sshArgs;

                try
                {
                    if (getOutput)
                    {
                        ProcessStartInfo start = new ProcessStartInfo(cmd);
                        start.Arguments = sshArgs;
                        var proc = Process.Start(start);
                    }
                }
                catch (Exception e)
                {

                }
            }
            else
            {
                //Write Linux version
            }
        }


        public void SFTP(string src, string dst, bool mkdir = true)
        {

        }
    }
}
