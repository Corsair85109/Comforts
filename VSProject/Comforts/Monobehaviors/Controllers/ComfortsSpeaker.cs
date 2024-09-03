using Comforts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    public class ComfortsSpeaker : MonoBehaviour
    {
        public static List<GameObject> allJukeboxes = new List<GameObject>();

        private GameObject currentJukebox;
        private FMOD_CustomLoopingEmitter fmodEmitter
        {
            get
            {
                return gameObject.GetComponent<FMOD_CustomLoopingEmitter>();
            }
        }

        private GameObject FindNearestJukebox(Vector3 position)
        {
            float ComputeDistance(GameObject thing)
            {
                return Vector3.Distance(position, thing.transform.position);
            }
            GameObject nearestJukebox = null;
            foreach (GameObject jukebox in allJukeboxes)
            {
                if (nearestJukebox == null || (ComputeDistance(jukebox) < ComputeDistance(nearestJukebox)))
                {
                    nearestJukebox = jukebox;
                }
            }

            return nearestJukebox;
        }

        public void Start()
        {
            currentJukebox = FindNearestJukebox(transform.position);
        }

        public void Update()
        {
            if (currentJukebox == null)
            {
                if (fmodEmitter.asset != null)
                {
                    fmodEmitter.asset = null;
                }
                if (fmodEmitter.playing)
                {
                    fmodEmitter.Stop();
                }
            }
            else
            {
                var jukeboxController = currentJukebox.GetComponent<ComfortJukeboxController>();

                if (fmodEmitter.asset == null)
                {
                    fmodEmitter.asset = jukeboxController.currentSong;
                }
                if (jukeboxController.playSong)
                {
                    if (!fmodEmitter.playing)
                    {
                        fmodEmitter.Play();
                    }
                }
                else
                {
                    if (fmodEmitter.playing)
                    {
                        fmodEmitter.Stop();
                    }
                }
            }
        }
    }
}
