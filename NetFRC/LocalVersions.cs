using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using RoboRIO_Tool;

namespace NetFRC
{
    public class LocalVersions
    {
        private static Dictionary<string, string> versions = new Dictionary<string, string>();


        public static void StartRIO(Action callback, ConnectionManager connectionManager)
        {
            new Thread(() =>
            {
                if (connectionManager.Connected)
                {
                    string[] sent = { "cat /home/lvuser/mono/version", "mono --version" };
                    var rioVersionDict = RoboRIO_Tool.RoboRIOConnection.RunCommands(sent, connectionManager.ConnectionInfo);
                    var split = rioVersionDict[sent[0]].Split(':');
                    if (split[0] == "HAL")
                        versions["HAL"] = split[1].Trim().Replace(@"\n", "");

                    split = rioVersionDict[sent[1]].Split(' ');
                    if (split[0] == "Mono")
                    {
                        versions["MONO"] = split[4];
                    }
                }


                callback();
            }).Start();
        }

        private static bool FoundHAL()
        {
            return versions.ContainsKey("HAL");
        }

        private static bool FoundDeploy()
        {
            return versions.ContainsKey("DEPLOY");
        }

        private static bool FoundTemplate()
        {
            return versions.ContainsKey("TEMPLATE");
        }

        private static bool FoundMono()
        {
            return versions.ContainsKey("MONO");
        }

        public static string GetHAL()
        {
            if (FoundHAL())
            {
                return versions["HAL"];
            }
            return null;
        }

        public static string GetDeploy()
        {
            if (FoundDeploy())
            {
                return versions["DEPLOY"];
            }
            return null;
        }

        public static string GetTemplate()
        {
            if (FoundTemplate())
            {
                return versions["TEMPLATE"];
            }
            return null;
        }

        public static string GetMono()
        {
            if (FoundMono())
            {
                return versions["MONO"];
            }
            return null;
        }
    }
}
