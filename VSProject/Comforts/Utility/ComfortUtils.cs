using Comforts.Tags;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UWE;
using static Nautilus.Utility.MaterialUtils;

namespace Comforts.Utility
{
    internal class ComfortUtils : MonoBehaviour
    {
        public static void NautilusBasicText(string msg, float time)
        {
            Nautilus.Utility.BasicText message = new Nautilus.Utility.BasicText(500, 0);
            message.ShowMessage(msg, time * Time.deltaTime);
        }

        public static void ApplyMarmosetUBERShader(GameObject gameObject, float shininess, float specularIntensity, float glowStrength)
        {
            // edited from nautilus ApplySNShaders
            var renderers = gameObject.GetComponentsInChildren<Renderer>(true);
            for (var i = 0; i < renderers.Length; i++)
            {
                for (var j = 0; j < renderers[i].materials.Length; j++)
                {
                    var material = renderers[i].materials[j];

                    var matNameLower = material.name.ToLower();
                    bool transparent = matNameLower.Contains("transparent");
                    bool alphaClip = matNameLower.Contains("cutout");

                    var materialType = MaterialType.Opaque;
                    if (transparent)
                        materialType = MaterialType.Transparent;
                    else if (alphaClip)
                        materialType = MaterialType.Cutout;

                    bool blockShaderConversion = false;

                    // stop some things getting marmo
                    if (renderers[i].gameObject.GetComponent<StopMarmoShader>() != null)
                    {
                        blockShaderConversion = true;
                    }

                    if (!blockShaderConversion)
                    {
                        ApplyUBERShader(material, shininess, specularIntensity, glowStrength, materialType);
                    }
                }
            }
        }

        public static void PlayFMODSound(string soundName, Transform position)
        {
            var asset = Nautilus.Utility.AudioUtils.GetFmodAsset(soundName);
            Utils.PlayFMODAsset(asset, position);
        }

        internal static IEnumerator TryPickupAsync(TechType techType, IOut<bool> result)
        {
            if (techType == TechType.None)
            {
                yield break;
            }

            Inventory inventory = Inventory.main;
            bool overrideTech = false;
            CoroutineTask<GameObject> request = CraftData.GetPrefabForTechTypeAsync(techType, true);
            yield return request;
            GameObject gameObject = request.GetResult();
            if (gameObject == null)
            {
                gameObject = Utils.genericLootPrefab;
                overrideTech = true;
            }
            if (gameObject != null)
            {
                Pickupable component = gameObject.GetComponent<Pickupable>();
                if (component != null)
                {
                    Vector2int itemSize = CraftData.GetItemSize(component.GetTechType());
                    if (inventory.HasRoomFor(itemSize.x, itemSize.y))
                    {
                        GameObject gameObject2 = Instantiate(gameObject);
                        component = gameObject2.GetComponent<Pickupable>();
                        if (overrideTech)
                        {
                            component.SetTechTypeOverride(techType, true);
                        }
                        CrafterLogic.NotifyCraftEnd(gameObject2, techType);
                        inventory.ForcePickup(component);
                        Player.main.PlayGrab();
                        result.Set(true);
                    }
                    else
                    {
                        ErrorMessage.AddMessage(Language.main.Get("InventoryFull"));
                        result.Set(false);
                    }
                }
                else
                {
                    Debug.LogErrorFormat("Can't find Pickupable component on prefab for TechType.{0}", new object[]
                    {
                    techType
                    });
                    result.Set(true);
                }
            }
            else
            {
                Debug.LogErrorFormat("Can't find prefab for TechType.{0}", new object[]
                {
                techType
                });
                result.Set(true);
            }

            yield break;
        }
    }
}
