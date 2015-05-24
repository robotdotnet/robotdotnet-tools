using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;

namespace RobotDotNetBuildTasks
{
    static class InstallDeployManager
    {
        public static bool DeployAndExecuteRobotCode(int teamNumber, string username, string password, DirectoryInfo outputDir)
        {
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
        }

        public static bool InstallRuntime(int teamNumber, string username, string password, FileInfo ipkZipFile, FileInfo halRoboRio, FileInfo halImpl)
        {
            var connectionInfo = GetWorkingConnectionInfo(teamNumber, username, password);
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

        private static ConnectionInfo GetWorkingConnectionInfo(int teamNumber, string username, string password)
        {
            var authMethod = new PasswordAuthenticationMethod(username, password);
            var zeroConfConnectionInfo = new ConnectionInfo("roborio-" + teamNumber + ".local", username, authMethod);
            using (SshClient zeroConfClient = new SshClient(zeroConfConnectionInfo))
            {
                zeroConfClient.Connect();
                if (!zeroConfClient.IsConnected)
                {
                    var staticIP = String.Format("10.{0:00}.{1:00}.02", teamNumber / 100, teamNumber % 100);
                    var staticIPConnectionInfo = new ConnectionInfo(staticIP, username, authMethod);
                    using (SshClient staticIPClient = new SshClient(staticIPConnectionInfo))
                    {
                        staticIPClient.Connect();
                        if (!staticIPClient.IsConnected) return null;
                        return staticIPConnectionInfo;
                    }
                }
                return zeroConfConnectionInfo;
            }
        } 
    }
}
