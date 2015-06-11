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
    class NetworkTablesDotNet : IProject
    {
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
                LatestVersion = version;
                AsmComplete = true;
            }).Start();
        }

        public void PatchAssembly()
        {
            //BitPantry.AssemblyPatcher.VersionPatcher.Patch("NetworkTablesDotNet", Directory.getd)
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
