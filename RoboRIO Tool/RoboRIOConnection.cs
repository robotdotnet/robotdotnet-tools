using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;

namespace RoboRIO_Tool
{
    public enum ConnectionType
    {
        USB,
        MDNS,
        IP,
        None
    }

    public static class RoboRIOConnection
    {
        public static readonly string RoboRioMdnsFormatString = "roborio-{0}.local";
        public static readonly string RoboRioUSBIp = "172.22.11.2";
        public static readonly string RoboRioIpFormatString = "10.{0}.{1}.2";

        public static ConnectionInfo CheckConnection(string teamNumberS, out ConnectionType type, out string conIP, bool admin)
        {
            return CheckConnection(teamNumberS, out type, out conIP, admin, TimeSpan.FromSeconds(2));
        }

        public static ConnectionInfo CheckConnection(string teamNumberS, out ConnectionType type, out string conIP, bool admin, TimeSpan timeout)
        {
            int teamNumber = 0;
            int.TryParse(teamNumberS, out teamNumber);


            if (teamNumber < 0)
            {
                teamNumber = 0;
            }
            string roboRioMDNS = string.Format(RoboRioMdnsFormatString, teamNumber);
            string roboRIOIP = string.Format(RoboRioIpFormatString, teamNumber / 100, teamNumber % 100);
            ConnectionInfo connection = null;
            try
            {
                connection = GetWorkingConnectionInfo(roboRioMDNS, admin, timeout);
                type = ConnectionType.MDNS;
                conIP = roboRioMDNS;
                return connection;
            }
            catch (SocketException ex)
            {

            }
            catch (SshOperationTimeoutException ex)
            {

            }
            try
            {
                connection = GetWorkingConnectionInfo(RoboRioUSBIp, admin, timeout);
                type = ConnectionType.USB;
                conIP = RoboRioUSBIp;
                return connection;
            }
            catch (SocketException ex)
            {

            }
            catch (SshOperationTimeoutException ex)
            {

            }
            try
            {
                connection = GetWorkingConnectionInfo(roboRIOIP, admin, timeout);
                type = ConnectionType.IP;
                conIP = roboRIOIP;
                return connection;
            }
            catch (SocketException ex)
            {

            }
            catch (SshOperationTimeoutException ex)
            {

            }

            type = ConnectionType.None;
            conIP = "";
            return connection;
        }

        private static ConnectionInfo GetWorkingConnectionInfo(string ip, bool admin, TimeSpan timeout)
        {
            string user = "lvuser";
            if (admin)
                user = "admin";

            KeyboardInteractiveAuthenticationMethod authMethod = new KeyboardInteractiveAuthenticationMethod(user);
            PasswordAuthenticationMethod pauth = new PasswordAuthenticationMethod(user, "");

            authMethod.AuthenticationPrompt += (sender, e) =>
            {
                foreach (AuthenticationPrompt p in e.Prompts.Where(p => p.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1))
                {
                    p.Response = "";
                }
            };
            var zeroConfConnectionInfo = new ConnectionInfo(ip, user, pauth, authMethod);
            zeroConfConnectionInfo.Timeout = timeout;
            using (SshClient zeroConfClient = new SshClient(zeroConfConnectionInfo))
            {

                zeroConfClient.Connect();
                return zeroConfConnectionInfo;
            }
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

        public static bool DeployFiles(IEnumerable files, string deployLocation, ConnectionInfo connectionInfo)
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
                foreach (FileInfo fileInfo in from string s in files where File.Exists(s) select new FileInfo(s))
                {
                    scp.Upload(fileInfo, deployLocation);
                }
            }
            return true;
        }
    }
}
