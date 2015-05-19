using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace hal_generator
{
    class HALFile
    {
        private StringBuilder setupDelegatesFunction = new StringBuilder();
        private StringBuilder baseFunctions = new StringBuilder();

        private StringBuilder roboRIOFunctions = new StringBuilder();

        public StringBuilder HALBaseFile = new StringBuilder();
        private StringBuilder HALRoboRIOFile = new StringBuilder();
        private string classname = "";
        private const string dllImport = "libHALAthena_shared.so";

        private CppFile cppFile;

        public HALFile(string fileName)
        {
            cppFile = new CppFile(fileName);
            cppFile.CreateFunctions();
            classname = Path.GetFileNameWithoutExtension(fileName);

            if (classname != "HAL")
                classname = "HAL" + classname;


            SetupDelegateSetupFunction();
            SetupBaseFunctions();
            GenerateHALBaseFile();
            GenerateRoboRIOFunctions();
            GenerateHALRoboRIOFile();

        }

        public void WriteFile()
        {
            using (StreamWriter writer = new StreamWriter("Output\\" + classname + "Generated.cs"))
            {
                writer.Write(HALBaseFile);
            }
            using (StreamWriter writer = new StreamWriter("OutputRIO\\" + classname + ".cs"))
            {
                writer.Write(HALRoboRIOFile);
            }
        }

        public void GenerateHALRoboRIOFile()
        {
            HALRoboRIOFile.AppendLine("using HAL_Base;");
            HALRoboRIOFile.AppendLine("namespace HAL_RoboRIO");
            HALRoboRIOFile.AppendLine("{");
            HALRoboRIOFile.AppendLine("public class " + classname);
            HALRoboRIOFile.AppendLine("{");
            HALRoboRIOFile.Append(roboRIOFunctions);
            HALRoboRIOFile.AppendLine("}");
            HALRoboRIOFile.AppendLine("}");

        }

        public void GenerateRoboRIOFunctions()
        {
            foreach (var f in cppFile.func)
            {
                string paramString = "(";
                bool first = true;


                foreach (var p in f.Parameters)
                {
                    if (first)
                    {
                        first = false;
                        paramString = paramString + TypeConversion.MarshallTypes(p.ParamType) + " " + p.ParamName;
                    }
                    else
                    {
                        paramString = paramString + ", " + TypeConversion.MarshallTypes(p.ParamType) + " " + p.ParamName;
                    }
                }
                paramString = paramString + ");";



                roboRIOFunctions.AppendLine("");
                roboRIOFunctions.AppendLine("[System.Runtime.InteropServices.DllImport(\"" + dllImport +
                                            "\", EntryPoint = \"" + f.FunctionNameHAL + "\")]");
                if (f.ReturnType == "bool")
                {
                    roboRIOFunctions.AppendLine("[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)]");
                }
                roboRIOFunctions.AppendLine("public static extern " + f.ReturnType + " " + f.FunctionNameHAL + paramString);

            }
        }

        public void GenerateHALBaseFile()
        {
            HALBaseFile.AppendLine("using System;");
            HALBaseFile.AppendLine("using System.Linq;");
            HALBaseFile.AppendLine("using System.Reflection;");

            HALBaseFile.AppendLine();
            HALBaseFile.AppendLine("namespace HAL_Base");
            HALBaseFile.AppendLine("{");
            HALBaseFile.AppendLine("public partial class " + classname);
            HALBaseFile.AppendLine("{");
            HALBaseFile.Append(setupDelegatesFunction);
            HALBaseFile.Append(baseFunctions);
            HALBaseFile.AppendLine("}}");
        }

        public void SetupDelegateSetupFunction()
        {
            setupDelegatesFunction.AppendLine("internal static void SetupDelegates()");
            setupDelegatesFunction.AppendLine("{");
            setupDelegatesFunction.AppendLine("string className = MethodBase.GetCurrentMethod().DeclaringType.Name;");
            setupDelegatesFunction.AppendLine("var types = HAL.HALAssembly.GetTypes();");
            setupDelegatesFunction.AppendLine("var q = from t in types where t.IsClass && t.Name == className select t;");
            setupDelegatesFunction.AppendLine("Type type = HAL.HALAssembly.GetType(q.ToList()[0].FullName);");

            foreach (var f in cppFile.func)
            {
                setupDelegatesFunction.AppendLine(f.FunctionNameNet + " = (" + f.FunctionNameDelegate +
                                                      ")Delegate.CreateDelegate(typeof(" + f.FunctionNameDelegate +
                                                      "), type.GetMethod(\"" + f.FunctionNameHAL + "\"));");
            }

            setupDelegatesFunction.AppendLine("}");
        }

        public void SetupBaseFunctions()
        {
            foreach (var f in cppFile.func)
            {
                string paramString = "(";
                bool first = true;
                foreach (var p in f.Parameters)
                {
                    if (first)
                    {
                        first = false;
                        paramString = paramString + p.ParamType + " " + p.ParamName;
                    }
                    else
                    {
                        paramString = paramString + ", " + p.ParamType + " " + p.ParamName;
                    }
                }

                paramString = paramString + ");";
                baseFunctions.AppendLine("");

                baseFunctions.AppendLine("public delegate " + f.ReturnType + " " + f.FunctionNameDelegate + paramString);
                baseFunctions.AppendLine("public static " + f.FunctionNameDelegate + " " + f.FunctionNameNet +
                                     ";");
            }
        }
    }
}
