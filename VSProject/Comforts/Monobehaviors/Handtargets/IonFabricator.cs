using Comforts.Utility;
using Discord;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static HandReticle;

namespace Comforts.Monobehaviors.Handtargets
{
    internal class IonFabricator : HandTarget, IHandTarget
    {
        [SerializeField]
        private FMOD_CustomLoopingEmitter soundEmitter;
        [SerializeField]
        private PowerConsumer powerConsumer;

        [SerializeField]
        private GameObject ionCube;

        [SerializeField]
        private GameObject beam1;
        [SerializeField]
        private GameObject beam2;
        [SerializeField]
        private GameObject beam3;
        [SerializeField]
        private GameObject beam4;
        [SerializeField]
        private GameObject beam5;
        [SerializeField]
        private GameObject beam6;
        [SerializeField]
        private GameObject beam7;
        [SerializeField]
        private GameObject beam8;
        [SerializeField]
        private GameObject beam9;
        [SerializeField]
        private GameObject beam10;
        [SerializeField]
        private GameObject beam11;
        [SerializeField]
        private GameObject beam12;
        [SerializeField]
        private GameObject beam13;
        [SerializeField]
        private GameObject beam14;
        [SerializeField]
        private GameObject beam15;
        [SerializeField]
        private GameObject beam16;

        [SerializeField]
        private Color beamColour;

        private float progressPercentage = 0f;
        private float progress = 0f;
        private bool isCrafting = false;
        private bool hasCraftedItem = false;
        private bool enoughPower = false;

        private bool requirePower = true;

        private int frameCounter = 0;

        [SerializeField]
        private readonly TechType craftingTechType = TechType.PrecursorIonCrystal;
        [SerializeField]
        private float timeToCraft = 60f;
        [SerializeField]
        private float energyCost = 1000f;
        [SerializeField]
        private float powerEndThreshhold = 100f;
        
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

        [SerializeField]
        private GameObject light;


        private void StartCrafting()
        {
            progress = 0f;
            progressPercentage = 0f;
            hasCraftedItem = false;

            isCrafting = true;
        }

        private void EndCrafting(bool item)
        {
            isCrafting = false;

            hasCraftedItem = item;
        }

        public void Update()
        {
            // check gamemode every 20th frame
            frameCounter++;
            if (frameCounter >= 20)
            {
                requirePower = GameModeUtils.RequiresPower();
                frameCounter = 0;
            }

            float powerToConsume = energyCost / timeToCraft * Time.deltaTime;

            if (requirePower)
            {
                if (enoughPower && powerConsumer.powerRelay.GetPower() < powerToConsume + powerEndThreshhold)
                {

                    enoughPower = false;
                    progress = 0f;
                }
                else if (!enoughPower && powerConsumer.powerRelay.GetPower() > powerToConsume + powerEndThreshhold * 2)
                {
                    enoughPower = true;
                }
            }
            else
            {
                enoughPower = true;
            }

            // control crafting
            if (isCrafting)
            {
                if (progress >= timeToCraft)
                {
                    EndCrafting(true);
                }
                else
                {
                    if (enoughPower)
                    {
                        powerConsumer.ConsumePower(powerToConsume, out _);
                        progress += Time.deltaTime;
                    }
                    progressPercentage = progress / timeToCraft * 100;
                }
            }

            // control visuals

            ionCube.SetActive(hasCraftedItem);

            if (isCrafting && !beamsSet)
            {
                SetEnergyBeams(true);
                light.SetActive(true);
            }
            if (!isCrafting && beamsSet)
            {
                SetEnergyBeams(false);
                light.SetActive(false);
            }
        }

        public void OnHandHover(GUIHand hand)
        {
            string text = craftingTechType.AsString(false);
            string text2 = string.Empty;
            GameInput.Button button = GameInput.Button.LeftHand;

            if (hasCraftedItem)
            {
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            else if (isCrafting)
            {
                if (enoughPower)
                {
                    text = Language.main.Get("IonFabricatorProgress");
                    HandReticle.main.SetProgress(progressPercentage / 100);
                    HandReticle.main.SetIcon(HandReticle.IconType.Progress, 1.5f);
                    button = GameInput.Button.None;
                }
                else
                {
                    text = Language.main.Get("IonFabricatorStop");
                    text2 = Language.main.Get("IonFabricatorNoPower");
                    HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
                }
            }
            else if (!enoughPower)
            {
                text = Language.main.Get("IonFabricatorNoPower");
                button = GameInput.Button.None;
            }
            else
            {
                text = Language.main.Get("IonFabricatorStart");
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }

            HandReticle.main.SetText(HandReticle.TextType.Hand, text, true, button);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, text2, true, GameInput.Button.None);
        }
        public void OnHandClick(GUIHand hand)
        {
            if (hasCraftedItem)
            {
                TryPickup();
            }
            else if (!isCrafting && enoughPower)
            {
                StartCrafting();
            }
            else if (isCrafting && !enoughPower)
            {
                EndCrafting(false);
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

            requirePower = GameModeUtils.RequiresPower();
        }

        private void SetEnergyBeams(bool active)
        {
            one.SetGravityBeam(active ? beam13.transform : null, beamColour);
            two.SetGravityBeam(active ? beam14.transform : null, beamColour);
            three.SetGravityBeam(active? beam15.transform: null, beamColour);
            four.SetGravityBeam(active ? beam16.transform : null, beamColour);
            five.SetGravityBeam(active ? beam9.transform : null, beamColour);
            six.SetGravityBeam(active ? beam10.transform : null, beamColour);
            seven.SetGravityBeam(active ? beam11.transform : null, beamColour);
            eight.SetGravityBeam(active ? beam12.transform : null, beamColour);

            beamsSet = active;
        }
    }
}
