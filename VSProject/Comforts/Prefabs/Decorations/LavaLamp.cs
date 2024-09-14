using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HandReticle;
using UnityEngine;

namespace Comforts.Prefabs.Decorations
{
    internal class LavaLamp : ComfortsCustomPrefab
    {
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("lavaLamp", Language.main.Get("LavaLamp"), Language.main.Get("LavaLampDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("LavaLampSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("lavaLamp", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("LavaLamp"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 1),
                    new CraftData.Ingredient(TechType.CopperWire, 1)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.CopperWire, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
