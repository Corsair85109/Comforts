using Comforts.Utility;
using Nautilus.Json;
using Nautilus.Json.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Saving
{
    [FileName("ComfortsSaveData")]
    public class SaveData : SaveDataCache
    {
        // (locker prefabid, list of ItemData)
        public Dictionary<string, List<ItemData>> floorLockerContents;
    }


    public class ItemData
    {
        public TechType itemType;
        public int amount;
        public TechType batteryType;
        public float batteryCharge;
    }


    public partial class SaveHandler
    {
        public static void OnSaveStart(object sender, JsonFileEventArgs args)
        {
            SaveFloorLockers();
            try
            {
                //SaveFloorLockers();
            }
            catch (Exception ex)
            {
                Utility.Logger.Log("Failed to save data with exception:");
                Debug.LogException(ex);
            }
        }

        public static void OnLoadFinish(object sender, JsonFileEventArgs args)
        {
            
        }
    }
}
