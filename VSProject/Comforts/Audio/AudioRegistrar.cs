using FMOD;
using Nautilus.Handlers;
using Nautilus.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Comforts.Audio
{
    internal class AudioRegistrar
    {
        public const MODE k3DSoundModes = MODE.DEFAULT | MODE._3D | MODE.ACCURATETIME | MODE._3D_LINEARSQUAREROLLOFF;
        public const MODE k2DSoundModes = MODE.DEFAULT | MODE._2D | MODE.ACCURATETIME;
        public const MODE kStreamSoundModes = k2DSoundModes | MODE.CREATESTREAM;


        public static void RegisterAudio(AssetBundle bundle)
        {



            // put custom audio to register here

            // Present
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Present"), "Present", 0f, JukeboxSongs.range, true);
            ComfortsFMODAssets.present = AudioUtils.GetFmodAsset("Present");

            // Constructable sound effects
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Cook_loop"), "Cook_loop", 0f, 10f, true);
            ComfortsFMODAssets.cookerLoop = AudioUtils.GetFmodAsset("Cook_loop");

            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Sink"), "Sink", 0f, 10f, false);
            ComfortsFMODAssets.sink = AudioUtils.GetFmodAsset("Sink");

            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Shower"), "Shower", 0f, 10f, false);
            ComfortsFMODAssets.shower = AudioUtils.GetFmodAsset("Shower");

            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Fusion_start"), "Fusion_start", 0f, 30f, false);
            ComfortsFMODAssets.FusionStart = AudioUtils.GetFmodAsset("Fusion_start");
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Fusion_loop"), "Fusion_loop", 0f, 30f, true);
            ComfortsFMODAssets.FusionLoop = AudioUtils.GetFmodAsset("Fusion_loop");
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Fusion_end"), "Fusion_end", 0f, 30f, false);
            ComfortsFMODAssets.FusionEnd = AudioUtils.GetFmodAsset("Fusion_end");

            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("button_click"), "button_click", 0f, 10f, false);
            ComfortsFMODAssets.switchSound = AudioUtils.GetFmodAsset("button_click");

        }

        public static void AddWorldSoundEffect(AudioClip clip, string soundPath, float minDistance = 0f, float maxDistance = 100f, bool looping = false, string overrideBus = null)
        {
            var sound = AudioUtils.CreateSound(clip, k3DSoundModes);
            sound.set3DMinMaxDistance(minDistance, maxDistance);
            if (looping)
            {
                sound.setMode(MODE.LOOP_NORMAL);
            }
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, string.IsNullOrEmpty(overrideBus) ? AudioUtils.BusPaths.PlayerSFXs : overrideBus);
        }

        public static void AddPDAVoiceline(AudioClip clip, string soundPath)
        {
            var sound = AudioUtils.CreateSound(clip, k2DSoundModes);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, AudioUtils.BusPaths.PDAVoice);
        }

        public static void AddVoiceLine(AudioClip clip, string soundPath)
        {
            var sound = AudioUtils.CreateSound(clip, kStreamSoundModes);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, AudioUtils.BusPaths.VoiceOvers);
        }
    }
}
