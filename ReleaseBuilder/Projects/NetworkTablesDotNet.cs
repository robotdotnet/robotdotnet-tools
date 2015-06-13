using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitPantry.AssemblyPatcher;
using LibGit2Sharp;
using ReleaseBuilder.Utilities;
using static ReleaseBuilder.Properties.Resources;
using NuGet;

namespace ReleaseBuilder.Projects
{
    class NetworkTablesDotNet : IProject
    {

        public string Version { get; set; } = ""; //Make sure this is the version is in the form x.x.x
        public bool Checked { get; set; } = false;
        public override string ToString() => name;

        private string name;

        public NetworkTablesDotNet()
        {
            name = nameof(NetworkTablesDotNet);
        }

        public void CloneRepository()
        {
            Repository.Clone(NetworkTablesDotNetRepository, name);
        }

        public void PatchAssembly()
        {
            if (Version == "")
                throw new InvalidOperationException($"Assembly Version not set yet. {name}");
            foreach (var s in Directory.EnumerateFiles(name, "*.csproj", SearchOption.AllDirectories))
            {
                Main.WriteStatusWindow($"Patching {s}");
                VersionPatcher.Patch(name, s, Version +".0");//TODO: Replace with Version
            }
        }

        public void BuildSolution()
        {
            StringBuilder builder = new StringBuilder();
            //Build the solution
            Process p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = MSBuildPath;
            p.StartInfo.Arguments = $"{name}\\{name}.sln /p:Configuration=Release /m /v:M /fl /flp:LogFile=Logs\\Solution-{name}.log;Verbosity=Normal /nr:false";
            //p.OutputDataReceived += ((sender, e) =>
            //{
            //    builder.AppendLine(e.Data);
            //    //Console.WriteLine(e.Data);
            //});
            //p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            //p.BeginOutputReadLine();
            p.WaitForExit();
            //File.WriteAllText("MSLog.txt", builder.ToString());
        }

        public void BuildDocumentation()
        { 
            //We need to patch the export location 
            string sandPath = $"{name}\\Sandcastle\\Sandcastle.shfbproj";
            if (File.Exists(sandPath))
            {
                string[] toPatch = File.ReadAllLines(sandPath);
                for (int i = 0; i < toPatch.Length; i++)
                {
                    if (toPatch[i].Contains("$pleasereplaceme$"))
                    {
                        toPatch[i] = toPatch[i].Replace("$pleasereplaceme$",
                            $"{Directory.GetCurrentDirectory()}\\{name}\\XMLOutput");
                    }
                }
                File.Delete(sandPath);
                Thread.Sleep(50);
                File.WriteAllLines(sandPath, toPatch);
            }

            //Build the documentation
            Process p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = MSBuildPath;
            p.StartInfo.Arguments = sandPath + $" /p:Configuration=Release /m /v:M /fl /flp:LogFile=Logs\\Sandcastle-{name}.log;Verbosity=Normal /nr:false";
            p.Start();
            p.WaitForExit();

            //Copy outputed XML files to build directory
            var xmlBuilt = Directory.GetFiles($"{name}\\XMLOutput\\", "*.xml").Select(Path.GetFileNameWithoutExtension);
            

            foreach (var s in xmlBuilt)
            {
                string xmlFile = $"{name}\\{s}\\Output\\{s}.xml";
                if (File.Exists(xmlFile))
                {
                    File.Delete(xmlFile);
                    File.Copy($"{name}\\XMLOutput\\{s}.xml", xmlFile);
                }
            }
        }

        public void BuildNuGet()
        {
            //Find all csProj
            foreach (var s in Directory.EnumerateFiles(name, "*.csproj", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory($"Packages");
                //Build the NuPk
                Process p = new Process();
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "nuget";
                p.StartInfo.Arguments = $"pack {s} -o Packages -Symbols -p Configuration=Release -version {Version}";
                p.Start();
                p.WaitForExit();
            }


            
        }


        /*

        //Starting Fresh

        public NetworkTablesDotNet()
        {
            Name = nameof(NetworkTablesDotNet);
        }


        public void CloneRepositoryAsync()
        {
            new Thread(() =>
            {
                CloneOptions options = new CloneOptions();
                Repository.Clone(Properties.Resources.NetworkTablesDotNetRepository, Name, options);
                CloneComplete = true;
            }).Start();
        }

        public void BuildSolution()
        {
            //return null;
        }

        public void BuildDocumentation()
        {
            //return null;
        }

        public void GetLatestAssemblyVersionAsync()
        {
            new Thread(() =>
            {
                string version = "";
                Directory.CreateDirectory($"Temp\\{Name}");
                CustomWebClient client = new CustomWebClient();
                    client.DownloadFile(Properties.Resources.NetworkTablesDotNetNuGet,
                        $"Temp\\{Name}\\{Name}.nupkg");
                try
                {
                    int firstP = client.ResponseURI.AbsolutePath.IndexOf('.');
                    int lastP = client.ResponseURI.AbsolutePath.LastIndexOf('.');
                    version = client.ResponseURI.AbsolutePath.Substring(firstP + 1, (lastP - firstP) - 1);
                }
                catch (IndexOutOfRangeException)
                {
                }
                Version = version;
                AsmComplete = true;
            }).Start();
        }

        public void PatchAssembly()
        {
            foreach (var s in Directory.EnumerateFiles(Name, "*.csproj", SearchOption.AllDirectories))
            {
                Main.WriteStatusWindow($"Patching {s}");
                VersionPatcher.Patch(Name, s, Version);//TODO: Replace with Version
            }
        }

        public void PatchNuGet(Dictionary<string, string> toPatch)
        {
        }

        public string Name { get; }

        

        public bool CloneComplete { get; private set; } = false;
        public bool AsmComplete { get; private set; } = false;
        */
        
    }
}
