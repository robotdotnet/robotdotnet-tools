using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibGit2Sharp;
using static ReleaseBuilder.Properties.Resources;

namespace ReleaseBuilder.Projects
{
    class Extension : IProject
    {
        public string Version { get; set; } = "";
        public bool Checked { get; set; }
        public override string ToString() => name;

        private string name;

        public string Name => name;


        public Extension()
        {
            name = "FRC Extension";
        }

        public void CloneRepository()
        {
            Repository.Clone(ExtensionRepository, name);
        }

        public void PatchVsix(NetworkTablesDotNet nt, WPILib wpi)
        {
            if (Version == "")
                throw new InvalidOperationException($"Assembly Version not set yet. {name}");
            var vsixFile = File.ReadAllLines($"{name}\\{name}\\source.extension.vsixmanifest");

            for (int i = 0; i < vsixFile.Length; i++)
            {
                if (vsixFile[i].Contains("<Identity Id=\"FRC_Extension\""))
                {
                    vsixFile[i] =
                        $"    <Identity Id=\"FRC_Extension\" Version=\"{Version}\" Language=\"en-US\" Publisher=\"RobotDotNet\" />";
                }
            }



            File.Delete($"{name}\\{name}\\source.extension.vsixmanifest");
            Thread.Sleep(50);
            File.WriteAllLines($"{name}\\{name}\\source.extension.vsixmanifest", vsixFile);

            string csFile = $"{name}\\{name}\\{name}.csproj";

            var file = File.ReadAllLines(csFile);
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i].Contains("<Content Include=\"packages\\WPILib"))
                {
                    file[i] = $"    <Content Include=\"packages\\WPILib.{wpi.Version}.nupkg\">";
                }
                if (file[i].Contains("<Content Include=\"packages\\NetworkTablesDotNet"))
                {
                    file[i] = $"    <Content Include=\"packages\\NetworkTablesDotNet.{nt.Version}.nupkg\">";
                }
            }

            File.Delete(csFile);
            Thread.Sleep(50);
            File.WriteAllLines(csFile, file);

            foreach (var s in Directory.EnumerateFiles($"{name}\\Templates\\CSharp\\Project Templates", "*.vstemplate", SearchOption.AllDirectories))
            {
                var templateFile = File.ReadAllLines(s);
                for (int i = 0; i < templateFile.Length; i++)
                {
                    if (templateFile[i].Contains("package id=\"WPILib\""))
                    {
                        templateFile[i] = $"       <package id=\"WPILib\" version=\"{wpi.Version}\"/>";
                    }
                    if (templateFile[i].Contains("<package id=\"NetworkTablesDotNet\""))
                    {
                        templateFile[i] = $"       <package id=\"NetworkTablesDotNet\" version=\"{nt.Version}\"/>";
                    }
                }

                File.Delete(s);
                Thread.Sleep(50);
                File.WriteAllLines(s, templateFile);
            }




        }

        public void MoveNuget(NetworkTablesDotNet nt, WPILib wpi)
        {
            foreach (var f in Directory.GetFiles($"{name}\\{name}\\packages"))
            {
                File.Delete(f);
            }

            File.Copy($"Packages\\WPILib.{wpi.Version}.nupkg", $"{name}\\{name}\\packages\\WPILib.{wpi.Version}.nupkg");
            File.Copy($"Packages\\NetworkTablesDotNet.{nt.Version}.nupkg", $"{name}\\{name}\\packages\\NetworkTablesDotNet.{nt.Version}.nupkg");
        }

        public void BuildExtension()
        {
            Process p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = "nuget";
            p.StartInfo.Arguments = $"restore \"{name}\\{name}\\packages.config\" -OutputDirectory \"{name}\\packages\" -NonInteractive";
            p.Start();
            p.WaitForExit();

            p = new Process();
            p.StartInfo.CreateNoWindow = false;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = MSBuildPath;
            p.StartInfo.Arguments = $"\"{name}\\{name}.sln\" /p:Configuration=Release /m /v:M /fl /flp:LogFile=Logs\\Solution-Template.log;Verbosity=Normal /nr:false";
            p.Start();
            p.WaitForExit();
        }
    }


}
