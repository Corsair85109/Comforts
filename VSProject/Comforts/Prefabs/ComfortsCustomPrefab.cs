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
            
            if (prefabGO.transform.Find("BoundingBox") != null)
            {
                BoxCollider boundingBox = prefabGO.transform.Find("BoundingBox").GetComponent<BoxCollider>();
                ConstructableBounds constructableBounds = prefabGO.EnsureComponent<ConstructableBounds>();
                constructableBounds.bounds.position = boundingBox.center;
                constructableBounds.bounds.size = boundingBox.size;
                boundingBox.enabled = false;
            }
            else if (prefabGO.transform.Find("Collider").GetComponent<BoxCollider>() != null)
            {
                BoxCollider collider = prefabGO.transform.Find("Collider").GetComponent<BoxCollider>();
                ConstructableBounds constructableBounds = prefabGO.EnsureComponent<ConstructableBounds>();
                constructableBounds.bounds.position = collider.center;
                constructableBounds.bounds.size = collider.size;
            }

            ComfortUtils.ApplyMarmosetUBERShader(prefabGO, 10f, 1f, 1f);

            return prefabGO;
        }
    }
}
