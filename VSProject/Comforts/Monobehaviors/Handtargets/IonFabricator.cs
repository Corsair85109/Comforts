using Comforts.Utility;
using Discord;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static HandReticle;

namespace Comforts.Monobehaviors.Handtargets
{
    internal class IonFabricator : HandTarget, IHandTarget
    {
        public FMOD_CustomLoopingEmitter soundEmitter;
        public PowerConsumer powerConsumer;

        private float progressPercentage = 0f;
        private float progress = 0f;
        private bool isCrafting = false;
        private bool hasCraftedItem = false;
        private bool enoughPower = false;

        private readonly TechType craftingTechType = TechType.PrecursorIonCrystal;
        private readonly float timeToCraft = 60f;
        private readonly float energyCost = 1000f;
        private readonly float powerEndThreshhold = 100f;
        
        private bool pickingUp = false;

        private void StartCrafting()
        {
            hasCraftedItem = false;

            isCrafting = true;
        }

        private void EndCrafting()
        {
            isCrafting = false;

            hasCraftedItem = true;
        }


        public void Update()
        {
            if (isCrafting)
            {
                if (progress >= timeToCraft)
                {
                    EndCrafting();
                }
                else
                {
                    float powerToConsume = energyCost / timeToCraft * Time.deltaTime;
                    if (powerConsumer.powerRelay.GetPower() >= powerToConsume + powerEndThreshhold || !GameModeUtils.RequiresPower())
                    {
                        enoughPower = true;
                        powerConsumer.ConsumePower(powerToConsume, out float consumed);
                        progress += Time.deltaTime;
                    }
                    else
                    {
                        enoughPower = false;
                    }
                    progressPercentage = progress / timeToCraft * 100;
                }


            }
        }

        public void OnHandHover(GUIHand hand)
        {
            string text = craftingTechType.AsString(false);
            string text2 = string.Empty;

            if (hasCraftedItem)
            {
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            else if (isCrafting)
            {
                if (!enoughPower)
                {
                    text = Language.main.Get("IonFabricatorNoPower");
                }
                HandReticle.main.SetProgress(progressPercentage / 100);
                HandReticle.main.SetIcon(HandReticle.IconType.Progress, 1.5f);
            }
            else
            {
                text = Language.main.Get("IonFabricatorStart");
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            HandReticle.main.SetText(HandReticle.TextType.Hand, text, true, GameInput.Button.LeftHand);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, text2, true, GameInput.Button.None);
        }
        public void OnHandClick(GUIHand hand)
        {
            if (hasCraftedItem)
            {
                TryPickup();
            }
            else if (!isCrafting)
            {
                StartCrafting();
            }
        }

        private void TryPickup()
        {
            if (!pickingUp)
            {
                pickingUp = true;

                TaskResult<bool> result = new TaskResult<bool>();
                UWE.CoroutineHost.StartCoroutine(ComfortUtils.TryPickupAsync(craftingTechType, result));

                pickingUp = false;

                Utility.Logger.Log("Pickup result: " + result.value.ToString());
                if (result.value)
                {
                    StartCrafting();
                }
            }
        }
    }
}
