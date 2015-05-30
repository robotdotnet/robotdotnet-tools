using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using Renci.SshNet.Common;

namespace RobotDotNetBuildTasks
{
    public static class InstallDeployManager
    {
        public static bool DeployAndExecuteRobotCode(int teamNumber, string username, string password, DirectoryInfo outputDir)
        {
            return true;
            /*
            var connectionInfo = GetWorkingConnectionInfo(teamNumber, username, password);
            if(connectionInfo == null) return false;
            using (ScpClient scp = new ScpClient(connectionInfo))
            {
                scp.Connect();
                scp.Upload(outputDir, "~/mono");
            }
            using (SshClient ssh = new SshClient(connectionInfo))
            {
                ssh.Connect();
                ssh.RunCommand("mono ~/mono/*.exe");
            }
            return true;
             * */
        }

        public static Dictionary<string, string> RunCommands(string[] commands, ConnectionInfo connectionInfo)
        {
            Dictionary<string, string> retCommands = new Dictionary<string, string>();
            using (SshClient ssh = new SshClient(connectionInfo))
            {
                try
                {
                    ssh.Connect();
                }
                catch (SshOperationTimeoutException ex)
                {

                }
                foreach (string s in commands)
                {
                    var x = ssh.RunCommand(s);
                    retCommands.Add(s, x.Result);
                }
            }
            return retCommands;
        }

        public static bool DeployFiles(string[] files, string deployLocation, ConnectionInfo connectionInfo)
        {
            if (connectionInfo == null) return false;
            using (ScpClient scp = new ScpClient(connectionInfo))
            {
                try
                {
                    scp.Connect();
                }
                catch (SshOperationTimeoutException ex)
                {
                    
                }
                foreach (string s in files)
                {
                    if (File.Exists(s))
                    {
                        FileInfo fileInfo = new FileInfo(s);
                        scp.Upload(fileInfo, deployLocation);
                    }
                }
            }
            return true;
        }



        public static bool InstallRuntime(int teamNumber, string username, string password, FileInfo ipkZipFile, FileInfo halRoboRio, FileInfo halImpl)
        {
            ConnectionType type;
            string ip;
            var connectionInfo = RoboRIOConnection.CheckConnection(teamNumber.ToString(), out type, out ip);
            if (connectionInfo == null) return false;
            using (ScpClient scp = new ScpClient(connectionInfo))
            using (SshClient ssh = new SshClient(connectionInfo))
            {
                scp.Connect();
                ssh.Connect();
                scp.Upload(ipkZipFile, "~/");
                ssh.RunCommand("unzip ~/IPKs.zip -d ~");
                ssh.RunCommand("opkg install ~/*.ipk");
                ssh.RunCommand("mkdir ~/HAL");
                ssh.RunCommand("mkdir ~/mono");
                scp.Upload(halRoboRio, "~/mono");
                scp.Upload(halImpl, "~/mono");
            }
            return true;
        }

        
    }
}
