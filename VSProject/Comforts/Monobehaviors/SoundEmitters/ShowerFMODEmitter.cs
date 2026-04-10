using Comforts.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comforts.Monobehaviors.SoundEmitters
{
    internal class ShowerFMODEmitter : FMOD_CustomEmitter
    {
        public override void Start()
        {
            base.Start();

            asset = ComfortsFMODAssets.shower;
        }
    }
}
