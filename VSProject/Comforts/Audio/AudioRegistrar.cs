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
            AddWorldSoundEffect(bundle.LoadAsset<AudioClip>("Present"), "Present");
        }

        public static void AddVoiceLine(AudioClip clip, string soundPath)
        {
            var sound = AudioUtils.CreateSound(clip, kStreamSoundModes);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, AudioUtils.BusPaths.VoiceOvers);
        }

        public static void AddWorldSoundEffect(AudioClip clip, string soundPath, float minDistance = 1f, float maxDistance = 100f, string overrideBus = null)
        {
            var sound = AudioUtils.CreateSound(clip, k3DSoundModes);
            if (maxDistance > 0f)
            {
                sound.set3DMinMaxDistance(minDistance, maxDistance);
            }
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, string.IsNullOrEmpty(overrideBus) ? AudioUtils.BusPaths.PlayerSFXs : overrideBus);
        }

        public static void AddInterfaceSoundEffect(AudioClip clip, string soundPath)
        {
            var sound = AudioUtils.CreateSound(clip, k2DSoundModes);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, AudioUtils.BusPaths.PlayerSFXs);
        }

        public static void AddPDAVoiceline(AudioClip clip, string soundPath)
        {
            var sound = AudioUtils.CreateSound(clip, k2DSoundModes);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, AudioUtils.BusPaths.PDAVoice);
        }

        public static void AddWorldLoopingSoundEffect(AudioClip clip, string soundPath, float minDistance = 1f, float maxDistance = 100f, string overrideBus = null)
        {
            var sound = AudioUtils.CreateSound(clip, k3DSoundModes);
            if (maxDistance > 0f)
            {
                sound.set3DMinMaxDistance(minDistance, maxDistance);
            }
            sound.setMode(MODE.LOOP_NORMAL);
            CustomSoundHandler.RegisterCustomSound(soundPath, sound, string.IsNullOrEmpty(overrideBus) ? AudioUtils.BusPaths.PlayerSFXs : overrideBus);
        }
    }
}
