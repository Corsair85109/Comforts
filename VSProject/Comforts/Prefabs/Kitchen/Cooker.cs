using Nautilus.Assets.Gadgets;
using Nautilus.Assets;
using Nautilus.Crafting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Nautilus.Utility;

namespace Comforts.Prefabs.Kitchen
{
    internal class Cooker : ComfortsCustomPrefab
    {
        public static CraftTree.Type cooker;
        public static TechType techType;
        public static void Register()
        {
            CustomPrefab customPrefab = new CustomPrefab("cooker", Language.main.Get("Cooker"), Language.main.Get("CookerDesc"), ComfortsPlugin.epicAtlasOfSprites.GetSprite("CookerSprite"));
            techType = customPrefab.Info.TechType;
            customPrefab.SetGameObject(GetGameObject("cooker", ComfortsPlugin.theUltimateBundleOfAssets.LoadAsset<GameObject>("Cooker"), techType));
            RecipeData recipeData = new RecipeData
            {
                craftAmount = 1,
                Ingredients = new List<CraftData.Ingredient>
                {
                    new CraftData.Ingredient(TechType.Titanium, 2),
                    new CraftData.Ingredient(TechType.WiringKit, 1)
                }
            };
            GadgetExtensions.SetRecipe(customPrefab, recipeData).WithCraftingTime(3f);
            GadgetExtensions.SetUnlock(customPrefab, TechType.WiringKit, 1);
            GadgetExtensions.SetPdaGroupCategory(customPrefab, TechGroup.Miscellaneous, TechCategory.Misc);

            // thanks metious!
            customPrefab.CreateFabricator(out var treeType).Root.CraftTreeCreation = () =>
            {
                // get the tab we want to copy
                var craftTreeToYoink = CraftTree.GetTree(CraftTree.Type.Fabricator);
                var foodTab = craftTreeToYoink.nodes.FindNodeById("Survival");

                // Create our craft tree root and add the copied tab to it
                var nodeRoot = new CraftNode("Root").AddNode((CraftNode)foodTab);

                /*
                 * Now we have to create the craft tree. Note that craft trees usually have unique IDs called "scheme" that match their class IDs,
                 * but for the sake of not having to deal with re-registering tab icons, we'll be using the same ID as the craft tree we yoinked from.
                 */
                return new CraftTree(craftTreeToYoink.id, nodeRoot);
            };

            cooker = treeType;

            customPrefab.Register();
        }
    }
}
