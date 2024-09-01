using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;

namespace Comforts.Utils
{
    internal class ComfortUtils
    {
        public static void NautilusBasicText(string msg, float time)
        {
            Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
            message.ShowMessage(msg, time * Time.deltaTime);
        }
    }
}
