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
        public static void DownloadMono(Action callback, DownloadProgressChangedEventHandler progressChanged)
        {
            Main.AppendToStatus("Downloading Mono");
            string dl =
                "https://onedrive.live.com/download?cid=D648460250CFE566&resid=d648460250cfe566%2134215&authkey=AHW_pNeWDEv5Q-s";

            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += progressChanged;
                client.DownloadFileCompleted += (sender, e) => callback();
                client.DownloadFileAsync(new Uri(dl), "Downloads" + Path.DirectorySeparatorChar + "Mono" + Path.DirectorySeparatorChar + "mono.zip");
            }
        }

        //Allow choice of which HAL to download. Allows either newest or recommeded HAL.
        public static void DownloadHAL(Action callback, DownloadProgressChangedEventHandler progressChanged, string version)
        {
            Main.AppendToStatus("Downloading HAL: " + version);
            string dl = "http://www.tortall.net/~robotpy/hal/" + version + "/libHALAthena_shared.so";

            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += progressChanged;
                client.DownloadFileCompleted += (sender, e) => callback();
                client.DownloadFileAsync(new Uri(dl), "Downloads" + Path.DirectorySeparatorChar + "HAL" + Path.DirectorySeparatorChar + "libHALAthena_shared.so");
            }
            /*
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
             * */
        }

        //Allow choice of which Template VSIX to download. Allows either newest or recommeded Template VSIX.
        public static void DownloadTemplates(Action callback, DownloadProgressChangedEventHandler progressChanged, string version)
        {

        }

        //Allow choice of which Deploy Extension to download. Allows either newest or recommeded Deploy Extension.
        public static void DownloadDeploy(Action callback, DownloadProgressChangedEventHandler progressChanged, string version)
        {

        }
    }
}
