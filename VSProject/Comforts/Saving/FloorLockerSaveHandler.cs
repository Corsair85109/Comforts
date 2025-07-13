using Nautilus.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static HandReticle;

namespace Comforts.Saving
{
    public partial class SaveHandler
    {
        public static List<GameObject> floorLockers = new List<GameObject>();



        public static void SaveFloorLockers()
        {
            if (floorLockers == null)
            {
                Utility.Logger.Log("No floor lockers to save");
                ComfortsPlugin.save.floorLockerContents = new Dictionary<string, List<ItemData>>();
                return;
            }

            Dictionary<string, List<ItemData>> save = new Dictionary<string, List<ItemData>>();

            foreach (GameObject locker in floorLockers)
            {
                PrefabIdentifier prefabIdentifier = locker.GetComponent<PrefabIdentifier>();
                if (prefabIdentifier == null)
                {
                    Utility.Logger.LogWarning("Prefab identifier null for floor locker to save");
                    continue;
                }
                string prefabIdentifierID = prefabIdentifier.id;
                if (string.IsNullOrEmpty(prefabIdentifierID))
                {
                    Utility.Logger.LogWarning("PrefabIdentifier id is null or empty for floor locker to save");
                    continue;
                }


                Utility.Logger.Log($"Getting data for floor locker with id: {prefabIdentifierID}");


                List<ItemData> contents = new List<ItemData>();

                Transform storageRoot;
                if (locker.GetComponent<StorageContainer>() == null)
                {
                    Utility.Logger.LogError("Null StorageContainer");
                    continue;
                }
                else if (locker.GetComponent<StorageContainer>().storageRoot == null)
                {
                    Utility.Logger.LogError("Null StorageContainer.storageRoot");
                    continue;
                }
                else
                {
                    storageRoot = locker.GetComponent<StorageContainer>().storageRoot.transform;
                }

                foreach (Transform child in storageRoot)
                {
                    Pickupable pickupable = child.GetComponent<Pickupable>();
                    if (pickupable == null)
                    {
                        Utility.Logger.LogError("Null pickupable");
                        continue;
                    }

                    TechType techType = pickupable.GetTechType();
                    ItemData data = new ItemData
                    {
                        itemType = techType,
                        amount = 1,
                        batteryType = TechType.None,
                        batteryCharge = 0f
                    };

                    EnergyMixin energyMixin = child.GetComponent<EnergyMixin>();
                    if (energyMixin != null)
                    {
                        data.batteryCharge = energyMixin.charge;
                        data.batteryType = energyMixin.GetBatteryGameObject().GetComponent<Pickupable>().GetTechType();
                    }

                    contents.Add(data);
                }

                save.Add(prefabIdentifierID, contents);
            }
            ComfortsPlugin.save.floorLockerContents = save;
        }

        // TODO get amount working


        // called in the gameobject
        public static void LoadFloorLocker(GameObject obj)
        {
            // if new game
            if (ComfortsPlugin.save == null)
            {
                return;
            }
            if (ComfortsPlugin.save.floorLockerContents == null)
            {
                return;
            }


            PrefabIdentifier prefabIdentifier = obj.GetComponent<PrefabIdentifier>();
            if (prefabIdentifier == null)
            {
                Utility.Logger.LogWarning("Prefab identifier null for floor locker to save");
                return;
            }
            string prefabIdentifierID = prefabIdentifier.id;
            if (string.IsNullOrEmpty(prefabIdentifierID))
            {
                Utility.Logger.LogWarning("PrefabIdentifier id is null or empty for floor locker to save");
                return;
            }


            Utility.Logger.Log($"Loading data for floor locker with id: {prefabIdentifierID}");

            StorageContainer container = obj.GetComponent<StorageContainer>();

            foreach (var item in ComfortsPlugin.save.floorLockerContents)
            {
                Utility.Logger.Log($"{item.Key}:");

                if (item.Key != prefabIdentifierID)
                {
                    continue;
                }

                foreach (ItemData data in item.Value)
                {
                    Utility.Logger.Log(data.itemType.ToString());
                }

                UWE.CoroutineHost.StartCoroutine(AddContents(item.Value, container));
            }

        }

        public static IEnumerator AddContents(List<ItemData> contents, StorageContainer container)
        {
            foreach (ItemData data in contents)
            {
                TechType tt = data.itemType;
                int amount = data.amount;
                TechType batteryType = data.batteryType;
                float charge = data.batteryCharge;


                Utility.Logger.Log($"Getting prefab for {tt}");
                CoroutineTask<GameObject> task = CraftData.GetPrefabForTechTypeAsync(tt);
                yield return task;
                GameObject prefab = task.GetResult();
                if (prefab == null)
                {
                    Utility.Logger.LogError("When you load into a save the second time since launching, for some reason CraftData.GetPrefabForTechTypeAsync stops working");
                    Utility.Logger.LogError("I will now crash your game to protect your save file from harm");
                    Application.Quit();
                    break;
                }


                InventoryItem item = container.container.AddItem(prefab.GetComponent<Pickupable>());

                if (batteryType != TechType.None)
                {
                    Utility.Logger.Log("Item has battery and energy mixin");

                    EnergyMixin energyMixin = item.item.gameObject.GetComponent<EnergyMixin>();

                    TaskResult<InventoryItem> task2 = new TaskResult<InventoryItem>();
                    Utility.Logger.Log("Trying...");

                    UWE.CoroutineHost.StartCoroutine(energyMixin.SetBatteryAsync(batteryType, charge, task2));

                    
                }


            }
        }


        
    }
}
