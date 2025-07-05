using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors
{
    internal class HealPlayerInCollider : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider collider;

        [SerializeField]
        private float healAmount;

        private LiveMixin playerHealth;
        private ParticleSystem particleSys;

        private int frame = 0;
        private int interval = 10;

        public void Start()
        {
            playerHealth = Player.main.gameObject.GetComponent<LiveMixin>();
            particleSys = transform.Find("ParticleSystem").GetComponent<ParticleSystem>();
        }

        public void FixedUpdate()
        {
            frame ++;

            if (frame >= interval)
            {
                if (IsInsideBoxCollider(Player.main.transform.position + new Vector3(0f, 0.5f, 0f), collider))
                {
                    if (particleSys.isEmitting)
                    {
                        playerHealth.AddHealth(healAmount);
                    }
                }
            }
        }


        private bool IsInsideBoxCollider(Vector3 point, BoxCollider collider)
        {
            // Transform the point into the collider's local space
            Vector3 localPoint = collider.transform.InverseTransformPoint(point);

            // Get the local center and size
            Vector3 center = collider.center;
            Vector3 size = collider.size * 0.5f;

            return (localPoint.x >= center.x - size.x && localPoint.x <= center.x + size.x) &&
                   (localPoint.y >= center.y - size.y && localPoint.y <= center.y + size.y) &&
                   (localPoint.z >= center.z - size.z && localPoint.z <= center.z + size.z);
        }
    }
}
