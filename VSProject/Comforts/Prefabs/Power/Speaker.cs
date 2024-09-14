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
    internal class Speaker : ComfortsCustomPrefab
    {
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("speaker", Language.main.Get("Speaker"), Language.main.Get("SpeakerDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("SpeakerSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("speaker", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("Speaker"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                    new CraftData.Ingredient(TechType.WiringKit, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.WiringKit, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);
            customPrefab.Register();
        }
    }
}
