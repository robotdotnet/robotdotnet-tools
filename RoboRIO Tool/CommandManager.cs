using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoboRIO_Tool
{
    public class CommandManager
    {
        private ConnectionManager m_manager;
        private IConsoleWriter writer;

        public CommandManager(ref ConnectionManager manager, IConsoleWriter writer)
        {
            m_manager = manager;
            this.writer = writer;
        }

        public event Action<Dictionary<string, string>> CommandCompleted;

        public void RunCommandAsync(string command)
        {
            if (!m_manager.Connected)
            {
                if (CommandCompleted != null)
                    CommandCompleted(null);
                return;
            }
            Thread t = new Thread(() =>
            {
                writer.WriteLine("Runnning Command - " + command);
                var retVal = RoboRIOConnection.RunCommands(new[] { command }, m_manager.ConnectionInfo);
                if (CommandCompleted != null)
                    CommandCompleted(retVal);
            });
            t.Start();
        }

        public void RunCommandsAsync(string[] commands)
        {
            if (!m_manager.Connected)
            {
                if (CommandCompleted != null)
                    CommandCompleted(null);
                return;
            }
            Thread t = new Thread(() =>
            {
                writer.WriteLine("Running Commands.");
                var retVal = RoboRIOConnection.RunCommands(commands, m_manager.ConnectionInfo);
                if (CommandCompleted != null)
                    CommandCompleted(retVal);
            });
            t.Start();
        }



        public string RunCommand(string command)
        {
            if (!m_manager.Connected) return null;

            writer.WriteLine("Running Command - " + command);
            var retVal = RoboRIOConnection.RunCommands(new[] { command }, m_manager.ConnectionInfo);
            return retVal[command];
        }

        public Dictionary<string, string> RunCommands(string[] commands)
        {
            if (!m_manager.Connected) return null;
            writer.WriteLine("Running Commands.");
            Dictionary<string, string> retVal = RoboRIOConnection.RunCommands(commands, m_manager.ConnectionInfo);
            return retVal;
        }
    }
}
