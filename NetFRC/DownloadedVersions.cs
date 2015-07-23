using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace NetFRC
{
    class DownloadedVersions
    {


        private static Dictionary<string, string> versions = new Dictionary<string, string>();

        public static readonly string dlFileLocation = "Downloads" + Path.DirectorySeparatorChar + "downloadedversions.txt";

        public static Dictionary<string, string> Versions
        {
            get { return versions; }
        }

        public static void Start(Action callback)
        {
            Main.AppendToStatus("Checking Downloaded Versions");
            new Thread(() =>
            {
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

        public static void WriteTxt()
        {
            Main.AppendToStatus("Writing Downloaded Versions File");
            new Thread(() =>
                {
                    if (File.Exists(dlFileLocation))
                    {
                        File.Delete(dlFileLocation);
                    }

                    using (StreamWriter writer = new StreamWriter(dlFileLocation))
                    {
                        foreach(var s in versions)
                        {
                            string toWrite = s.Key + ":" + s.Value;
                            writer.WriteLine(toWrite);
                        }
                        writer.Close();
                    }


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
