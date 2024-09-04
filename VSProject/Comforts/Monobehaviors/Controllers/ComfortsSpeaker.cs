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
    public class ComfortsSpeaker : MonoBehaviour
    {
        public static List<GameObject> allJukeboxes = new List<GameObject>();
        public static List<GameObject> allSpeakers = new List<GameObject>();

        private ComfortJukeboxController currentJukebox;
        private FMOD_CustomLoopingEmitter fmodEmitter
        {
            get
            {
                return gameObject.GetComponent<FMOD_CustomLoopingEmitter>();
            }
        }

        public static void UpdateJukeboxes()
        {
            if (allJukeboxes.Count <= 1) return;

            foreach (GameObject jukebox in allJukeboxes)
            {
                ComfortJukeboxController controller = jukebox.GetComponent<ComfortJukeboxController>();

                controller.Refresh();
            }
        }

        public static void UpdateSpeakers()
        {
            if (allSpeakers.Count <= 1) return;

            foreach (GameObject speaker in allSpeakers)
            {
                ComfortsSpeaker component = speaker.GetComponent<ComfortsSpeaker>();

                component.SetUp();
            }
        }

        public void Start()
        {
            allSpeakers.Add(gameObject);

            UpdateJukeboxes();
        }

        public void OnDestroy()
        {
            allSpeakers.Remove(gameObject);

            UpdateJukeboxes();
        }

        public void SetUp()
        {
            fmodEmitter.Stop();

            currentJukebox = FindNearestJukebox(transform.position).GetComponent<ComfortJukeboxController>();

            if (currentJukebox != null)
            {
                fmodEmitter.asset = currentJukebox.currentSong;
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

        public void Update()
        {
            if (currentJukebox == null || PirateChecker.isPirated)
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
                if (fmodEmitter.asset != currentJukebox.currentSong)
                {
                    fmodEmitter.asset = currentJukebox.currentSong;
                }
                if (currentJukebox.playSong)
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
