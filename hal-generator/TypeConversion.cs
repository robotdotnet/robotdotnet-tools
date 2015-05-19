using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hal_generator
{
    class TypeConversion
    {
        public static string MarshallTypes(string orig)
        {
            var local = orig.Trim();
            switch (local)
            {
                case "bool":
                    {
                        return "[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool";
                    }

                case "char":
                    {
                        return 
                        "[System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string";
                    }

                default:
                    {
                        return local;
                    }
            }
        }

        public static string ToCSharpType(string orig)
        {
            var local = orig.Trim();
            switch (local)
            {
                case "uint8_t":
                {
                    return "byte";
                }

                case "int8_t":
                {
                    return "sbyte"; 
                }

                case "uint16_t":
                {
                    return "ushort";
                }

                case "int16_t":
                {
                    return "short";
                }

                case "uint32_t":
                {
                    return "uint";
                }

                case "int32_t":
                {
                    return "int";
                }

                case "uint64_t":
                {
                    return "ulong";
                }

                case "int64_t":
                {
                    return "long";
                }

                case "bool":
                {
                    return "bool";//"[MarshalAs(System.Runtime.InteropServices.UnmanagedType.I1)] bool";
                }

                case "char":
                {
                    return "byte";
                        //"[System.Runtime.InteropServices.InAttribute()] [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)] string";
                }

                case "MULTIWAIT_ID":
                {
                    return "System.IntPtr";
                }

                case "MUTEX_ID":
                {
                    return "System.IntPtr";
                }

                case "SEMAPHORE_ID":
                {
                    return "System.IntPtr";
                }

                case "unsigned":
                {
                    return "uint";
                }
                    
                default:
                {
                    return local;
                }

            }
        }
    }
}
