using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Handtargets
{
    internal class LavaLampHandTarget : HandTarget, IHandTarget
    {
        public Animator animator;
        public GameObject light;

        private bool active = false;

        public void OnHandHover(GUIHand hand)
        {
            string displayString = active ? Language.main.Get("GenericOff") : Language.main.Get("GenericOn");
            HandReticle.main.SetText(HandReticle.TextType.Hand, displayString, true, GameInput.Button.LeftHand);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false, GameInput.Button.None);
            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
        }

        public void OnHandClick(GUIHand hand)
        {
            active = !active;

            UpdateStuff();
        }

        public void Start()
        {
            UpdateStuff();
        }

        private void UpdateStuff()
        {
            animator.SetBool("LampOn", active);
            light.SetActive(active);
        }
    }
}
