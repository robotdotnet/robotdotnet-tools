using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoboRIO_Tool
{
    public class FileDeployManager
    {
        private ConnectionManager m_manager;

        private IConsoleWriter writer;

        public FileDeployManager(ref ConnectionManager manager, IConsoleWriter writer)
        {
            m_manager = manager;
            this.writer = writer;
        }

        public event Action<bool> DeployComplete;

        public void DeployFileAsync(string file, string deployLocation)
        {
            if (!m_manager.Connected)
            {
                if (DeployComplete != null)
                    DeployComplete(false);
                return;
            }
            Thread t = new Thread(() =>
            {
                writer.WriteLine("Deploying File.");
                bool retVal = RoboRIOConnection.DeployFiles(new[] {file}, deployLocation, m_manager.ConnectionInfo);
                writer.WriteLine(retVal ? "Successfully deployed file." : "File Deployment failed.");
                if (DeployComplete != null)
                    DeployComplete(retVal);
            });
            t.Start();

        }

        public void DeployFilesAsync(IEnumerable<string> files, string deployLocation)
        {
            if (!m_manager.Connected)
            {
                if (DeployComplete != null)
                    DeployComplete(false);
                return;
            }
            Thread t = new Thread(() =>
            {
                writer.WriteLine("Deploying Files.");
                bool retVal = RoboRIOConnection.DeployFiles(files, deployLocation, m_manager.ConnectionInfo);
                writer.WriteLine(retVal ? "Successfully deployed files." : "File Deployment failed.");
                if (DeployComplete != null)
                    DeployComplete(retVal);
            });
            t.Start();
        }

        public bool DeployFile(string file, string deployLocation)
        {
            if (!m_manager.Connected)
                return false;
            writer.WriteLine("Deploying File.");
            bool retVal = RoboRIOConnection.DeployFiles(new[] { file }, deployLocation, m_manager.ConnectionInfo);
            writer.WriteLine(retVal ? "Successfully deployed file." : "File Deployment failed.");
            return retVal;
        }

        public bool DeployFiles(string[] files, string deployLocation)
        {
            if (!m_manager.Connected)
                return false;
            writer.WriteLine("Deploying Files.");
            bool retVal = RoboRIOConnection.DeployFiles(files, deployLocation, m_manager.ConnectionInfo);
            writer.WriteLine(retVal ? "Successfully deployed file." : "File Deployment failed.");
            return retVal;
        }
    }
}
