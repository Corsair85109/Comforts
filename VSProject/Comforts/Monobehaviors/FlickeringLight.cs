using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors
{
    internal class FlickeringLight : MonoBehaviour
    {
        private Light light;

        [SerializeField]
        private float minIntensity;
        [SerializeField]
        private float maxIntensity;

        [SerializeField]
        private float speed;

        public void Start()
        {
            light = GetComponent<Light>();
        }

        public void Update()
        {
            float noise = Mathf.PerlinNoise(Time.time * speed, 0.0f);
            light.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }
}
