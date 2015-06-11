using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseBuilder.Projects
{
    public interface IProject
    {
        void CloneRepositoryAsync();
        void BuildSolution();
        void BuildDocumentation();
        void GetLatestAssemblyVersionAsync();
            
        void PatchAssembly();
        void PatchNuGet(Dictionary<string, string> toPatch);

        string Name { get; }
        string LatestVersion { get; }

        bool CloneComplete { get; }
        bool AsmComplete { get; }

    }
}
