using Comforts.Monobehaviors.Controllers;
using Comforts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors
{
    internal class MusicChipFMODEmitter : FMOD_CustomLoopingEmitter
    {
        private ComfortJukeboxController currentJukebox;

        private GameObject FindNearestJukebox()
        {
            return ComfortUtils.FindNearestGameObjectFromList(transform.position, ComfortsSpeaker.allJukeboxes);
        }

        public override void Start()
        {
            base.Start();

            followParent = true;
        }

        public void Update()
        {
            GameObject near = FindNearestJukebox();
            if (near)
            {
                currentJukebox = near.GetComponent<ComfortJukeboxController>();
            }


            if (currentJukebox == null)
            {
                if (asset) { asset = null; }
                if (playing) { Stop(); }
            }
            else
            {
                if (asset != currentJukebox.currentSong)
                {
                    Stop();
                    asset = currentJukebox.currentSong;
                    Play();
                }

                if (currentJukebox.playSong)
                {
                    if (!playing) { Play(); }
                }
                else
                {
                    if (playing) { Stop(); }
                }
            }
        }
    }
}
