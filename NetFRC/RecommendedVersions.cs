using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;

namespace NetFRC
{
    class RecommendedVersions
    {
        private static Dictionary<string, string> versions = new Dictionary<string, string>();

        public static readonly string dlFileLocation = "Downloads" + Path.DirectorySeparatorChar + "recommendedversions.txt";

        public static void Start(Action callback)
        {
            Main.AppendToStatus("Getting Recommended Versions");
            new Thread(() =>
            {
                versions.Clear();
                using (var client = new WebClient())
                {
                    client.DownloadFile(
                        "https://raw.githubusercontent.com/robotdotnet/robotdotnet-wpilib/master/recommendedver.txt",
                        dlFileLocation);
                }

                if (File.Exists(dlFileLocation))
                {
                    foreach (var split in File.ReadAllLines(dlFileLocation).Select(l => l.Split(':')).Where(split => split.Length >= 2))
                    {
                        Main.AppendToStatus("Found: " + split[0]);
                        versions[split[0]] = split[1];
                    }
                }

                callback();
            }).Start();
        }

        private static bool FoundMono()
        {
            return versions.ContainsKey("MONO");
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
