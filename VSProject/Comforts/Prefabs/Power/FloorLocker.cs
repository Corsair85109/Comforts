using Comforts.Utility;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Power
{
    internal class FloorLocker : ComfortsCustomPrefab
    {
        // thank you metious and indigocoder for helping me to get the saving to work for this

        public static TechType techType;

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("floorLocker", Language.main.Get("FloorLocker"), Language.main.Get("FloorLockerDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("FloorLockerSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("floorLocker", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("FloorLocker"), techType, true, 5.7f));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.SmallLocker, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.Register();
        }
    }
}
