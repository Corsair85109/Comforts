﻿using Comforts.Utility;
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
        public static GameObject GetGameObject(string classID, GameObject prefabGO, TechType techType, bool addConstructableBounds = true)
        {
            PrefabUtils.AddBasicComponents(prefabGO, classID, techType, 0);
            
            if (addConstructableBounds)
            {
                if (prefabGO.transform.Find("BoundingBox") != null)
                {
                    BoxCollider boundingBox = prefabGO.transform.Find("BoundingBox").GetComponent<BoxCollider>();
                    ConstructableBounds constructableBounds = prefabGO.EnsureComponent<ConstructableBounds>();
                    constructableBounds.bounds.position = boundingBox.center;
                    constructableBounds.bounds.size = boundingBox.size;
                    boundingBox.enabled = false;
                }
                else if (prefabGO.transform.Find("Collider") != null)
                {
                    if (prefabGO.transform.Find("Collider").GetComponent<BoxCollider>() == null)
                    {
                        Utility.Logger.LogError("Collider object had no BoxCollider for ConstrucableBounds on " + classID);
                        Utility.Logger.LogError("If you see the above error, please report it to the developer!");
                    }
                    else
                    {
                        BoxCollider collider = prefabGO.transform.Find("Collider").GetComponent<BoxCollider>();
                        ConstructableBounds constructableBounds = prefabGO.EnsureComponent<ConstructableBounds>();
                        constructableBounds.bounds.position = collider.center;
                        constructableBounds.bounds.size = collider.size;
                    }
                }
            }

            ComfortUtils.ApplyMarmosetUBERShader(prefabGO, 10f, 1f, 1f);

            return prefabGO;
        }
    }
}
