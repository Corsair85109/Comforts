using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Utility.References
{
    internal class IoncubeReferenceManager
    {
        public static GameObject IoncubeReference { get; private set; }

        private static bool loaded;

        public static IEnumerator EnsureIoncubeReferenceExists()
        {
            if (IoncubeReference != null)
            {
                yield break;
            }

            loaded = false;

            yield return new WaitUntil(() => LightmappedPrefabs.main);

            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.PrecursorIonCrystal);
            yield return task;
            IoncubeReference = task.GetResult();

            loaded = true;
            Logger.Log("Ion cube reference loaded");
        }
    }
}
