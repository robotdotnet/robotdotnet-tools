﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;

namespace RobotDotNetBuildTasks
{
    public class ConnectionManager : Manager
    {
        private string connectionIP;
        private ConnectionType connectionType;

        private ConnectionInfo m_connection = null;

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


        public void Connect(string teamNumber, bool blocking = false, bool admin = false)
        {
            m_connection = null;
            Thread t = new Thread(() =>
            {
                m_connection = RoboRIOConnection.CheckConnection(teamNumber, out connectionType, out connectionIP, admin);
                OnTaskComplete();
            });

            t.Start();
            if (blocking)
            {
                t.Join();
            }

        }

        public string GetConnectionStatus()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Connected ? "Connected to RoboRIO..." : "Connection to RoboRIO failed...");
            builder.AppendLine("Interface: " + connectionType.ToString());
            builder.Append("IP Address: " + connectionIP);
            return builder.ToString();
        }
    }
}
