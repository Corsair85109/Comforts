using Comforts.Monobehaviors.Controllers;
using Comforts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Handtargets
{
    internal class JukeboxUIHandtarget : HandTarget, IHandTarget
    {
        internal string useKey = "GenericUse";
        internal ComfortJukeboxController jukebox
        {
            get
            {
                return transform.parent.parent.gameObject.GetComponent<ComfortJukeboxController>();
            }
        }
        public virtual void OnHandHover(GUIHand hand)
        {
            string displayString = Language.main.Get(useKey);
            HandReticle.main.SetText(HandReticle.TextType.Hand, displayString, true, GameInput.Button.LeftHand);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false, GameInput.Button.None);
            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
        }
        public virtual void OnHandClick(GUIHand hand) { }
    }




    internal class JukeboxUIPlayHandTarget : JukeboxUIHandtarget
    {
        public override void Awake()
        {
            base.Awake();

            useKey = "JukeboxUIPlay";
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Play();
        }
    }

    internal class JukeboxUIStopHandTarget : JukeboxUIHandtarget
    {
        public override void Awake()
        {
            base.Awake();

            useKey = "JukeboxUIStop";
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Stop();
        }
    }

    internal class JukeboxUISkipHandTarget : JukeboxUIHandtarget
    {
        public override void Awake()
        {
            base.Awake();

            useKey = "JukeboxUISkip";
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Skip();
        }
    }

    internal class JukeboxUIBackHandTarget : JukeboxUIHandtarget
    {
        public override void Awake()
        {
            base.Awake();

            useKey = "JukeboxUIBack";
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Back();
        }
    }
}
