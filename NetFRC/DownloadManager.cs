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
                "https://github.com/robotdotnet/robotdotnet.github.io/raw/master/Builds/Mono/4.0.1.zip";

            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += progressChanged;
                client.DownloadFileCompleted += (sender, e) => callback();
                client.DownloadFileAsync(new Uri(dl), "Downloads" + Path.DirectorySeparatorChar + "Mono" + Path.DirectorySeparatorChar + "mono.zip");
            }
        }
    }
}
