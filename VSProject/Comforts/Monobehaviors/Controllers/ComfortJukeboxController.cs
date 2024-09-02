using Comforts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class ComfortJukeboxController : MonoBehaviour
    {
        public FMODAsset currentSong;
        public int currentSongNum = 0;
        public bool playSong = false;

        public void Start()
        {
            ComfortsSpeaker.allJukeboxes.Add(gameObject);
        }

        public void Update()
        {
            currentSong = JukeboxSongs.songs[currentSongNum];
        }

        public void Play()
        {
            playSong = true;
        }

        public void Stop()
        {
            playSong = false;
        }

        public void Skip()
        {
            if (currentSongNum == JukeboxSongs.songs.Count - 1)
            {
                currentSongNum = 0;
            }
            else
            {
                currentSongNum++;
            }
        }

        public void Back()
        {
            if (currentSongNum == 0)
            {
                currentSongNum = JukeboxSongs.songs.Count - 1;
            }
            else
            {
                currentSongNum--;
            }
        }
    }
}
