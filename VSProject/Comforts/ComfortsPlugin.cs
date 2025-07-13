using BepInEx;
using BepInEx.Logging;
using Comforts.Audio;
using Comforts.Commands;
using Comforts.Patches;
using Comforts.Prefabs.Bathroom;
using Comforts.Prefabs.Bedroom.Beds;
using Comforts.Prefabs.Decorations;
using Comforts.Prefabs.Decorations.Curtains;
using Comforts.Prefabs.Kitchen;
using Comforts.Prefabs.Power;
using Comforts.Utility;
using Comforts.Utility.References;
using HarmonyLib;
using Nautilus.Handlers;
using Nautilus.Utility;
using System.Collections;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.U2D;
using static TechStringCache;
using Comforts.Saving;

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

        public static SaveData save { get; set; } = SaveDataHandler.RegisterSaveDataCache<SaveData>();

        public static ComfortsConfig ModConfig { get; } = OptionsPanelHandler.RegisterModOptions<ComfortsConfig>();

        private void Awake()
        {
            Utility.Logger.Log($"Will load {PluginName} version {VersionString}...");


            // Apply all harmony patches
            Harmony.PatchAll();

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

            // Register pda entries
            RegisterEncies();

            save.OnStartedSaving += SaveHandler.OnSaveStart;
            save.OnFinishedLoading += SaveHandler.OnLoadFinish;

            Utility.Logger.Log($"{PluginName} version {VersionString} is loaded.");


            // Get all references
            GetAllReferences();
        }

        private void RegisterAllPrefabs()
        {
            // Bathroom
            Shower.Register();
            RoundSink.Register();

            // Power
            Jukebox.Register();
            Speaker.Register();
            WallSpeaker.Register();
            IonFusionReactor.Register();
            MusicChip.Register();
            FloorLocker.Register();

            // Kitchen
            Sink.Register();
            Cooker.Register();

            // Decorations
            LavaLamp.Register();
            RedCurtain.Register();
            BlueCurtain.Register();
            GreenCurtain.Register();
            Beanbag.Register();

            // Bedroom
            BlueBed.Register();
        }

        private void RegisterEncies()
        {
            //PDAEncyclopedia.mapping
            PDAHandler.AddEncyclopediaEntry("IonFusionReactorEncy", "Tech/Habitats/Comforts", Language.main.Get("IonFusionReactor"), Language.main.Get("IonFusionReactorEncyDesc"), theUltimateBundleOfAssets.LoadAsset<Texture2D>("IonFusionReactorDatabank"));
            StoryGoalHandler.RegisterItemGoal("IonFusionReactorEncy", Story.GoalType.Encyclopedia, TechType.PrecursorIonPowerCell, 60f);
        }

        private void GetAllReferences()
        {
            UWE.CoroutineHost.StartCoroutine(BenchReferenceManager.EnsureBenchReferenceExists());
            UWE.CoroutineHost.StartCoroutine(BedReferenceManager.EnsureBedReferencesExist());
        }
    }
}
