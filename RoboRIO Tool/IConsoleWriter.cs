using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboRIO_Tool
{
    public interface IConsoleWriter
    {
        void Write(string value);
        void Write(int value);
        void Write(double value);
        void WriteLine(string value);
        void WriteLine(int value);
        void WriteLine(double value);
    }
}
