using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Comforts.Utility;
using Nautilus.Utility;

namespace Comforts.Prefabs.Power
{
    internal class MusicChip : ComfortsCustomPrefab
    {
        public static TechType techType;

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("musicChip", Language.main.Get("MusicChip"), Language.main.Get("MusicChipDesc"), SpriteManager.Get(TechType.ComputerChip));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("musicChip", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("MusicChip"), techType));
            
            EquipmentGadget equipment = customPrefab.SetEquipment(EquipmentType.Chip);
            
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.ComputerChip, 2),
                    new CraftData.Ingredient(TechType.AdvancedWiringKit, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(2f).WithFabricatorType(CraftTree.Type.Fabricator).WithStepsToFabricatorTab("Personal", "Equipment");
            GadgetExtensions.SetUnlock(customPrefab, TechType.AdvancedWiringKit, 1);
            customPrefab.Register();
        }

        public static GameObject GetGameObject(string classID, GameObject prefabGO, TechType techType)
        {
            PrefabUtils.AddBasicComponents(prefabGO, classID, techType, 0);

            ComfortUtils.ApplyMarmosetUBERShader(prefabGO, 10f, 1f, 1f);

            prefabGO.EnsureComponent<Pickupable>();

            Rigidbody rb = prefabGO.EnsureComponent<Rigidbody>();
            WorldForces wf = prefabGO.EnsureComponent<WorldForces>();
            wf.useRigidbody = rb;
            wf.underwaterGravity = 1f;

            return prefabGO;
        }
    }
}
