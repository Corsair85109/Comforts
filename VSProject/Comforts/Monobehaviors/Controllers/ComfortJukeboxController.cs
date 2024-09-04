using Comforts.Audio;
using Comforts.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class ComfortJukeboxController : MonoBehaviour
    {
        internal FMODAsset currentSong;
        internal int currentSongNum = 0;
        internal bool playSong;


        public void Start()
        {
            ComfortsSpeaker.allJukeboxes.Add(gameObject);

            ComfortsSpeaker.UpdateJukeboxes();
        }

        public void OnDestroy()
        {
            ComfortsSpeaker.allJukeboxes.Remove(gameObject);

            ComfortsSpeaker.UpdateJukeboxes();
        }

        public void Update()
        {
            currentSong = JukeboxSongs.songs[currentSongNum];
        }

        public void Play()
        {
            playSong = true;
            ComfortsSpeaker.UpdateSpeakers();

            if (PirateChecker.isPirated)
            {
                ComfortUtils.PlayFMODSound("Present", Player.main.transform);
            }
        }

        public void Stop()
        {
            playSong = false;
            ComfortsSpeaker.UpdateSpeakers();
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

            Refresh();
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

            Refresh();
        }

        public void Refresh()
        {
            UWE.CoroutineHost.StartCoroutine(RefreshAllTheShits());
        }

        private IEnumerator RefreshAllTheShits()
        {
            bool wasPlaying = playSong;
            Stop();
            yield return new WaitForEndOfFrame();
            ComfortsSpeaker.UpdateSpeakers();
            if (wasPlaying) Play();
        }
    }
}
