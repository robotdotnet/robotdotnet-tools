using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotDotNetBuildTasks
{
    public class InstallRuntime : Task
    {
        public int TeamNumber { get; set; }

        public string RoboRIOUser { get; set; }

        public string RoboRIOPassword { get; set; }

        public string IPKLoc { get; set; }

        public string HALRoboRio { get; set; }

        public string HALImpl { get; set; }

        public override bool Execute()
        {
            return InstallDeployManager.InstallRuntime(TeamNumber, RoboRIOUser, RoboRIOPassword, new System.IO.FileInfo(IPKLoc), new System.IO.FileInfo(HALRoboRio), new System.IO.FileInfo(HALImpl));
        }
    }
}
