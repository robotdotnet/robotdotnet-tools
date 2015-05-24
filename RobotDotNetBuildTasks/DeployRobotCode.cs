using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RobotDotNetBuildTasks
{
    public class DeployRobotCode : Task
    {
        public int TeamNumber { get; set; }

        public string RoboRIOUser { get; set; }

        public string RoboRIOPassword { get; set; }

        public string OutputDirectory { get; set; }

        public override bool Execute()
        {
            var dir = new DirectoryInfo(OutputDirectory);
            return InstallDeployManager.DeployAndExecuteRobotCode(TeamNumber, RoboRIOUser, RoboRIOPassword, dir);
        }
    }
}
