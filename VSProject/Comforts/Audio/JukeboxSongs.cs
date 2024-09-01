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
        private static string[] songPaths;

        internal static List<FMODAsset> songs = new List<FMODAsset>();
        internal static void RegisterSongs(string folder)
        {
            if (Directory.Exists(folder))
            {
                songPaths = Directory.GetFiles(folder);
                foreach (string song in songPaths)
                {
                    string busPath = "bus:/master/SFX_for_pause/PDA_pause/all/SFX/reverbsend";
                    CustomSoundHandler.RegisterCustomSound(song, song, busPath, FMOD.MODE.DEFAULT);
                    FMODAsset asset = ScriptableObject.CreateInstance<FMODAsset>();
                    asset.id = song;
                    songs.Add(asset);
                }
            }
        }
    }
}
