using Nautilus.Handlers;
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
        internal static void RegisterSongs(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Debug.LogError("Could not find jukebox songs file directory!");
                return;
            }

            string[] songFiles = Directory.GetFiles(folder);

            if (songFiles.Length == 0)
            {
                Debug.LogWarning("Jukebox songs file directory is empty! Your jukebox will not play anything");
                return;
            }

            foreach (string songFile in songFiles)
            {
                Debug.Log("songFile: " + songFile);
                string songName = Path.GetFileNameWithoutExtension(songFile);
                Debug.Log("songName: " + songName);

                string busPath = Nautilus.Utility.AudioUtils.BusPaths.PlayerSFXs;
                CustomSoundHandler.RegisterCustomSound(songName, songFile, busPath, FMOD.MODE.DEFAULT);

                FMODAsset asset = ScriptableObject.CreateInstance<FMODAsset>();
                asset.id = songName;
                songs.Add(asset);
            }


            // Log songs found
            Debug.Log("Found songs:");
            foreach (FMODAsset asset in songs)
            {
                Debug.Log(asset.id);
            }
        }
    }
}
