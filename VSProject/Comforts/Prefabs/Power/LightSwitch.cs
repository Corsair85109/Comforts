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
    internal class LightSwitch : ComfortsCustomPrefab
    {
        public static TechType techType;

        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("lightSwitch", Language.main.Get("LightSwitch"), Language.main.Get("LightSwitchDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("LightSwitchSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("lightSwitch", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("LightSwitch"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 1)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(1f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.Copper, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.Register();
        }
    }
}
