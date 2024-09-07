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
            // put audio to register here

            // Present
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Present"), "Present", 0f, JukeboxSongs.range, true);
            ComfortsFMODAssets.present = AudioUtils.GetFmodAsset("Present");

            // Constructable sound effects
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Cook_loop"), "Cook_loop", 0f, 10f, true);
            ComfortsFMODAssets.cookerLoop = AudioUtils.GetFmodAsset("Cook_loop");

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
