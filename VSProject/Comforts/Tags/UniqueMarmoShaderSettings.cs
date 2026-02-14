using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Tags
{
    internal class UniqueMarmoShaderSettings : MonoBehaviour
    {
        [SerializeField]
        public float shininess;
        [SerializeField]
        public float specularIntensity;
        [SerializeField]
        public float glowStrength;
    }
}
