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
    internal class IonFusionReactor : ComfortsCustomPrefab
    {
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("ionFusionReactor", Language.main.Get("IonFusionReactor"), Language.main.Get("IonFusionReactorDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("IonFusionReactorSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("ionFusionReactor", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("IonFusionReactor"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                    new CraftData.Ingredient(TechType.Lithium, 2),
                    new CraftData.Ingredient(TechType.Kyanite, 4),
                    new CraftData.Ingredient(TechType.AdvancedWiringKit, 4),
                    new CraftData.Ingredient(TechType.PrecursorIonPowerCell, 4)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(10f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.PrecursorIonPowerCell, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.Register();
        }
    }
}
