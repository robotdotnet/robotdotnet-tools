using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hal_generator
{
    public struct Parameter
    {
        public string ParamType { get; set; }

        public string ParamName { get; set; }
    }
    class Function
    {
        private string m_returnType;

        private List<Parameter> m_parameters;

        private string m_functionName;

        public string ReturnType
        {
            get { return m_returnType; }
        }

        public List<Parameter> Parameters
        {
            get { return m_parameters; }
        }

        public string FunctionNameHAL
        {
            get { return m_functionName; }
        }

        public string FunctionNameDelegate
        {
            get { return ToPascalCase(m_functionName) + "Delegate"; }
        }

        public string FunctionNameNet
        {
            get { return ToPascalCase(m_functionName); }
        }


        public Function(string cppFunction)
        {
            m_parameters = new List<Parameter>();
            m_returnType = "";
            m_functionName = "";
            CreateFunction(cppFunction);
        }

        public bool CreateFunction(string cppFunction)
        {

            string localString = cppFunction;
            if (cppFunction.IndexOf('/') != -1)
                localString = cppFunction.Substring(0, cppFunction.IndexOf('/'));
            localString = localString.Trim();//.Substring(0, cppFunction.IndexOf('/'));

            if (localString == "")
                return false;

            int paramStart = localString.IndexOf('(');
            int paramEnd = localString.LastIndexOf(')');

            string parameters = localString.Substring(paramStart, paramEnd - paramStart);
            char[] trimParameters = {'(', ')'};
            parameters = parameters.Trim(trimParameters);

            string[] parametersSplit = parameters.Split(',');
            int i = 0;
            foreach (string s in parametersSplit)
            {
                string local = s.Replace("const", "").Replace("enum", "").Replace("NULL", "null").Trim();
                if (local.Contains("unsigned short"))
                {
                    local = local.Replace("unsigned short", "uint16_t");
                }
                if (local.Contains('*'))
                {
                    string paramType = "";
                    string paramName = "";
                    string[] tmp = local.Split('*');
                    paramName = tmp[1].Trim();
                    if (local.Contains("char *") || local.Contains("char*"))
                        paramType = "string";
                    else if (local.Contains("uint8_t *") || local.Contains("uint8_t* "))
                        paramType = "byte[]";
                    else
                        paramType = local.Contains("void") ? "System.IntPtr" : "ref " + TypeConversion.ToCSharpType(tmp[0]);

                    m_parameters.Add(new Parameter { ParamType = paramType, ParamName = paramName });

                }
                else
                {
                    string[] splitP = local.Split(' ');
                    string paramName;
                    if (splitP[0] == "") 
                        continue;
                    if (splitP.Length <= 1)
                        paramName = "param" + i;
                    else
                        paramName = splitP[1].Trim();
                    string paramType = TypeConversion.ToCSharpType(splitP[0]);
                    //string paramName = splitP[1].Trim();
                    m_parameters.Add(new Parameter { ParamType = paramType, ParamName = paramName });
                }
            }

            string funcRetAndName = localString.Substring(0, (localString.IndexOf('(')));

            funcRetAndName = funcRetAndName.Replace("const", "").Replace("enum", "").Replace("NULL", "null").Trim();
            if (funcRetAndName.Contains("unsigned short"))
            {
                funcRetAndName = funcRetAndName.Replace("unsigned short", "uint16_t");
            }

            if (funcRetAndName.Contains('*'))
            {
                string retType = "";
                string funcName = "";

                string[] tmp = funcRetAndName.Split('*');
                funcName = tmp[1].Trim();
                if (funcRetAndName.Contains("char *") || funcRetAndName.Contains("char*"))
                    retType = "System.IntPtr";
                else
                    retType = funcRetAndName.Contains("void") ? "System.IntPtr" : TypeConversion.ToCSharpType(tmp[0]);

                m_returnType = retType;
                m_functionName = funcName;
            }
            else
            {
                string[] splitP = funcRetAndName.Split(' ');
                if (splitP.Length <= 1)
                    return false;
                string retType = TypeConversion.ToCSharpType(splitP[0]);
                string funcName = splitP[1].Trim();

                m_returnType = retType;
                m_functionName = funcName;
            }

            if (m_functionName == "initializeNotifier")
            {
                m_parameters.Clear();
                m_parameters.Add(new Parameter {ParamName = "ProcessQueue", ParamType = "NotifierDelegate"});
                m_parameters.Add(new Parameter {ParamName = "status", ParamType = "ref int"});
            }
            else if (m_functionName == "initializeNotifier")
            {
                m_parameters.Clear();
                m_parameters.Add(new Parameter { ParamName = "ProcessQueue", ParamType = "NotifierDelegate" });
                m_parameters.Add(new Parameter { ParamName = "status", ParamType = "ref int" });
            }



            return true;



        }

        private static string ToPascalCase(string theString)
        {
            // If there are 0 or 1 characters, just return the string.
            if (theString == null) return theString;
            if (theString.Length < 2) return theString.ToUpper();

            // Split the string into words.
            string[] words = theString.Split(
                new char[] { },
                StringSplitOptions.RemoveEmptyEntries);

            // Combine the words.
            string result = "";
            foreach (string word in words)
            {
                result +=
                    word.Substring(0, 1).ToUpper() +
                    word.Substring(1);
            }

            return result;
        }
    }
}
