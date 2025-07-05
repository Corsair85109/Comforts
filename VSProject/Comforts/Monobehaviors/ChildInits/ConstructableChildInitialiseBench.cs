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
    internal class ConstructableChildInitialiseBench : MonoBehaviour
    {
        [SerializeField]
        private bool skipModel = true;

        [SerializeField]
        private bool skipCollider = true;

        [SerializeField]
        private Bench goBench;



        public void Start()
        {
            goBench.enabled = false;
            UWE.CoroutineHost.StartCoroutine(AddChildren());
        }



        private IEnumerator AddChildren()
        {
            yield return BenchReferenceManager.EnsureBenchReferenceExists();

            GameObject bench = BenchReferenceManager.BenchReference;

            // Make sure the prefab is instantiated, not just accessed directly from assets
            GameObject benchInstance = GameObject.Instantiate(bench);

            foreach (Transform child in benchInstance.transform)
            {
                if (child.name.ToLower() == "model") continue;

                if (child.name.ToLower() == "collider") continue;


                if (child.name != "model" && child.name != "Collider")
                {
                    // Clone the GameObject (not just the Transform), and parent it to your beanbag
                    GameObject clone = GameObject.Instantiate(child.gameObject);
                    clone.transform.SetParent(gameObject.transform, false);
                    clone.name = child.name; // remove (Clone)
                }
            }

            // Clean up the temporary clone
            GameObject.Destroy(benchInstance);



            // Setup stuff on prefab gameobject
            Bench gameObjectBench = gameObject.EnsureComponent<Bench>();

            gameObjectBench.animator = gameObjectBench.transform.Find("bench_animation").GetComponent<Animator>();
            gameObjectBench.playerTarget = gameObjectBench.transform.Find("bench_animation/root/cine_loc/player_target");
            gameObjectBench.frontObstacleCheck = gameObjectBench.transform.Find("obstacleCheck/front").gameObject;
            gameObjectBench.backObstacleCheck = gameObjectBench.transform.Find("obstacleCheck/back").gameObject;

            PlayerCinematicController[] playerCineControllers = gameObjectBench.transform.Find("bench_animation").GetComponents<PlayerCinematicController>();
            foreach (PlayerCinematicController cont in playerCineControllers)
            {
                if (cont.playerViewAnimationName == "bench_stand_up")
                {
                    gameObjectBench.standUpCinematicController = cont;
                }
                else if (cont.playerViewAnimationName == "bench_sit")
                {
                    gameObjectBench.cinematicController = cont;
                }
            }

            goBench.enabled = true;
        }
    }
}
