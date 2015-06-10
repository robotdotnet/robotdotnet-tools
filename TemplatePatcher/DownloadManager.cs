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
    [System.ComponentModel.DesignerCategory("")]
    class DownloadManager
    {
        public static string WPIVersion = "";
        public static string NTVersion = "";

        public static void GetNewestWPILib(Action OnComplete)
        {
            new Thread(() =>
            {
                const string dlLocation = "FRC Extension\\packages\\WPILib.nupkg";
                CustomWebClient client = new CustomWebClient();
                client.DownloadFile("https://www.nuget.org/api/v2/package/WPILib/", dlLocation);
                try
                {
                    int firstP = client.ResponseURI.AbsolutePath.IndexOf('.');
                    int lastP = client.ResponseURI.AbsolutePath.LastIndexOf('.');
                    WPIVersion = client.ResponseURI.AbsolutePath.Substring(firstP + 1, (lastP - firstP) - 1);
                    if (File.Exists(dlLocation.Replace(".nupkg", "." + WPIVersion + ".nupkg")))
                    {
                        File.Delete(dlLocation.Replace(".nupkg", "." + WPIVersion + ".nupkg"));
                    }
                    File.Move(dlLocation, dlLocation.Replace(".nupkg", "." + WPIVersion + ".nupkg"));
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
            const string dlLocation = "FRC Extension\\packages\\NetworkTablesDotNet.nupkg";
                CustomWebClient client = new CustomWebClient();
                client.DownloadFile(new Uri("https://www.nuget.org/api/v2/package/NetworkTablesDotNet/"), dlLocation);
                try
                {
                    int firstP = client.ResponseURI.AbsolutePath.IndexOf('.');
                    int lastP = client.ResponseURI.AbsolutePath.LastIndexOf('.');
                    NTVersion = client.ResponseURI.AbsolutePath.Substring(firstP + 1, (lastP - firstP) - 1);
                    if (File.Exists(dlLocation.Replace(".nupkg", "." + NTVersion + ".nupkg")))
                    {
                        File.Delete(dlLocation.Replace(".nupkg", "." + NTVersion + ".nupkg"));
                    }
                    File.Move(dlLocation, dlLocation.Replace(".nupkg", "." + NTVersion +".nupkg"));
                }
                catch (IndexOutOfRangeException e)
                {
                    
                }
                OnComplete();
            }).Start();
        }
    }

    [System.ComponentModel.DesignerCategory("")]
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
