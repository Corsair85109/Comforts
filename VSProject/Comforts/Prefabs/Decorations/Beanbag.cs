using Comforts.Utility;
using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using Nautilus.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Decorations
{
    internal class Beanbag : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("beanbag", Language.main.Get("Beanbag"), Language.main.Get("BeanbagDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("BeanbagSprite"));
            techType = customPrefab.Info.TechType;

            customPrefab.SetGameObject(GetGameObject("beanbag", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("Beanbag"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.FiberMesh, 2),
                    new CraftData.Ingredient(TechType.CoralChunk, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.FiberMesh, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
