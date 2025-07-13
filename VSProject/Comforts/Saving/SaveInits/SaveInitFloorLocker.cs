using Comforts.Prefabs.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Saving.SaveInits
{
    internal class SaveInitFloorLocker : MonoBehaviour
    {
        public void Start()
        {
            SaveHandler.floorLockers.Add(gameObject);

            SaveHandler.LoadFloorLocker(gameObject);
        }
    }
}
