using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors.Controllers
{
    internal class ComfortsPlayerClothCapsuleController : MonoBehaviour
    {
        private CapsuleCollider collider;

        private Player player;

        public void Start()
        {
            collider = GetComponent<CapsuleCollider>();
            player = Player.main;
        }

        public void Update()
        {
            if (player.IsInSub())
            {
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
        }
    }
}
