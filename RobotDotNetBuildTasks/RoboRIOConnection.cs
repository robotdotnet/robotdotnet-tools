using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;
using System.IO;
using Renci.SshNet.Common;

namespace RobotDotNetBuildTasks
{
    public enum ConnectionType
    {
        USB,
        MDNS,
        IP,
        None
    }
    public class RoboRIOConnection
    {
        public static readonly string ROBO_RIO_MDNS_FORMAT_STRING = "roborio-{0}.local";
        public static readonly string ROBO_RIO_USB_IP = "172.22.11.2";
        public static readonly string ROBO_RIO_IP_FORMAT_STRING = "10.{0}.{1}.2";

        public static ConnectionInfo CheckConnection(string teamNumberS, out ConnectionType type, out string conIP, bool admin)
        {
            int teamNumber = 0;
            int.TryParse(teamNumberS, out teamNumber);


            if (teamNumber < 0)
            {
                teamNumber = 0;
            }
            string roboRioMDNS = string.Format(ROBO_RIO_MDNS_FORMAT_STRING, teamNumber);
            string roboRIOIP = string.Format(ROBO_RIO_IP_FORMAT_STRING, teamNumber/100, teamNumber%100);
            ConnectionInfo connection = null;
            try
            {
                connection = GetWorkingConnectionInfo(roboRioMDNS, admin);
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
                connection = GetWorkingConnectionInfo(ROBO_RIO_USB_IP, admin);
                type = ConnectionType.USB;
                conIP = ROBO_RIO_USB_IP;
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
                connection = GetWorkingConnectionInfo(roboRIOIP, admin);
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


        private static ConnectionInfo GetWorkingConnectionInfo(string ip, bool admin)
        {
            string user = "lvuser";
            if (admin)
                user = "admin";

            KeyboardInteractiveAuthenticationMethod authMethod = new KeyboardInteractiveAuthenticationMethod(user);
            PasswordAuthenticationMethod pauth = new PasswordAuthenticationMethod(user, "");

            authMethod.AuthenticationPrompt += HandleEvent;
            //var authMethod = new PasswordAuthenticationMethod(username, password);
            var zeroConfConnectionInfo = new ConnectionInfo(ip, user, pauth, authMethod);
            zeroConfConnectionInfo.Timeout = TimeSpan.FromSeconds(2);
            using (SshClient zeroConfClient = new SshClient(zeroConfConnectionInfo))
            {
                
                zeroConfClient.Connect();
                return zeroConfConnectionInfo;
            }
        }

        static void HandleEvent(object sender, AuthenticationPromptEventArgs e)
        {
            foreach (AuthenticationPrompt p in e.Prompts)
            {
                if (p.Request.IndexOf("Password:", StringComparison.InvariantCultureIgnoreCase) != -1)
                {
                    p.Response = "";
                }
            }
        }
    }
}
