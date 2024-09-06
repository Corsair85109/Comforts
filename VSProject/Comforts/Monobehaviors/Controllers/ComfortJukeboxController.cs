using Comforts.Audio;
using Comforts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class ComfortJukeboxController : MonoBehaviour
    {
        internal FMODAsset currentSong;
        internal int currentSongNum = 0;
        internal bool playSong;
        private TMP_Text songNameText
        {
            get
            {
                return transform.Find("UI/SongNameText").GetComponent<TMP_Text>();
            }
        }

        public void Start()
        {
            ComfortsSpeaker.allJukeboxes.Add(gameObject);

            ComfortsSpeaker.UpdateMusicConstructables();
        }

        public void OnDestroy()
        {
            ComfortsSpeaker.allJukeboxes.Remove(gameObject);

            ComfortsSpeaker.UpdateMusicConstructables();
        }

        public void Update()
        {
            if (PirateChecker.isPirated)
            {
                currentSong = AudioRegistrar.presentAsset;
            }
            else
            {
                currentSong = JukeboxSongs.songs[currentSongNum];
            }
            songNameText.SetText(currentSong.id);
        }

        public void Play()
        {
            playSong = true;

            ComfortsSpeaker.UpdateMusicConstructables();
        }

        public void Stop()
        {
            playSong = false;

            ComfortsSpeaker.UpdateMusicConstructables();
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

            ComfortsSpeaker.UpdateMusicConstructables();
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

            ComfortsSpeaker.UpdateMusicConstructables();
        }
    }
}
