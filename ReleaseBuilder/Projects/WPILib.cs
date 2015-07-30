using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitPantry.AssemblyPatcher;
using LibGit2Sharp;
using NuGet;
using ReleaseBuilder.Utilities;
using static ReleaseBuilder.Properties.Resources;

namespace ReleaseBuilder.Projects
{
    class WPILib : IProject
    {
        public string Version { get; set; } = ""; //Make sure this is the version is in the form x.x.x
        public bool Checked { get; set; } = false;
        public override string ToString() => name;

        private string name;

        public WPILib()
        {
            name = "robotdotnet-wpilib";
        }

        public void CloneRepository()
        {
            CloneOptions options = new CloneOptions();
            //options.BranchName = "CSharp-6.0-Update";
            Repository.Clone(WPILibRepository, name, options);
        }

        public void PatchAssembly(NetworkTablesDotNet nt)
        {
            if (Version == "")
                throw new InvalidOperationException($"Assembly Version not set yet. {name}");
            foreach (var s in Directory.EnumerateFiles(name, "*.csproj", SearchOption.AllDirectories))
            {
                Main.WriteStatusWindow($"Patching {s}");
                VersionPatcher.Patch(name, s, Version + ".0");//TODO: Replace with Version

                //patching nuget
                string d = Path.GetDirectoryName(s) + "\\packages.config";

                if (File.Exists(d))
                {
                    var arr = File.ReadAllLines(d);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i].Contains("NetworkTables"))
                        {
                            arr[i] = $"  <package id=\"FRC.NetworkTables\" version=\"{nt.Version}\" targetFramework=\"net45\" />";
                        }
                    }
                    File.Delete(d);
                    Thread.Sleep(50);
                    File.WriteAllLines(d, arr);
                }

            }

            Process p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "nuget";
            p.StartInfo.Arguments = $"restore {name}\\{name}.sln";
            p.Start();
            p.WaitForExit();


        }

        public void BuildSolution(NetworkTablesDotNet nt)
        {
            //Get Newest Packages
            IPackageRepository repo = new LocalPackageRepository("Packages");



            string path = $"{name}\\packages";
            PackageManager manager = new PackageManager(repo, path);
            manager.InstallPackage("FRC.NetworkTables", SemanticVersion.Parse(nt.Version));

            //Patch csproj's for newest
            foreach (var s in Directory.EnumerateFiles(name, "*.csproj", SearchOption.AllDirectories))
            {
                string[] load = File.ReadAllLines(s);
                for (int i = 0; i < load.Length; i++)
                {
                    if (load[i].Contains("Reference Include=\"NetworkTables"))
                    {
                        load[i] =
                            $"    <Reference Include=\"NetworkTables, Version = {nt.Version}.0, Culture = neutral, processorArchitecture = MSIL\">";
                    }
                    if (load[i].Contains("<HintPath>..\\packages\\FRC.NetworkTables"))
                    {
                        load[i] =
                            $"      <HintPath>..\\packages\\FRC.NetworkTables.{nt.Version}\\lib\\net45\\NetworkTables.dll</HintPath>";
                    }
                }
                File.Delete(s);
                Thread.Sleep(50);
                File.WriteAllLines(s, load);
            }


                StringBuilder builder = new StringBuilder();
            //Build the solution
            Process p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = MSBuildPath;
            p.StartInfo.Arguments = $"{name}\\{name}.sln /p:Configuration=Release /m /v:M /fl /flp:LogFile=Logs\\Solution-{name}.log;Verbosity=Normal /nr:false";
            p.OutputDataReceived += ((sender, e) =>
            {
                builder.AppendLine(e.Data);
                //Console.WriteLine(e.Data);
            });
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            p.BeginOutputReadLine();
            p.WaitForExit();
            File.WriteAllText("MSLog.txt", builder.ToString());
        }

        public void BuildDocumentation(NetworkTablesDotNet nt)
        {
            //We need to patch the export location - Also patch the NT documentation location
            string sandPath = $"{name}\\Sandcastle\\WPILib.shfbproj";
            if (File.Exists(sandPath))
            {
                string[] toPatch = File.ReadAllLines(sandPath);
                for (int i = 0; i < toPatch.Length; i++)
                {
                    if (toPatch[i].Contains(@"C:\Users\thad\Documents\GitHub\robotdotnet-wpilib\Output"))
                    {
                        toPatch[i] = toPatch[i].Replace(@"C:\Users\thad\Documents\GitHub\robotdotnet-wpilib\Output",
                            $"{Directory.GetCurrentDirectory()}\\{name}\\XMLOutput");
                    }
                    if (toPatch[i].Contains("<DocumentationSource sourceFile=\"..\\packages\\NetworkTablesDotNet"))
                    {
                        if (toPatch[i].Contains("NetworkTablesDotNet.dll"))
                        {
                            toPatch[i] =
                                $"      <DocumentationSource sourceFile = \"..\\packages\\NetworkTablesDotNet.{nt.Version}\\lib\\net45\\NetworkTablesDotNet.dll\" />";
                        }
                        else
                        {
                            toPatch[i] =
                                $"      <DocumentationSource sourceFile = \"..\\packages\\NetworkTablesDotNet.{nt.Version}\\lib\\net45\\NetworkTablesDotNet.xml\" />";
                        }
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
            p.StartInfo.Arguments = sandPath + $" /p:Configuration=Release /flp:LogFile=Logs\\Sandcastle-{name}.log;Verbosity=Normal";
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
                if (Path.GetFileName(s) == "WPILib.csproj")
                    p.StartInfo.Arguments = $"pack {s} -o Packages -IncludeReferencedProjects -Symbols -p Configuration=Release -version {Version}";
                else
                {
                    p.StartInfo.Arguments = $"pack {s} -o Packages -Symbols -p Configuration=Release -version {Version}";
                }
                p.Start();
                p.WaitForExit();
            }



        }


        /*
        public WPILib()
        {
            Name = nameof(WPILib);
        }

        public void CloneRepositoryAsync()
        {
            new Thread(() =>
            {
                CloneOptions options = new CloneOptions();
                options.BranchName = "CSharp-6.0-Update";
                Repository.Clone(Properties.Resources.WPILibRepository, "WPILib", options);
                CloneComplete = true;
            }).Start();
        }

        public void BuildSolution()
        {
        }

        public void BuildDocumentation()
        {
        }

        public void GetLatestAssemblyVersionAsync()
        {
            new Thread(() =>
            {
                string version = "";
                Directory.CreateDirectory("Temp\\WPILib");
                CustomWebClient client = new CustomWebClient();
                client.DownloadFile(Properties.Resources.WPILibNuGet,
                    "Temp\\WPILib\\WPILIb.nupkg");
                try
                {
                    int firstP = client.ResponseURI.AbsolutePath.IndexOf('.');
                    int lastP = client.ResponseURI.AbsolutePath.LastIndexOf('.');
                    version = client.ResponseURI.AbsolutePath.Substring(firstP + 1, (lastP - firstP) - 1);
                }
                catch (IndexOutOfRangeException)
                {
                }
                LatestVersion = version;
                AsmComplete = true;
            }).Start();
        }

        public void PatchAssembly(string version)
        {
            foreach (var s in Directory.EnumerateFiles(Name, "*.csproj", SearchOption.AllDirectories))
            {
                Main.WriteStatusWindow($"Patching {s}");
                VersionPatcher.Patch(Name, s, version);//TODO: Replace with Version
            }
        }

        public void PatchNuGet(Dictionary<string, string> toPatch)
        {
        }

        public string Name { get; }

        public bool Checked { get; set; }

        public string LatestVersion { get; private set; } = "";
        public bool CloneComplete { get; private set; } = false;
        public bool AsmComplete { get; private set; } = false;

        public override string ToString() => nameof(WPILib);

    */
    }
}
