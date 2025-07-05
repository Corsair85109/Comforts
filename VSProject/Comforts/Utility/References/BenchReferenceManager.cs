using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Utility.References
{
    internal class BenchReferenceManager
    {
        public static GameObject BenchReference {  get; private set; }

        private static bool loaded;

        public static IEnumerator EnsureBenchReferenceExists()
        {
            if (BenchReference != null)
            {
                yield break;
            }

            loaded = false;

            yield return new WaitUntil(() => LightmappedPrefabs.main);

            CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(TechType.Bench);
            yield return task;
            BenchReference = task.GetResult();

            loaded = true;
            Logger.Log("Bench reference loaded");
        }
    }
}
