using Comforts.Monobehaviors;
using Comforts.Prefabs.Power;
using Comforts.Utility;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Patches
{
    [HarmonyPatch(typeof(Equipment))]
    internal class MusicChipPatch
    {
        [HarmonyPatch(nameof(Equipment.NotifyEquip))]
        [HarmonyPostfix]
        public static void NotifyEquip_Postfix(Equipment __instance, string slot, InventoryItem item)
        {
            if (__instance != Inventory.main.equipment)
            {
                Utility.Logger.Log("YEET");
                return;
            }
            if (item.techType == MusicChip.techType)
            {
                Utility.Logger.Log("Adding music chip FMODemitter to player");
                Player.main.gameObject.EnsureComponent<MusicChipFMODEmitter>();
            }
        }

        [HarmonyPatch(nameof(Equipment.NotifyUnequip))]
        [HarmonyPostfix]
        public static void NotifyUnequip_Postfix(Equipment __instance, string slot, InventoryItem item)
        {
            if (__instance != Inventory.main.equipment)
            {
                Utility.Logger.Log("YEET");
                return;
            }
            if (item.techType == MusicChip.techType)
            {
                Utility.Logger.Log("Removing music chip fmodemitter from player");
                if (Player.main.gameObject.TryGetComponent<MusicChipFMODEmitter>(out var comp))
                {
                    GameObject.Destroy(comp);
                }
            }
        }
    }
}
