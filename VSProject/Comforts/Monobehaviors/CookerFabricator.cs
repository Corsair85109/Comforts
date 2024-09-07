using Comforts.Audio;
using FMODUnity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Monobehaviors
{
    internal class CookerFabricator : Crafter, IHandTarget, IConstructable, IObstacle
    {
        public FMOD_CustomLoopingEmitter soundEmitter;

        public CrafterGhostModel ghost;

        public CraftTree.Type craftTree = CraftTree.Type.Fabricator;

        public float closeDistance = 5f;

        private bool isDeconstructionObstacle = true;

        private float spawnAnimationDelay = 1f;

        private float spawnAnimationDuration = 1.7f;

        private PowerRelay powerRelay;

        private bool _opened;

        private bool _progressChanged;

        private bool _itemChanged;

        private float _progressDelayScalar;


        public override void Start()
        {
            base.Start();

            powerRelay = base.gameObject.GetComponentInParent<PowerRelay>();

            soundEmitter = transform.Find("SoundEmitter").GetComponent<FMOD_CustomLoopingEmitter>();
            soundEmitter.asset = ComfortsFMODAssets.cookerLoop;
        }

        public override void OnStateChanged(bool crafting)
        {
            if (soundEmitter.playing != crafting)
            {
                if (crafting)
                {
                    soundEmitter.Play();
                    return;
                }
                soundEmitter.Stop();
            }


        }

        private bool opened
        {
            get
            {
                return _opened;
            }
            set
            {
                if (_opened != value)
                {
                    _opened = value;
                    OnOpenedChanged(_opened);
                }
            }
        }


        protected virtual void LateUpdate()
        {
            if (opened)
            {
                if (!HasEnoughPower() || (!FPSInputModule.current.lockMovement && !PlayerIsInRange(closeDistance)))
                {
                    Close();
                }
            }
            else if (base.HasCraftedItem())
            {
                opened = true;
            }
            if (_itemChanged)
            {
                _itemChanged = false;
                if (ghost != null)
                {
                    ghost.UpdateModel((base.logic != null) ? base.logic.currentTechType : TechType.None);
                }
            }
            if (_progressChanged)
            {
                _progressChanged = false;
                if (ghost != null && base.logic != null)
                {
                    ghost.UpdateProgress(Mathf.Clamp01((base.logic.progress - _progressDelayScalar) / (1f - _progressDelayScalar)));
                }
            }
        }

        public override void Initialize()
        {
            if (_initialized)
            {
                return;
            }
            base.Initialize();
            if (base.logic != null && ghost != null)
            {
                ghost.UpdateModel(base.logic.currentTechType);
                ghost.UpdateProgress(base.logic.progress);
            }
        }

        public override void Deinitialize()
        {
            if (!_initialized)
            {
                return;
            }
            base.Deinitialize();
            if (base.logic != null && ghost != null)
            {
                ghost.UpdateModel(TechType.None);
                ghost.UpdateProgress(0f);
            }
        }

        public override void Craft(TechType techType, float duration)
        {
            if (!CrafterLogic.ConsumeEnergy(powerRelay, 5f))
            {
                return;
            }
            if (!CrafterLogic.ConsumeResources(techType))
            {
                return;
            }
            if (CraftData.GetCraftTime(techType, out duration))
            {
                duration = Mathf.Max(spawnAnimationDelay + spawnAnimationDuration, duration);
            }
            else
            {
                duration = spawnAnimationDelay + spawnAnimationDuration;
            }
            base.Craft(techType, duration);
        }

        public override void OnCraftingBegin(TechType techType, float duration)
        {
            if (duration > 20f)
            {
                ErrorMessage.AddMessage(Language.main.GetFormat<string, float>("CraftingBegin", Language.main.Get(techType.AsString(false)), duration));
            }
            _progressDelayScalar = Mathf.Clamp(spawnAnimationDelay / duration, 0f, 0.9f);
            base.OnCraftingBegin(techType, duration);
        }

        public override void OnCraftingEnd()
        {
            if (base.logic == null)
            {
                return;
            }
            base.logic.TryPickup();
        }

        public override void OnCraftedItemPickup(GameObject item)
        {
            PlayerTool component = item.GetComponent<PlayerTool>();
            if (component != null && component.ShouldPlayFirstUseAnimation() && Inventory.main.quickSlots.SelectSlotByGameObject(item))
            {
                Close();
            }
        }

        public override void OnItemChanged(TechType techType)
        {
            _itemChanged = true;
            _progressChanged = true;
        }

        public override void OnProgress(float progress)
        {
            _progressChanged = true;
        }

        public void OnOpenedChanged(bool opened)
        {
            if (!opened)
            {
                uGUI.main.craftingMenu.Close(this);
            }
        }

        private void Close()
        {
            if (base.HasCraftedItem())
            {
                uGUI.main.craftingMenu.Close(this);
                return;
            }
            opened = false;
        }

        private bool PlayerIsInRange(float distance)
        {
            return (Player.main.transform.position - base.transform.position).sqrMagnitude < distance * distance;
        }

        private bool HasEnoughPower()
        {
            return !GameModeUtils.RequiresPower() || (powerRelay != null && powerRelay.GetPower() >= 5f);
        }

        public void OnHandHover(GUIHand hand)
        {
            if (!base.enabled || base.logic == null)
            {
                return;
            }
            string text = "GenericUse";
            string text2 = string.Empty;
            if (base.logic.inProgress)
            {
                text = base.logic.craftingTechType.AsString(false);
                HandReticle.main.SetProgress(base.logic.progress);
                HandReticle.main.SetIcon(HandReticle.IconType.Progress, 1.5f);
            }
            else if (base.HasCraftedItem())
            {
                text = base.logic.currentTechType.AsString(false);
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            else
            {
                if (!HasEnoughPower())
                {
                    text2 = "unpowered";
                }
                HandReticle.main.SetIcon(HandReticle.IconType.Hand, 1f);
            }
            HandReticle.main.SetText(HandReticle.TextType.Hand, text, true, GameInput.Button.LeftHand);
            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, text2, true, GameInput.Button.None);
        }

        public void OnHandClick(GUIHand hand)
        {
            if (!base.enabled || base.logic == null || base.logic.inProgress)
            {
                return;
            }
            if (base.HasCraftedItem())
            {
                base.logic.TryPickup();
                return;
            }
            if (HasEnoughPower() && base.isValidHandTarget)
            {
                opened = true;
                uGUI.main.craftingMenu.Open(craftTree, this);
            }
        }

        public bool IsDeconstructionObstacle()
        {
            return isDeconstructionObstacle;
        }

        public bool CanDeconstruct(out string reason)
        {
            reason = null;
            return (base.logic == null || !base.logic.inProgress) && !base.HasCraftedItem();
        }

        public void OnConstructedChanged(bool constructed)
        {
            if (!constructed)
            {
                Close();
            }
        }


    }
}
