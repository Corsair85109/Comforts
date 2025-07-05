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
    internal class GreenCurtain : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("greenCurtain", Language.main.Get("GreenCurtain"), Language.main.Get("GreenCurtainDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("GreenCurtainSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("greenCurtain", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("GreenCurtain"), techType));
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
