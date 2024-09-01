using Comforts.Monobehaviors;
using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Prefabs.Power
{
    internal class Jukebox : ComfortsCustomPrefab
    {
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("jukebox", Language.main.Get("Jukebox"), Language.main.Get("JukeboxDesc"), SpriteManager.Get(TechType.Seamoth));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("jukebox", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("Jukebox"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                    new CraftData.Ingredient(TechType.ComputerChip, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.Titanium, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
