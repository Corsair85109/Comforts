using BepInEx;
using BepInEx.Logging;
using Comforts.Audio;
using Comforts.Prefabs.Bathroom;
using Comforts.Prefabs.Kitchen;
using Comforts.Prefabs.Power;
using HarmonyLib;
using Nautilus.Handlers;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Comforts
{
    [BepInPlugin(MyGUID, PluginName, VersionString)]
    public class ComfortsPlugin : BaseUnityPlugin
    {
        public const string MyGUID = "com.Bobasaur.Comforts";
        public const string PluginName = "Comforts";
        public const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGUID);

        public static AssetBundle theUltimateBundleOfAssets;
        public static string modFolder;

        private void Awake()
        {
            Debug.Log($"Will load {PluginName} version {VersionString}.");
            Harmony.PatchAll();
            Debug.Log($"{PluginName} version {VersionString} is loaded.");

            // Get mod folder
            modFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Find assetbundle
            theUltimateBundleOfAssets = AssetBundle.LoadFromFile(Path.Combine(modFolder, "Assets/Comforts"));

            // Register Localization
            LanguageHandler.RegisterLocalizationFolder();

            // Register jukebox songs
            JukeboxSongs.RegisterSongs(Path.Combine(modFolder, "JukeboxSongs"));

            // Register prefabs
            RegisterAllPrefabs();
        }

        private void RegisterAllPrefabs()
        {
            // Bathroom
            Shower.Register();

            // Power
            Jukebox.Register();
            Speaker.Register();
            WallSpeaker.Register();

            // Kitchen
            Sink.Register();
        }
    }
}
