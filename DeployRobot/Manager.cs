using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeployRobot
{
    public class Manager
    {
        public event EventHandler TaskComplete;

        protected virtual void OnTaskComplete(EventArgs e)
        {
            EventHandler handler = TaskComplete;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
