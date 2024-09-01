using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class JukeboxUIManager : MonoBehaviour
    {
        private GameObject playButton
        {
            get
            {
                return transform.Find("UI/ButtonPlay").gameObject;
            }
        }
        private GameObject stopButton
        {
            get
            {
                return transform.Find("UI/ButtonStop").gameObject;
            }
        }

        private ComfortJukeboxController controller
        {
            get
            {
                return gameObject.GetComponent<ComfortJukeboxController>();
            }
        }

        internal bool playing = false;

        public void Update()
        {
            playing = controller.playSong;

            if (playing)
            {
                playButton.SetActive(false);
                stopButton.SetActive(true);
            }
            else
            {
                playButton.SetActive(true);
                stopButton.SetActive(false);
            }
        }
    }
}
