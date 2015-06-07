using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Renci.SshNet;

namespace RoboRIO_Tool
{
    public class ConnectionManager
    {
        private string connectionIP;
        private ConnectionType connectionType;

        private ConnectionInfo m_connection = null;

        private IConsoleWriter writer;

        public bool Connected
        {
            get
            {
                if (m_connection == null)
                    return false;
                return true;
            }
        }

        public ConnectionManager(IConsoleWriter writer)
        {
            this.writer = writer;
        }

        public ConnectionInfo ConnectionInfo
        {
            get { return m_connection; }
        }

        public string ConnectionIP
        {
            get { return connectionIP; }
        }

        public event Action ConnectionComplete;

        public void ConnectAsync(string teamNumber, bool admin = false)
        {
            ConnectAsync(teamNumber, TimeSpan.FromSeconds(2), admin);
        }

        public void ConnectAsync(string teamNumber, TimeSpan timeout, bool admin = false)
        {
            m_connection = null;
            Thread t = new Thread(() =>
            {
                m_connection = RoboRIOConnection.CheckConnection(teamNumber, out connectionType, out connectionIP, admin);
                writer.WriteLine(GetConnectionStatus());
                if (ConnectionComplete != null)
                    ConnectionComplete();
            });

            t.Start();
        }

        public void Connect(string teamNumber, bool admin = false)
        {
            Connect(teamNumber, TimeSpan.FromSeconds(2), admin);
        }

        public void Connect(string teamNumber, TimeSpan timeout, bool admin = false)
        {
            m_connection = null;
            m_connection = RoboRIOConnection.CheckConnection(teamNumber, out connectionType, out connectionIP, admin);
            writer.WriteLine(GetConnectionStatus());


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
