using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using Nautilus.Crafting;
using Nautilus.Utility.MaterialModifiers;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Bathroom
{
    internal class Shower : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("shower", Language.main.Get("Shower"), Language.main.Get("ShowerDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("ShowerSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("shower", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("Shower"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                    new CraftData.Ingredient(TechType.Glass, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.Glass, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
