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

        public GameObject ionCube;
        public GameObject beam1;
        public GameObject beam2;
        public GameObject beam3;
        public GameObject beam4;
        public GameObject beam5;
        public GameObject beam6;
        public GameObject beam7;
        public GameObject beam8;
        public GameObject beam9;
        public GameObject beam10;
        public GameObject beam11;
        public GameObject beam12;
        public GameObject beam13;
        public GameObject beam14;
        public GameObject beam15;
        public GameObject beam16;

        public Color beamColour;

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

        private EnergyBeamVFX one;
        private EnergyBeamVFX two;
        private EnergyBeamVFX three;
        private EnergyBeamVFX four;
        private EnergyBeamVFX five;
        private EnergyBeamVFX six;
        private EnergyBeamVFX seven;
        private EnergyBeamVFX eight;

        private bool beamsSet = false;

        private float inactiveIllum = 1.4f;
        private float activeIllum = 3f;

        private void StartCrafting()
        {
            progress = 0f;
            progressPercentage = 0f;
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
            // control crafting
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

            // control visuals

            ionCube.SetActive(hasCraftedItem);

            if (isCrafting && !beamsSet)
            {
                SetEnergyBeams(isCrafting);
            }
            else if (!isCrafting && beamsSet)
            {
                SetEnergyBeams(isCrafting);
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

                StartCrafting();
            }
        }

        public void Start()
        {
            one = beam1.EnsureComponent<EnergyBeamVFX>();
            two = beam2.EnsureComponent<EnergyBeamVFX>();
            three = beam3.EnsureComponent<EnergyBeamVFX>();
            four = beam4.EnsureComponent<EnergyBeamVFX>();
            five = beam5.EnsureComponent<EnergyBeamVFX>();
            six = beam6.EnsureComponent<EnergyBeamVFX>();
            seven = beam7.EnsureComponent<EnergyBeamVFX>();
            eight = beam8.EnsureComponent<EnergyBeamVFX>();
        }

        private void SetEnergyBeams(bool active)
        {
            Utility.Logger.Log("Setting grav beam 1");
            one.SetGravityBeam(active ? beam13.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 2");
            two.SetGravityBeam(active ? beam14.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 3");
            three.SetGravityBeam(active? beam15.transform: null, beamColour);
            Utility.Logger.Log("Setting grav beam 4");
            four.SetGravityBeam(active ? beam16.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 5");
            five.SetGravityBeam(active ? beam9.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 6");
            six.SetGravityBeam(active ? beam10.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 7");
            seven.SetGravityBeam(active ? beam11.transform : null, beamColour);
            Utility.Logger.Log("Setting grav beam 8");
            eight.SetGravityBeam(active ? beam12.transform : null, beamColour);

            beamsSet = active;
        }
    }
}
