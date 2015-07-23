using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BitPantry.AssemblyPatcher;
using NuGet;
using ReleaseBuilder.Projects;

namespace ReleaseBuilder
{
    internal struct CheckedProject
    {
        public readonly IProject project;
        public readonly bool check;

        public CheckedProject(IProject proj, bool check)
        {
            project = proj;
            this.check = check;
        }

    }
    class Builder
    {
        
        public static void Run(List<CheckedProject> projects, Dictionary<string, string> versions )
        {
            NetworkTablesDotNet networkTable = null;
            WPILib wpiLib = null;
            Extension ext = null;
            


            //IEnumerable<Task<bool>> cloneQuery = from p in projects select p.CloneRepositoryAsync();
            //var asmQuery = from p in projects select p.GetLatestAssemblyVersionAsync();

            //Task < bool >[] cloneTasks = (from p in projects select p.CloneRepositoryAsync()).ToArray();
            //Task<bool>[] asmTasks = (from p in projects select p.GetLatestAssemblyVersionAsync()).ToArray();

            foreach (var project in projects)
            {
                //Main.WriteStatusWindow($"Cloning Repository: {project.Name}");
                //Main.WriteStatusWindow($"Getting Latest Assembly Version: {project.Name}");
                //project.GetLatestAssemblyVersionAsync();
                //project.CloneRepositoryAsync();
                project.project.Checked = project.check;

                if (project.project is NetworkTablesDotNet && networkTable == null)
                {
                    networkTable = project.project as NetworkTablesDotNet;
                }
                else if (project.project is WPILib && wpiLib == null)
                {
                    wpiLib = project.project as WPILib;
                }
                else if (project.project is Extension && ext == null)
                {
                    ext = project.project as Extension;
                }
            }

            Directory.CreateDirectory("Logs");

            //Doing Network Tables
            networkTable.Version = versions["NT"];
            //if checked build the assembly
            if (networkTable.Checked)
            {
                Main.WriteStatusWindow("Cloning NetworkTables");
                networkTable.CloneRepository();
                Main.WriteStatusWindow("Patching NetworkTables");
                networkTable.PatchAssembly();
                Main.WriteStatusWindow("Building NetworkTables");
                networkTable.BuildSolution();
                Main.WriteStatusWindow("Building Documentation");
                networkTable.BuildDocumentation();
                Main.WriteStatusWindow("Packing NuGet");
                networkTable.BuildNuGet();
                Main.WriteStatusWindow("Finished Packing Nuget");

            }
            // Else Download the latest assembly
            else
            {
                string packageID = "FRC.NetworkTables";
                IPackageRepository repo =
                    PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                List<IPackage> packages = repo.FindPackagesById(packageID).ToList();
                packages = packages.Where(item => item.IsLatestVersion).ToList();

                if (packages.Count > 0)
                {
                    networkTable.Version = packages[0].Version.ToString();
                    Directory.CreateDirectory("Packages");
                    WebClient client = new WebClient();
                    client.DownloadFile(((DataServicePackage)packages[0]).DownloadUrl.AbsoluteUri, $"Packages\\{packages[0].Id}.{networkTable.Version}.nupkg");
                    //PackageManager packageManager = new PackageManager(repo, "Packages");
                    //packageManager.InstallPackage(packageID, packages[0].Version);
                }
            }

            wpiLib.Version = versions["WPI"];
            //Doing WPILib
            if (wpiLib.Checked)
            {
                Main.WriteStatusWindow("Cloning WPILib");
                wpiLib.CloneRepository();
                Main.WriteStatusWindow("Patching WPILib");
                wpiLib.PatchAssembly(networkTable);
                Main.WriteStatusWindow("Building WPILib");
                wpiLib.BuildSolution(networkTable);
                Main.WriteStatusWindow("Building WPILib Documentation");
                wpiLib.BuildDocumentation(networkTable);
                Main.WriteStatusWindow("Packing NuGet");
                wpiLib.BuildNuGet();
                Main.WriteStatusWindow("Finished Packing Nuget");
            }
            else
            {
                string packageID = "WPILib";
                IPackageRepository repo =
                    PackageRepositoryFactory.Default.CreateRepository("https://packages.nuget.org/api/v2");
                List<IPackage> packages = repo.FindPackagesById(packageID).ToList();
                packages = packages.Where(item => item.IsLatestVersion).ToList();

                if (packages.Count > 0)
                {
                    wpiLib.Version = packages[0].Version.ToString();
                    Directory.CreateDirectory("Packages");
                    WebClient client = new WebClient();
                    client.DownloadFile(((DataServicePackage)packages[0]).DownloadUrl.AbsoluteUri, $"Packages\\{packages[0].Id}.{wpiLib.Version}.nupkg");
                    //PackageManager packageManager = new PackageManager(repo, "Packages");
                    //packageManager.InstallPackage(packageID, packages[0].Version);
                }
            }

            ext.Version = versions["EXT"];
            if (ext.Checked)
            {
                Main.WriteStatusWindow("Cloning Extension");
                ext.CloneRepository();
                Main.WriteStatusWindow("Patching Extension");
                ext.PatchVsix(networkTable, wpiLib);
                ext.MoveNuget(networkTable, wpiLib);
                Main.WriteStatusWindow("Building Extension");
                ext.BuildExtension();
            }

            //Copy upload files.

            Directory.CreateDirectory("Output");

            string outDir = "Output";
            string packDir = "Packages";

            string vsixDir = $"{ext.Name}\\{ext.Name}\\Output";

            //Copy WPILib
            foreach (
                var s in
                    Directory.EnumerateFiles($"{packDir}\\", "FRC.WPI*",
                        SearchOption.AllDirectories))
            {
                Directory.CreateDirectory($"{outDir}");
                File.Copy(s, $"{outDir}\\{Path.GetFileName(s)}");
            }

            foreach (
                var s in
                    Directory.EnumerateFiles($"{packDir}\\", "FRC.NetworkTables*",
                        SearchOption.AllDirectories))
            {
                Directory.CreateDirectory($"{outDir}");
                File.Copy(s, $"{outDir}\\{Path.GetFileName(s)}");
            }

            File.Copy($"{vsixDir}\\FRC Extension.vsix", $"{outDir}\\FRC Extension.{ext.Version}.vsix");





        }
    }
}
