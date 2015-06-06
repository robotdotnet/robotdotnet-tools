using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;


namespace TemplatePatcher
{
    class Program
    {
        private static string WPIVersion;
        private static string NTVersion;

        private static object s_lockObject = new object();

        static bool dlWPI = false;
        static bool dlNT = false;

        private static List<TemplateManager> templateFiles = new List<TemplateManager>();

        static void Main(string[] args)
        {
            Console.CursorVisible = true;
            Console.Write("Enter Version Number: ");
            string ver = Console.ReadLine();
            Console.CursorVisible = false;

            new Thread(() =>
            {
                VSIXManager vsix = new VSIXManager("FRC Robot Templates\\source.extension.vsixmanifest");
                Console.WriteLine("Replacing VSIX Version to " + ver);
                vsix.ReplaceVersion(ver);
                Console.WriteLine("Writing VSIX File");
                vsix.WriteFile();
                Console.WriteLine("Sucessfully Wrote VSIX File");
            }).Start();

            foreach (var s in Directory.EnumerateFiles("CSharp\\Project Templates", "*.vstemplate", SearchOption.AllDirectories))
            {
                Console.WriteLine("Found Template File: " + s);
                templateFiles.Add(new TemplateManager(s));
            }


            //manager = new TemplateManager("IterativeRobot.vstemplate");
            Console.WriteLine("Downloading WPILib");
            DownloadManager.GetNewestWPILib(OnWPILibComplete);
            Console.WriteLine("Downloading NetworkTables");
            DownloadManager.GetNewestNT(OnNTComplete);

            while (!dlWPI || !dlNT)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Writing Template Files");
            foreach (var s in templateFiles)
            {
                Console.WriteLine("Writing " + s.FilePath);
                s.WriteFile();
            }
            Console.WriteLine("Sucessfully wrote template files.");
        }

        public static void OnNTComplete()
        {
            NTVersion = DownloadManager.NTVersion;

            lock (s_lockObject)
            {
                foreach (var s in templateFiles)
                {
                    Console.WriteLine("Updating NetworkTables Version in " + s.FilePath + " to " + NTVersion);
                    s.UpdateNT(NTVersion);
                }
                dlNT = true;
            }
        }

        public static void OnWPILibComplete()
        {
            WPIVersion = DownloadManager.WPIVersion;

            lock (s_lockObject)
            {
                foreach (var s in templateFiles)
                {
                    Console.WriteLine("Updating WPILib Version in " + s.FilePath + " to " + WPIVersion);
                    s.UpdateWPI(WPIVersion);
                }
                dlWPI = true;
            }
        }
    }
}
