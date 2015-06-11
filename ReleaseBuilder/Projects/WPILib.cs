using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BitPantry.AssemblyPatcher;
using LibGit2Sharp;
using ReleaseBuilder.Utilities;

namespace ReleaseBuilder.Projects
{
    class WPILib : IProject
    {
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

        public void PatchAssembly()
        {
            foreach (var s in Directory.EnumerateFiles(Name, "*.csproj", SearchOption.AllDirectories))
            {
                Main.WriteStatusWindow($"Patching {s}");
                VersionPatcher.Patch(Name, s, "0.5.0.0");//TODO: Replace with Version
            }
        }

        public void PatchNuGet(Dictionary<string, string> toPatch)
        {
        }

        public string Name { get; }
        public string LatestVersion { get; private set; } = "";
        public bool CloneComplete { get; private set; } = false;
        public bool AsmComplete { get; private set; } = false;
    }
}
