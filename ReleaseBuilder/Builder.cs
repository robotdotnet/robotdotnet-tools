using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReleaseBuilder.Projects;

namespace ReleaseBuilder
{
    class Builder
    {
        public static void Run(List<IProject> projects)
        {

            NetworkTablesDotNet networkTable = null;
            WPILib wpiLib = null;


            //IEnumerable<Task<bool>> cloneQuery = from p in projects select p.CloneRepositoryAsync();
            //var asmQuery = from p in projects select p.GetLatestAssemblyVersionAsync();

            //Task < bool >[] cloneTasks = (from p in projects select p.CloneRepositoryAsync()).ToArray();
            //Task<bool>[] asmTasks = (from p in projects select p.GetLatestAssemblyVersionAsync()).ToArray();

            foreach (IProject project in projects)
            {
                Main.WriteStatusWindow($"Cloning Repository: {project.Name}");
                Main.WriteStatusWindow($"Getting Latest Assembly Version: {project.Name}");
                project.GetLatestAssemblyVersionAsync();
                project.CloneRepositoryAsync();

                if (project is NetworkTablesDotNet && networkTable == null)
                {
                    networkTable = project as NetworkTablesDotNet;
                }
                else if (project is WPILib && wpiLib == null)
                {
                    wpiLib = project as WPILib;
                }
            }


            foreach (var project in projects)
            {
                while (!project.CloneComplete || !project.AsmComplete)
                {
                    Thread.Sleep(20);
                }
            }

            Main.WriteStatusWindow($"Current NT Assembly Version: {networkTable?.LatestVersion}");
            Main.WriteStatusWindow($"Current WPILib Assembly Version: {wpiLib?.LatestVersion}");

            foreach (var project in projects)
            {
                project.PatchAssembly();
            }


        }
    }
}
