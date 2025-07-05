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
using Comforts.Utility;
using Comforts.Monobehaviors;

namespace Comforts.Prefabs.Decorations.Curtains
{
    internal class BlueCurtain : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("blueCurtain", Language.main.Get("BlueCurtain"), Language.main.Get("BlueCurtainDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("BlueCurtainSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("blueCurtain", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("BlueCurtain"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 1)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.CopperWire, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
