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
    internal class IonFissionReactor : ComfortsCustomPrefab
    {
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("ionFissionReactor", Language.main.Get("IonFissionReactor"), Language.main.Get("IonFissionReactorDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("IonFissionReactorSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("ionFissionReactor", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("IonFissionReactor"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient(TechType.Titanium, 4),
                    new Ingredient(TechType.Lithium, 2),
                    new Ingredient(TechType.Lead, 4),
                    new Ingredient(TechType.Quartz, 2)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(10f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.PrecursorIonCrystal, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.InteriorModules, TechCategory.InteriorModule);
            customPrefab.Register();
        }
    }
}
