using Nautilus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comforts.Commands
{
    internal class ComfortsConsoleCommands
    {
        [ConsoleCommand("SetPiracy")]
        public static void SetPiracy(bool pirated)
        {
            PirateChecker.isPirated = pirated;
        }
    }
}
