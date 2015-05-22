using System;
using System.Collections.Generic;

namespace DeployRobot
{
    class Argument
    {
        public string help { get; set; }
        public bool required { get; set; }
        public string shortName { get; set; }
        public string longName { get; set; }
        public bool cmdSwitch { get; set; }
        public string defaultF { get; set; }
        public Argument(string shortName, string longName, bool required, bool cmdSwitch, string help, string defaultF = "")
        {
            this.required = required;
            this.help = help;
            this.shortName = shortName;
            this.longName = longName;
            this.cmdSwitch = cmdSwitch;
            this.defaultF = defaultF;
        }
    }

    /// <summary>
    /// This is a library to parse command line objects into a readable form.
    /// </summary>
    public class ArgParse
    {
        private static List<Argument> testArguments = new List<Argument>();

        /// <summary>
        /// Parses Arguments into a dictionary out of a string array
        /// Note that needed arguments must be set using AddArgument
        /// </summary>
        /// <param name="arguments">Arguments to parse</param>
        /// <returns>Dictionary of parsed arguments</returns>
        public static Dictionary<string, string> ParseArguments(string[] arguments)
        {
            //int numberOfArguments = 0;
            Dictionary<string, string> parsedArguments = new Dictionary<string, string>();

            //Sorts through all arguments specific to the current running test
            foreach (Argument t in testArguments)
            {
                string key, value;

                //Check the argument to see if it exists
                int argReturn = CheckArgument(t, arguments, out key, out value);
                //Successfully found and added argument
                if (argReturn == 0)
                {
                    parsedArguments.Add(key, value);
                    //numberOfArguments++;
                }
                //If either argument required and not found
                else if (argReturn == 2 || argReturn == 3)
                {
                    return null;
                }
                //Do nothing on return code 3, which is argument not required
            }
            return parsedArguments;
        }

        //Checks argument against list
        private static int CheckArgument(Argument searchArgument, string[] cmdArguments, out string key, out string value)
        {
            bool nextIsArgParam = false;
            key = null;
            value = null;
            bool foundArgument = false;

            //Loop through all arguments given in the cmd line
            foreach (string s in cmdArguments)
            //for (int i = 0; i < cmdArguments.Count; i++)
            {
                //string s = cmdArguments[i];
                //Request help and return
                if (s == "-h" || s == "--help")
                {
                    ShowAllHelp();
                    return 3;
                }

                //If searching for arguement type, not parameter of argument
                if (!nextIsArgParam)
                {
                    //Look for long name if using --"
                    if (s.StartsWith("--"))
                    {
                        var temp = s.Remove(0, 2);
                        //If argument has param continue to look at next argument
                        if (temp == searchArgument.longName && !searchArgument.cmdSwitch)
                        {
                            nextIsArgParam = true;
                            continue;
                        }
                        //If argument has no param return found
                        else if (temp == searchArgument.longName && searchArgument.cmdSwitch)
                        {
                            key = searchArgument.longName;
                            value = "1";
                            foundArgument = true;
                            break;
                        }
                    }
                    //Look for short name if using -"
                    else if (s.StartsWith("-"))
                    {
                        var temp = s.Remove(0, 1);
                        //If argument has param continue to look at next argument
                        if (temp == searchArgument.shortName && !searchArgument.cmdSwitch)
                        {
                            nextIsArgParam = true;
                            continue;
                        }
                        //If argument has no param return found
                        else if (temp == searchArgument.shortName && searchArgument.cmdSwitch)
                        {
                            key = searchArgument.longName;
                            value = "1";
                            foundArgument = true;
                            break;
                        }
                    }
                    //Entered argument is not what we are looking for. continuing.
                    else
                    {
                    }
                }
                //If told to look at the next argument for the parameter.
                else
                {
                    key = searchArgument.longName;
                    value = s;
                    nextIsArgParam = false;
                    foundArgument = true;
                    break;
                }
            }
            if (searchArgument.defaultF != "" && key == null)
            {
                key = searchArgument.longName;
                value = searchArgument.defaultF;
            }
            if (searchArgument.required && !foundArgument)
            {
                ShowHelp(searchArgument);
                return 2;
            }
            if (key == null || value == null)
                return 1;
            else
            {
                return 0;
            }
        }

        //Shows help if missing argument
        private static void ShowHelp(Argument missingArg)
        {
            string line1 = "usage : " + System.AppDomain.CurrentDomain.FriendlyName + " [-h] ";
            foreach (Argument a in testArguments)
            {
                if (a.required)
                {
                    line1 = line1 + "-" + a.shortName + " " + a.longName.ToUpper() + " ";
                }
                else
                {
                    line1 = line1 + "[-" + a.shortName + " " + a.longName.ToUpper() + "] ";
                }
            }
            Console.WriteLine(line1);
            string line2 = System.AppDomain.CurrentDomain.FriendlyName + ": error: argument -" + missingArg.shortName + "/--" + missingArg.longName + " is required";
            Console.WriteLine(line2);
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
            //Console.WriteLine("usage: " + System.AppDomain.CurrentDomain.FriendlyName + " [-h] " +  
        }

        //Show help if you ask for it
        private static void ShowAllHelp()
        {
            string blank = "                    ";
            string line1 = "usage : " + System.AppDomain.CurrentDomain.FriendlyName + " [-h] ";
            foreach (Argument a in testArguments)
            {
                if (a.required)
                {
                    line1 = line1 + "-" + a.shortName + " " + a.longName.ToUpper() + " ";
                }
                else
                {
                    line1 = line1 + "[-" + a.shortName + " " + a.longName.ToUpper() + "] ";
                }
            }
            Console.WriteLine(line1);
            Console.WriteLine();
            Console.WriteLine("Optional arguments: ");
            Console.WriteLine("  -h. --help");
            Console.WriteLine(blank + "show this help message and exit");
            foreach (Argument a in testArguments)
            {
                if (!a.required)
                {
                    string line = "  -" + a.shortName + " " + a.longName.ToUpper() + ", --" + a.longName + " " + a.longName.ToUpper();
                    Console.WriteLine(line);
                    Console.WriteLine(blank + a.help);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Required arguments: ");
            foreach (Argument a in testArguments)
            {
                if (a.required)
                {
                    string line = "  -" + a.shortName + " " + a.longName.ToUpper() + ", --" + a.longName + " " + a.longName.ToUpper();
                    Console.WriteLine(line);
                    Console.WriteLine(blank + a.help);
                }
            }
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// Add an argument to the list of arguments. This is defaulted to be a parameterized switch
        /// </summary>
        /// <param name="shortName">The argument used if using -</param>
        /// <param name="keyName">The argument used if using --. Also the Key Name in the dictionary</param>
        /// <param name="required">If the argument is required</param>
        /// <param name="help">Text for the help output</param>
        public static void AddArgument(string shortName, string keyName, bool required, string help)
        {
            AddArgument(shortName, keyName, required, false, help);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="keyName"></param>
        /// <param name="defaultK"></param>
        /// <param name="help"></param>
        public static void AddArgument(string shortName, string keyName, string defaultK, string help)
        {
            testArguments.Add(new Argument(shortName, keyName, false, false, help, defaultK));
        }

        /// <summary>
        /// Add an argument to the list of arguments
        /// </summary>
        /// <param name="shortName">The argument used if using -</param>
        /// <param name="keyName">The argument used if using --. Also the Key Name in the dictionary</param>
        /// <param name="required">If the argument is required</param>
        /// <param name="noParam">If the argument has a parameter afterward or is standalone</param>
        /// <param name="help">Text for the help output</param>
        public static void AddArgument(string shortName, string keyName, bool required, bool noParam, string help)
        {
            testArguments.Add(new Argument(shortName, keyName, required, noParam, help));
        }
    }
}
