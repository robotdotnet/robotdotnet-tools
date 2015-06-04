using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;

namespace NetFRC
{
    class DownloadManager
    {
        //Always download the newest mono. We control this, and its not automatically built.
        public static void DownloadMono(Action callback)
        {
            new Thread(() =>
            {


                callback();
            }).Start();
        }

        //Allow choice of which HAL to download. Allows either newest or recommeded HAL.
        public static void DownloadHAL(Action callback, string version)
        {
            Main.AppendToStatus("Downloading HAL: " + version);
            new Thread(() =>
            {
                string dl = "http://www.tortall.net/~robotpy/hal/" + version + "/libHALAthena_shared.so";

                Directory.CreateDirectory("Downloads" + Path.DirectorySeparatorChar + "HAL");

                using (var client = new WebClient())
                {
                    client.DownloadFile(dl, "Downloads" + Path.DirectorySeparatorChar + "HAL" + Path.DirectorySeparatorChar + "libHALAthena_shared.so");
                }

                DownloadedVersions.Versions["HAL"] = version;
                DownloadedVersions.WriteTxt();

                callback();
            }).Start();
        }

        //Allow choice of which Template VSIX to download. Allows either newest or recommeded Template VSIX.
        public static void DownloadTemplates(Action callback, string version)
        {
            new Thread(() =>
            {


                callback();
            }).Start();
        }

        //Allow choice of which Deploy Extension to download. Allows either newest or recommeded Deploy Extension.
        public static void DownloadDeploy(Action callback, string version)
        {
            new Thread(() =>
            {


                callback();
            }).Start();
        }
    }
}
