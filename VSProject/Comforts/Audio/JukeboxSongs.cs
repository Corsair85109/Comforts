using Nautilus.Handlers;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Audio
{
    internal class JukeboxSongs
    {
        internal static List<FMODAsset> songs = new List<FMODAsset>();

        internal static float range = 15f;
        internal static void RegisterSongs(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Utility.Logger.LogError("Could not find jukebox songs file directory!");
                return;
            }

            string[] songFiles = Directory.GetFiles(folder);

            if (songFiles.Length == 0)
            {
                Utility.Logger.LogWarning("Jukebox songs file directory is empty! Your jukebox will not play anything");
                return;
            }

            foreach (string songFile in songFiles)
            {
                Utility.Logger.Log("[Comforts]: songFile: " + songFile);
                string songName = Path.GetFileNameWithoutExtension(songFile);
                Utility.Logger.Log("[Comforts]: songName: " + songName);

                string busPath = AudioUtils.BusPaths.PlayerSFXs;
                var songSound = CustomSoundHandler.RegisterCustomSound(songName, songFile, busPath, AudioRegistrar.k3DSoundModes);
                songSound.setMode(FMOD.MODE.LOOP_NORMAL);
                songSound.set3DMinMaxDistance(0f, range);

                FMODAsset asset = AudioUtils.GetFmodAsset(songName);
                songs.Add(asset);
            }
        }
    }
}
