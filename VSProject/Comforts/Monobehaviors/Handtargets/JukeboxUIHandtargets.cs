using Comforts.Monobehaviors.Controllers;
using Comforts.Utility;
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
        internal virtual string useKey
        {
            get
            {
                return "GenericUse";
            }
        }
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
        internal override string useKey
        {
            get
            {
                return "JukeboxUIPlay";
            }
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Play();
        }
    }

    internal class JukeboxUIStopHandTarget : JukeboxUIHandtarget
    {
        internal override string useKey
        {
            get
            {
                return "JukeboxUIStop";
            }
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Stop();
        }
    }

    internal class JukeboxUISkipHandTarget : JukeboxUIHandtarget
    {
        internal override string useKey
        {
            get
            {
                return "JukeboxUISkip";
            }
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Skip();
        }
    }

    internal class JukeboxUIBackHandTarget : JukeboxUIHandtarget
    {
        internal override string useKey
        {
            get
            {
                return "JukeboxUIBack";
            }
        }

        public override void OnHandClick(GUIHand hand)
        {
            jukebox.Back();
        }
    }
}
