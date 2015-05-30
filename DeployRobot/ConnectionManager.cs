using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using RobotDotNetBuildTasks;

namespace DeployRobot
{
    public class ConnectionManager : Manager
    {
        private string connectionIP;
        private ConnectionType connectionType;

        ConnectionInfo m_connection = null;

        public ConnectionManager()
        {
        }

        public bool Connected
        {
            get
            {
                if (m_connection == null)
                    return false;
                return true;
            }
        }

        public ConnectionInfo Connection
        {
            get { return m_connection; }
        }

        public string ConnectionIP
        {
            get { return connectionIP; }
        }


        public void Connect(string teamNumber, bool blocking = false)
        {
            m_connection = null;
            Thread t = new Thread(() =>
            {
                m_connection = RoboRIOConnection.CheckConnection(teamNumber, out connectionType, out connectionIP);
                OnTaskComplete(null);
            });

            t.Start();
            if (blocking)
            {
                t.Join();
            }

        }
    }
}
