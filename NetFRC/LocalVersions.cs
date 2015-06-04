using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using RobotDotNetBuildTasks;

namespace NetFRC
{
    public class LocalVersions
    {
        private static Dictionary<string, string> versions = new Dictionary<string, string>();

        public static Dictionary<string,string> Versions
        {
            get { return versions; }
        }


        public static void StartRIO(Action callback, ConnectionManager connectionManager)
        {
            new Thread(() =>
            {
                if (connectionManager.Connected)
                {
                    string[] sent = { "cat /home/lvuser/mono/version", "mono --version" };
                    var rioVersionDict = InstallDeployManager.RunCommands(sent, connectionManager.Connection);
                    var split = rioVersionDict[sent[0]].Split(':');
                    if (split[0] == "HAL")
                        versions["HAL"] = rioVersionDict[sent[0]].Trim();

                    split = rioVersionDict[sent[1]].Split(' ');
                    if (split[0] == "Mono")
                    {
                        versions["Mono"] = split[4];
                    }
                }


                callback();
            }).Start();
        }
    }
}
