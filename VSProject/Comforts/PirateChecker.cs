using Oculus.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nautilus.Assets.CustomModelData;
using UnityEngine;

namespace Comforts
{
    internal class PirateChecker
    {
        // borrowed from ramunelib
        // thanks ramune!


        public static bool isPirated = false;

        private static readonly string steamapi = "steam_api64.dll";
        private static readonly long steamapisize = 220000;

        private static readonly List<string> Targets = new List<string>() {
            "steam_api64.cdx", "steam_api64.ini", "steam_emu.ini",
            "Torrent-Igruha.Org.URL", "oalinst.exe", "account_name.txt",
            "valve.ini", "chuj.cdx", "SteamUserID.cfg", "Achievements.bin",
            "steam_settings", "user_steam_id.txt", "Free Steam Games Pre-installed for PC.url",
        };

        public static void CheckPiracy()
        {
            var directory = Directory.GetFiles(Environment.CurrentDirectory);
            var filenames = directory.Select(_ => Path.GetFileName(_));

            if (filenames.Contains(steamapi))
            {
                var length = new FileInfo(steamapi);

                if (length.Length > steamapisize)
                {
                    new GameObject("IsPirated");
                    isPirated = true;
                    return;
                }
            }

            foreach (var filename in filenames)
            {
                if (Targets.Contains(filename))
                {
                    new GameObject("IsPirated");
                    isPirated = true;
                    return;
                }
            }
        }
    }
}
