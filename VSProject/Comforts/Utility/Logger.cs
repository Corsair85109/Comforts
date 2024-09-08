using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Utility
{
    internal class Logger
    {
        private static string prefix = "[Comforts Log] ";
        public static void Log(string message)
        {
            Debug.Log(prefix + message);
        }
        public static void LogWarning(string message)
        {
            Debug.LogWarning(prefix + message);
        }
        public static void LogError(string message)
        {
            Debug.LogError(prefix + message);
        }
    }
}
