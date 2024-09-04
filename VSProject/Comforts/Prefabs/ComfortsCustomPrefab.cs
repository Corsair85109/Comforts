using Comforts.Utility;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs
{
    internal class ComfortsCustomPrefab
    {
        public static TechType techType;

        public static GameObject GetGameObject(string classID, GameObject prefabGO, TechType techType)
        {
            PrefabUtils.AddBasicComponents(prefabGO, classID, techType, 0);

            
            if (prefabGO.transform.Find("ConstructBounds") != null)
            {
                BoxCollider boundsCollider = prefabGO.transform.Find("ConstructBounds").GetComponent<BoxCollider>();
                ConstructableBounds constructableBounds = prefabGO.EnsureComponent<ConstructableBounds>();
                constructableBounds.bounds.position = boundsCollider.center;
                constructableBounds.bounds.size = boundsCollider.size;
                boundsCollider.enabled = false;
            }

            ComfortUtils.ApplyMarmosetUBERShader(prefabGO, 10f, 1f, 1f);



            return prefabGO;
        }
    }
}
