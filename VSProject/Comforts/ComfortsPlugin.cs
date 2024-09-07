using BepInEx;
using BepInEx.Logging;
using Comforts.Audio;
using Comforts.Commands;
using Comforts.Prefabs.Bathroom;
using Comforts.Prefabs.Kitchen;
using Comforts.Prefabs.Power;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Utility;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.U2D;

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
        public static SpriteAtlas epicAtlasOfSprites;
        public static string modFolder;

        private void Awake()
        {
            Debug.Log($"Will load {PluginName} version {VersionString}.");
            Harmony.PatchAll();
            Debug.Log($"{PluginName} version {VersionString} is loaded.");

            PirateChecker.CheckPiracy();

            // Get mod folder
            modFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Find assetbundle
            theUltimateBundleOfAssets = AssetBundle.LoadFromFile(Path.Combine(modFolder, "Assets/Comforts"));
            epicAtlasOfSprites = theUltimateBundleOfAssets.LoadAsset<SpriteAtlas>("SpriteAtlas");

            // Register Audio
            AudioRegistrar.RegisterAudio(theUltimateBundleOfAssets);

            // Register Localization
            LanguageHandler.RegisterLocalizationFolder();

            // Register console commands
            ConsoleCommandsHandler.RegisterConsoleCommands(typeof(ComfortsConsoleCommands));

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
            Cooker.Register();
        }
    }
}
