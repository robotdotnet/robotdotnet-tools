using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RobotDotNetBuildTasks;

namespace DeployRobot
{
    public class HALManager : Manager
    {
        private ConnectionManager manager;
        private string rioVersion = "";
        private bool matches = false;

        public bool Matches
        {
            get { return matches; }
        }
        

        public string HALVersion
        {
            get { return rioVersion; }
        }

        public HALManager(ref ConnectionManager manager)
        {
            this.manager = manager;
        }

        public bool CheckHALVersion(double netHAL, double soHAL, bool blocking = false)
        {

            bool retVal = false;
            Thread t = new Thread(() =>
            {
                if (manager.Connected)
                {
                    string[] send = {"cat /home/lvuser/hal/version"};
                    var rioVersionDict = InstallDeployManager.RunCommands(send, manager.Connection);
                    rioVersion = rioVersionDict[send[0]];
                    rioVersion = rioVersion.Replace("\n", "");
                    var split = rioVersion.Split(':');
                    if (split.Length < 2)
                        retVal = false;
                    double rioVersionNet, rioVersionSo;

                    var success = double.TryParse(split[0], out rioVersionNet);
                    if (!success)
                        retVal = false;
                    success = double.TryParse(split[1], out rioVersionSo);
                    if (!success)
                        retVal = false;

                    if (rioVersionNet == netHAL && rioVersionSo == soHAL)
                    {
                        retVal = true;
                    }
                    matches = retVal;
                }
                else
                {
                    retVal = false;
                    matches = false;
                    rioVersion = "";
                }
                OnTaskComplete(null);
            });

            t.Start();
            if (blocking)
                t.Join();
            return retVal;
        }
    }
}
