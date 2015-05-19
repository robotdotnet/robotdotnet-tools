using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace hal_generator
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<CppFile> files = new List<CppFile>();

            List<HALFile> files = new List<HALFile>();

            
            foreach (var s in Directory.EnumerateFiles(@"C:\Users\thad\Documents\GitHub\robotdotnet-tools\hal-generator\bin\Debug\Generator\Input"))
            {
                files.Add(new HALFile(s));
                
            }
             
            //files.Add(
              //  new HALFile(
                //    @"C:\Users\thad\Documents\GitHub\robotdotnet-tools\hal-generator\bin\Debug\Generator\Input\HAL.hpp"));
            
            foreach (var f in files)
            {
                f.WriteFile();
            }



            int x = 0;
            /*
            foreach (var s in Directory.EnumerateFiles(@"C:\Users\thad\Documents\GitHub\robotdotnet-tools\hal-generator\bin\Debug\Generator\Input"))
            {
                files.Add(new CppFile(s));
            }

            foreach (CppFile f in files)
            {
                f.CreateFunctions();
            }

            /*

            StringBuilder setupDelegatesFunction = new StringBuilder();
            StringBuilder functions = new StringBuilder();

            setupDelegatesFunction.AppendLine("internal static void SetupDelegate()");
            setupDelegatesFunction.AppendLine("{");
            setupDelegatesFunction.AppendLine("string className = MethodBase.GetCurrentMethod().DeclaringType.Name;");
            setupDelegatesFunction.AppendLine("var types = HAL.HALAssembly.GetTypes();");
            setupDelegatesFunction.AppendLine("var q = from t in types where t.IsClass && t.Name == className select t;");
            setupDelegatesFunction.AppendLine("Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);");

            foreach (var f in files)
            {
                foreach (var s in f.func)
                {
                    setupDelegatesFunction.AppendLine(s.FunctionNameNet + " = (" + s.FunctionNameDelegate +
                                                      ")Delegate.CreateDelegate(typeof(" + s.FunctionNameDelegate +
                                                      "), type.GetMethod(\"" + s.FunctionNameHAL + "\"));");
                }
            }


            int x = 0;

            //CppFile file = new CppFile("HAL.hpp");
            //file.CreateFunctions();
             * */
        }
    }
}
