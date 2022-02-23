using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Yotogis;

namespace COM3D25.DebugLilly.BepInExPlugin
{
    internal static class StopwatchPatch
    {
        internal static ManualLogSource log;
        internal static ConfigFile config;
        internal static Stopwatch stopwatch;

        internal static void Awake(BepInEx.Logging.ManualLogSource logger, BepInEx.Configuration.ConfigFile Config, System.Diagnostics.Stopwatch Stopwatch)
        {
            log = logger;
            config = Config;
            stopwatch = Stopwatch;
        }

        [HarmonyPatch(typeof(GameMain), "OnInitialize")]
        [HarmonyPrefix]
        public static void GameMain_Init_pre()
        {
            log.LogMessage($"GameMain_OnInitialize_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(GameMain), "OnInitialize")]
        [HarmonyPostfix]
        public static void GameMain_Init_post()
        {
            log.LogMessage($"GameMain_OnInitialize_post {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(GameUty), "Init")]
        [HarmonyPrefix]
        public static void GameUty_Init_pre()
        {
            log.LogMessage($"GameUty_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(GameUty), "Init")]
        [HarmonyPostfix]
        public static void GameUty_Init_post()
        {
            log.LogMessage($"GameUty_Init_post {stopwatch.Elapsed}");
        }


        [HarmonyPatch(typeof(SoundMgr), "Init")]
        [HarmonyPrefix]
        public static void SoundMgr_Init_pre()
        {
            log.LogMessage($"SoundMgr_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(SoundMgr), "Init")]
        [HarmonyPostfix]
        public static void SoundMgr_Init_post()
        {
            log.LogMessage($"SoundMgr_Init_post {stopwatch.Elapsed}");
        }


        [HarmonyPatch(typeof(Skill), "CreateData")]
        [HarmonyPrefix]
        public static void Skill_Init_pre()
        {
            log.LogMessage($"Skill_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(Skill), "CreateData")]
        [HarmonyPostfix]
        public static void Skill_Init_post()
        {
            log.LogMessage($"Skill_Init_post {stopwatch.Elapsed}");
        }


        [HarmonyPatch(typeof(SystemDialog), "Init")]
        [HarmonyPrefix]
        public static void SystemDialog_Init_pre()
        {
            log.LogMessage($"SystemDialog_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(SystemDialog), "Init")]
        [HarmonyPostfix]
        public static void SystemDialog_Init_post()
        {
            log.LogMessage($"SystemDialog_Init_post {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(CharacterMgr), "Init")]
        [HarmonyPrefix]
        public static void CharacterMgr_Init_pre()
        {
            log.LogMessage($"CharacterMgr_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(CharacterMgr), "Init")]
        [HarmonyPostfix]
        public static void CharacterMgr_Init_post()
        {
            log.LogMessage($"CharacterMgr_Init_post {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(FacilityManager), "Init")]
        [HarmonyPrefix]
        public static void FacilityManager_Init_pre()
        {
            log.LogMessage($"FacilityManager_Init_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(FacilityManager), "Init")]
        [HarmonyPostfix]
        public static void FacilityManager_Init_post()
        {
            log.LogMessage($"FacilityManager_Init_post {stopwatch.Elapsed}");
        }


        [HarmonyPatch(typeof(ScriptManager), "Initialize")]
        [HarmonyPrefix]
        public static void ScriptManager_Init_pre()
        {
            log.LogMessage($"ScriptManager_Initialize_pre {stopwatch.Elapsed}");
        }

        [HarmonyPatch(typeof(ScriptManager), "Initialize")]
        [HarmonyPostfix]
        public static void ScriptManager_Init_post()
        {
            log.LogMessage($"ScriptManager_Initialize_post {stopwatch.Elapsed}");
        }


    }
}
