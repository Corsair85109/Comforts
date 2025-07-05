using Nautilus.Json;
using Nautilus.Options.Attributes;
using System;
using UnityEngine;

namespace Comforts
{
    [Menu("Comforts Configuration")]
    public class ComfortsConfig : ConfigFile
    {
        [Toggle("Simulate cloth physics (is pretty janky, off by default")]
        public bool simulateClothPhysics = false;

        [Toggle("Activate jukebox secondary mode")]
        public bool jukeboxPresent = false;
    }
}
