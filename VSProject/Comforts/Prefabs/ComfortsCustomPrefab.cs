using Comforts.Utils;
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
            float num = 1f;
            prefabGO.transform.localScale = new Vector3(num, num, num);
            ComfortUtils.ApplyMarmosetUBERShader(prefabGO, 10f, 1f, 1f);
            return prefabGO;
        }
    }
}
