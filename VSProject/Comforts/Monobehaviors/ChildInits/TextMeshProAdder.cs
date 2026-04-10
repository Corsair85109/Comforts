using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Comforts.Monobehaviors.ChildInits
{
    internal class TextMeshProAdder : MonoBehaviour
    {

        private TextMeshPro tmp;

        public void Start()
        {
            tmp = gameObject.AddComponent<TextMeshPro>();
        }
    }
}
