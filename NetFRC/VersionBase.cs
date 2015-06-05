using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFRC
{
    public abstract class VersionBase
    {
        protected static Dictionary<string, string> versions = new Dictionary<string, string>();

        public static Dictionary<string, string> Versions
        {
            get { return versions; }
        }
    }
}
