using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Utility.References
{
    internal class BedReferenceManager
    {
        public static GameObject BedReference { get; private set; }
        public static GameObject BedNarrowReference { get; private set; }

        private static bool loaded;

        public static IEnumerator EnsureBedReferencesExist()
        {
            if (BedReference != null)
            {
                yield break;
            }

            loaded = false;

            yield return new WaitUntil(() => LightmappedPrefabs.main);

            CoroutineTask<GameObject> task1 = CraftData.GetPrefabForTechTypeAsync(TechType.Bed1);
            CoroutineTask<GameObject> task2 = CraftData.GetPrefabForTechTypeAsync(TechType.NarrowBed);
            yield return task1;
            yield return task2;
            BedReference = task1.GetResult();
            BedNarrowReference = task2.GetResult();

            loaded = true;
            Logger.Log("Bed references loaded");
        }
    }
}
