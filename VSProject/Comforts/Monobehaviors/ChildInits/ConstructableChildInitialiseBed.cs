using Comforts.Utility.References;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.ChildInits
{
    internal class ConstructableChildInitialiseBed : MonoBehaviour
    {
        [SerializeField]
        private bool skipModel = true;

        [SerializeField]
        private GameObject modelReplacement;

        [SerializeField]
        private bool skipCollider = true;

        [SerializeField]
        private Bed goBed;



        public void Start()
        {
            goBed.enabled = false;
            UWE.CoroutineHost.StartCoroutine(AddChildren());
        }



        private IEnumerator AddChildren()
        {
            yield return BedReferenceManager.EnsureBedReferencesExist();

            GameObject bed = BedReferenceManager.BedReference;

            // Make sure the prefab is instantiated, not just accessed directly from assets
            GameObject bedInstance = GameObject.Instantiate(bed);

            // Copy bed child
            GameObject clone = GameObject.Instantiate(bedInstance.transform.Find("bed_01").gameObject);
            clone.transform.SetParent(gameObject.transform, false);
            clone.name = "bed_01"; // remove (Clone)

            // Replace model
            GameObject.DestroyImmediate(clone.transform.Find("bed").gameObject);
            modelReplacement.transform.SetParent(clone.transform, false);



            // Setup stuff on prefab gameobject
            Bed prefabBed = gameObject.EnsureComponent<Bed>();

            //prefabBed = bedInstance.GetComponent<Bed>();

            prefabBed.animator = prefabBed.transform.Find("bed_01").GetComponent<Animator>();
            prefabBed.playerTarget = prefabBed.transform.Find("bed_01/root/player_cineLoc");
            prefabBed.leftObstacleCheck = prefabBed.transform.Find("bed_01/obstacle_check/left").gameObject;
            prefabBed.rightObstacleCheck = prefabBed.transform.Find("bed_01/obstacle_check/right").gameObject;

            PlayerCinematicController[] playerCineControllers = prefabBed.transform.Find("bed_01").GetComponents<PlayerCinematicController>();

            foreach (PlayerCinematicController cont in playerCineControllers)
            {
                switch (cont.playerViewAnimationName)
                {
                    case "bed_down_left":
                        prefabBed.leftLieDownCinematicController = cont;
                        break;

                    case "bed_down_right":
                        prefabBed.rightLieDownCinematicController = cont;
                        break;

                    case "bed_up_left":
                        prefabBed.leftStandUpCinematicController = cont;
                        break;

                    case "bed_up_right":
                        prefabBed.rightStandUpCinematicController = cont;
                        break;
                }
            }






            // Clean up the temporary clone
            GameObject.Destroy(bedInstance);

            goBed.enabled = true;
        }
    }
}
