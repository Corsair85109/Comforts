using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Power
{
    internal class AnchoredBeacon : ComfortsCustomPrefab
    {
        public static TechType techType;

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("anchoredBeacon", Language.main.Get("AnchoredBeacon"), Language.main.Get("AnchoredBeaconDesc"), SpriteManager.Get(TechType.Beacon));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("anchoredBeacon", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("AnchoredBeacon"), techType, true, 5.7f));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 1),
                    new Ingredient(TechType.Copper, 1)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(1.5f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.Beacon, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.Register();
        }
    }
}
