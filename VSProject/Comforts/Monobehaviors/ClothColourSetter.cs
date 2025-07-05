using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors
{
    internal class ClothColourSetter : MonoBehaviour
    {
        public static Dictionary<string, Color> clothColours = new Dictionary<string, Color>
        {
            {"red", new Color(0.83f, 0f, 0f)},
            {"blue", new Color(0f, 0.57f, 1f)},
            {"green", new Color(0.027f, 0.665f, 0.027f)}
        };
        public enum clothColour
        {
            red,
            blue,
            green
        }

        [SerializeField]
        private Renderer renderer;

        [SerializeField]
        internal clothColour colour;




        public void Awake()
        {
            if (renderer != null)
            {
                renderer.material.color = clothColours[colour.ToString()];
            }
        }
    }
}
