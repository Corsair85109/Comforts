using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Handtargets
{
    internal class LightSwitchHandTarget : HandTarget, IHandTarget
    {
        private SubRoot _subRoot = null;
        internal SubRoot SubRoot
        {
            get
            {
                if (_subRoot == null)
                {
                    transform.parent.TryGetComponent(out _subRoot);
                }
                return _subRoot;
            }
        }

        public virtual void OnHandHover(GUIHand hand)
        {
            string displayString = Language.main.Get("GenericUse");
            HandReticle.main.SetText(HandReticle.TextType.Hand, displayString, true, GameInput.Button.LeftHand);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, string.Empty, false, GameInput.Button.None);
            HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
        }
        public virtual void OnHandClick(GUIHand hand)
        {
            SubRoot.ForceLightingState(!SubRoot.subLightsOn);
        }
    }
}
