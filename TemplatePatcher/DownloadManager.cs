using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TemplatePatcher
{
    class DownloadManager
    {
        public static string WPIVersion = "";
        public static string NTVersion = "";

        public static void GetNewestWPILib(Action OnComplete)
        {
            new Thread(() =>
            {
                CustomWebClient client = new CustomWebClient();
                client.DownloadFile("https://www.myget.org/F/robotdotnet/api/v2/package/WPILib/", "FRC Robot Templates\\packages\\WPILib.nupkg");
                try
                {
                    WPIVersion = client.ResponseURI.AbsolutePath.Split('-')[1];
                }
                catch (IndexOutOfRangeException e)
                {
                    
                }
                OnComplete();
            }).Start();
        }

        public static void GetNewestNT(Action OnComplete)
        {new Thread(() =>
            {
                CustomWebClient client = new CustomWebClient();
                client.DownloadFile(new Uri("https://www.myget.org/F/robotdotnet/api/v2/package/NetworkTablesDotNet/"), "FRC Robot Templates\\packages\\NetworkTablesDotNet.nupkg");
                try
                {
                    NTVersion = client.ResponseURI.AbsolutePath.Split('-')[1];
                }
                catch (IndexOutOfRangeException e)
                {
                    
                }
                OnComplete();
            }).Start();
        }
    }

    internal class CustomWebClient : WebClient
    {
        private Uri m_uri;

        public Uri ResponseURI
        {
            get { return m_uri; }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            m_uri = response.ResponseUri;
            return response;
        }
    }
}
