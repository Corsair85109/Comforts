using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class ComfortsClothPhysController : MonoBehaviour
    {
        [SerializeField]
        private Cloth cloth;

        private GameObject capsuleChild;

        public void Start()
        {
            if (ComfortsPlugin.ModConfig.simulateClothPhysics)
            {
                cloth.enabled = true;

                EnsurePlayerCapsuleColliderChild();

                SetCapsuleCollider();
            }
        }

        private void EnsurePlayerCapsuleColliderChild()
        {
            Transform player = Player.main.transform;

            if (player.Find("ComfortsCapsuleCollider(Clone)") == null)
            {
                capsuleChild = ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("ComfortsCapsuleCollider");

                GameObject.Instantiate(capsuleChild, player);
            }
        }

        private void SetCapsuleCollider()
        {
            cloth.capsuleColliders = new CapsuleCollider[1];
            cloth.capsuleColliders[0] = Player.main.transform.Find("ComfortsCapsuleCollider(Clone)").GetComponent<CapsuleCollider>();
        }
    }
}
