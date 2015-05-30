using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RobotDotNetBuildTasks;

namespace DeployRobot
{
    public class DeployManager : Manager
    {
        private ConnectionManager m_manager;

        public DeployManager(ref ConnectionManager manager)
        {
            m_manager = manager;
        }

        public bool Deploy(List<DeployFile> files, bool blocking = false)
        {
            if (!m_manager.Connected) return false;
            Thread t = new Thread(() =>
            {

                InstallDeployManager.DeployFiles(files.Select(f => f.FileNamePath).ToArray(), "/home/lvuser/mono", m_manager.Connection);

                
                OnTaskComplete(null);
            });
            t.Start();
            if (blocking)
            {
                t.Join();
            }

            return true;
        }
    }
}
