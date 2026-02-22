using Comforts.Monobehaviors.Handtargets;
using Comforts.Utility;
using Comforts.Utility.References;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;
using Logger = Comforts.Utility.Logger;

namespace Comforts.Monobehaviors.ChildInits
{
    internal class IonFusionReactorChildInit : MonoBehaviour
    {
        [SerializeField]
        MeshRenderer rendererWithTheGlowMaterial;

        public bool constructed = false;

        private bool spawnedChild = false;

        public void Start()
        {
            transform.localScale = Vector3.one;
            Destroy(gameObject.GetComponent<MeshFilter>());
            Destroy(gameObject.GetComponent<MeshRenderer>());

            constructed = false;
            spawnedChild = false;

            CoroutineHost.StartCoroutine(AddChild());
        }

        private IEnumerator AddChild()
        {
            yield return IoncubeReferenceManager.EnsureIoncubeReferenceExists();

            while (!constructed) yield return null;

            GameObject ioncube = IoncubeReferenceManager.IoncubeReference.transform.Find("Mesh").gameObject;
            Material glowMaterial = ioncube.GetComponent<MeshRenderer>().material;

            Material[] materials = rendererWithTheGlowMaterial.materials;
            materials[0] = glowMaterial;
            materials[2] = glowMaterial;

            rendererWithTheGlowMaterial.materials = materials;

            if (spawnedChild) yield break;
            
            GameObject clone = Instantiate(ioncube, transform);
            clone.transform.localPosition = new Vector3(0f, -0.2f, 0f);
            clone.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

            spawnedChild = true;
        }
    }
}
