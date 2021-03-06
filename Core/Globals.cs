﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
namespace Reclamation.Core
{

    public static class Globals
    {
        private static string s_testData = "";
        private static string s_cfgData = "";

        /// <summary>
        /// Returns Test data path, in the source code tree.
        /// </summary>
        public static string TestDataPath
        { 
            get
            {
                if (s_testData != "")
                    return s_testData;

                string dir = Path.Combine(GetPathAbove("Pisces"), "PiscesTestData", "data");
                Console.WriteLine(dir);

                s_testData = dir;
                return s_testData;
            }
        }

        /// <summary>
        /// Returns cfg data path, in the source code tree.
        /// </summary>
        public static string CfgDataPath
        {
            get
            {
                if (s_cfgData != "")
                    return s_cfgData;

                string dir = Path.Combine(GetPathAbove("Pisces"), "Hydromet", "cfg");
                Console.WriteLine(dir);

                s_cfgData = dir;
                return s_cfgData;
            }
        }


        private static string GetPathAbove(string part)
        {
            string rval = "";
            //asmList[6].CodeBase
            //file:///C:/Users/KTarbet/Documents/project/Pisces/Core/bin/x86/Debug/Reclamation.Core.DLL
            //Reclamation.Core, Version=2.0.0.10, Culture=neutral, PublicKeyToken=null
            var asmList = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly item in asmList)
            {
                if (item.FullName.IndexOf("Reclamation.Core") == 0)
                {
                    Uri u = new Uri(item.CodeBase);
                    var dir = u.AbsolutePath.Replace("%20", " ");
                    int idx = dir.ToLower().LastIndexOf(part.ToLower() + "/");
                    if (idx > 0)
                        dir = dir.Substring(0, idx + part.Length); // include 'pisces'
                    
                    rval = Path.GetFullPath(dir);
                    break;
                }
            }
            return rval;
        }


        public static string LocalConfigurationDataPath {

            get
            {

                var s = ConfigurationManager.AppSettings["LocalConfigurationDataPath"];
                if (s == null || s == "")
                {
                    Logger.WriteLine("Error: LocalConfigurationDataPath is not defined in the config file");
                    return "";
                }

                if (!Directory.Exists(s))
                {
                    var s2 = ConfigurationManager.AppSettings["LocalConfigurationDataPath2"];
                    if (s2 != "" && s2 != null)
                        s = s2;
                }
                return s;

            }
        }

       
       
    }
    
}
