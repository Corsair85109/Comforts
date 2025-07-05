using Nautilus.Assets;
using Nautilus.Assets.Gadgets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Bedroom.Beds
{
    internal class BlueBed : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("blueBed", Language.main.Get("BlueBed"), Language.main.Get("BlueBedDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("BlueBedSprite"));
            techType = customPrefab.Info.TechType;


            // error line
            customPrefab.SetGameObject(GetGameObject("blueBed", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("BlueBed"), techType, false));



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
            GadgetExtensions.SetUnlock(customPrefab, TechType.Bed1, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
