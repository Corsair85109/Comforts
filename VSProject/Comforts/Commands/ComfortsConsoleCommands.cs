using Nautilus.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Commands
{
    internal class ComfortsConsoleCommands
    {
        // thanks eldritch
        [ConsoleCommand("W")]
        public static void WarpForwardShortcut(float distance, bool setWalk = false)
        {
            Transform aimingTransform = Player.main.camRoot.GetAimingTransform();
            Player.main.SetPosition(Player.main.transform.position + aimingTransform.forward * distance);
            Player.main.OnPlayerPositionCheat();
            Player.main.precursorOutOfWater = setWalk;
        }
    }
}
