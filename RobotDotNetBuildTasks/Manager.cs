using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotDotNetBuildTasks
{
    public class Manager
    {
        public event Action TaskComplete;

        protected virtual void OnTaskComplete()
        {
            Action handler = TaskComplete;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
