﻿using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using PrivateMaidMode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.DebugLilly.BepInExPlugin
{
    internal class DebugLillyPatch
    {
        internal static ManualLogSource log;
        internal static ConfigFile config;

        internal static void Awake(BepInEx.Logging.ManualLogSource logger, BepInEx.Configuration.ConfigFile Config)
        {
            log = logger;
            config = Config;

            //
            ADVKagManagerLog = config.Bind("ADVKagManager", "Log", false);

            //
            baseKagManagerLog = config.Bind("baseKagManager", "Log", false);
            baseKagManager_OnScenarioLoadEvent_Log = config.Bind("baseKagManager", "OnScenarioLoadEvent_Log", false);

            //
            DLLKagScriptLog = config.Bind("DLLKagScript", "Log", false);
            DLL_KAG_LoadScenarioStringLog = config.Bind("DLLKagScript", "DLL_KAG_LoadScenarioString Log", false);
            DLL_KAG_LoadScriptFile_Log = config.Bind("DLLKagScript", "DLLKagScript_LoadScriptFile Log", false);
            DLL_KAG_AddFinishFileLog = config.Bind("DLLKagScript", "DLL_KAG_AddFinishFile Log", false);
            DLL_KAG_AddTagLog = config.Bind("DLLKagScript", "DLL_KAG_AddTag Log", false);

            //
            DLLTJSScriptLog = config.Bind("DLLTJSScript", "Log", false);

            //
            GameUtyLog = config.Bind("GameUty", "Log", false);

            //
            KagScript_Log = config.Bind("KagScript", "Log", false);
            KagScript_TagCallLog = config.Bind("KagScript", "TagCall Log", false);
            KagScript_ExecLog = config.Bind("KagScript", "Exec Log", false);
            KagScript_OnScenarioLoadEventLog = config.Bind("KagScript", "OnScenarioLoadEvent Log", false);

            //
            Maid_SetProp_log = config.Bind("Maid", "SetProp Log", false);
            Maid_EyeToCamera_log = config.Bind("Maid", "EyeToCamera Log", false);

            //
            PrivateMaidModeLog = config.Bind("ScenePrivateEventModeAwake", "Log", false);

            //
            ScriptManagerLog = config.Bind("ScriptManager", "Log", false);
            ScriptManager_EvalScript_Log = config.Bind("ScriptManager", "EvalScript Log", false);
            ScriptManager_ReplacePersonal_Log = config.Bind("ScriptManager", "ReplacePersonal Log", true);

            //
            TJSScriptLog = config.Bind("TJSScript", "Log", false);

            //
            YotogiPlayManagerLog = config.Bind("YotogiPlayManager", "Log", false);


        }

        #region BaseKagManager

        internal static ConfigEntry<bool> baseKagManagerLog;
        internal static ConfigEntry<bool> baseKagManager_OnScenarioLoadEvent_Log;


        [HarmonyPatch(typeof(BaseKagManager), "LoadScriptFile", typeof(string), typeof(string))]
        [HarmonyPrefix]
        public static void BaseKagManager_LoadScriptFile(string file_name, string label_name = "")
        {

            if (baseKagManagerLog.Value)
                log.LogMessage($"BaseKagManager.LoadScriptFile , {file_name} , {label_name}");
        }

        [HarmonyPatch(typeof(BaseKagManager), "JumpLabel", typeof(string))]
        [HarmonyPrefix]
        public static void BaseKagManager_JumpLabel(string label_name)
        {
            if (baseKagManagerLog.Value)
                log.LogMessage($"BaseKagManager.JumpLabel , {label_name}");
        }

        // private void OnScenarioLoadEvent(string file_name, string label_name)
        [HarmonyPatch(typeof(BaseKagManager), "OnScenarioLoadEvent")]
        [HarmonyPrefix]
        public static void BaseKagManager_JumpLabel(string file_name, string label_name)
        {
            if (baseKagManager_OnScenarioLoadEvent_Log.Value)
                log.LogMessage($"BaseKagManager.OnScenarioLoadEvent , {file_name} , {label_name}");
        }

        #endregion

        #region ADVKagManager

        internal static ConfigEntry<bool> ADVKagManagerLog;

        [HarmonyPatch(typeof(ADVKagManager), "Exec")]
        [HarmonyPrefix]
        public static void ADVKagManager_Exec(Dictionary<string, string> ___tag_backup_)
        {
            if (!ADVKagManagerLog.Value) return;
            foreach (var item in ___tag_backup_)
            {
                log.LogMessage($"ADVKagManager.Exec: {item.Key} , {item.Value}");
            }
        }

        #endregion

        #region DLLKagScript

        internal static ConfigEntry<bool> DLLKagScriptLog;
        internal static ConfigEntry<bool> DLL_KAG_LoadScenarioStringLog;
        internal static ConfigEntry<bool> DLL_KAG_LoadScriptFile_Log;
        internal static ConfigEntry<bool> DLL_KAG_AddFinishFileLog;
        internal static ConfigEntry<bool> DLL_KAG_AddTagLog;

        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_GoToLabel")]
        [HarmonyPrefix]
        public static void DLL_KAG_GoToLabel(string label_name)
        {
            if (DLL_KAG_LoadScriptFile_Log.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_GoToLabel: " + label_name);
        }

        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_LoadScenario")]
        [HarmonyPrefix]
        public static void DLL_KAG_LoadScenario(string file_name)
        {
            if (DLL_KAG_LoadScriptFile_Log.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_LoadScenario: " + file_name);
        }


        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_LoadScenarioString")]
        [HarmonyPrefix]
        public static void DLL_KAG_LoadScenarioString(string scenario_str)
        {
            if (DLL_KAG_LoadScenarioStringLog.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_LoadScenarioString: " + scenario_str);
        }

        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_AddFinishFile")]
        [HarmonyPrefix]
        public static void DLL_KAG_AddFinishFile(string file_name)
        {
            if (DLL_KAG_AddFinishFileLog.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_AddFinishFile: " + file_name);
        }

        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_GetFinishFile")]
        [HarmonyPrefix]
        public static void DLL_KAG_GetFinishFile(string file_name)
        {
            if (DLL_KAG_AddFinishFileLog.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_GetFinishFile: " + file_name);
        }

        /// <summary>
        /// DLLKagScript.DLL_KAG_AddTag: faceblend
        /// </summary>
        /// <param name="tag_name"></param>
        [HarmonyPatch(typeof(DLLKagScript), "DLL_KAG_AddTag")]
        [HarmonyPrefix]
        public static void DLL_KAG_AddTag(string tag_name)
        {
            if (DLL_KAG_AddTagLog.Value)
                log.LogMessage("DLLKagScript.DLL_KAG_AddTag: " + tag_name);
        }

        #endregion

        #region DLLTJSScript

        internal static ConfigEntry<bool> DLLTJSScriptLog;

        [HarmonyPatch(typeof(DLLTJSScript), "DLL_TJS_ExecScript")]
        [HarmonyPrefix]
        public static void DLLTJSScript_DLL_TJS_ExecScript(string exec_str)
        {
            if (DLLTJSScriptLog.Value)
                log.LogMessage($"DLLTJSScript.DLL_TJS_ExecScript , {exec_str}");
        }

        #endregion



        #region GameMain

        internal static bool loaded = false;

        /// <summary>
        /// 세이브 파일 로딩 시작
        /// </summary>
        [HarmonyPatch(typeof(GameMain), "Deserialize", new Type[] { typeof(int), typeof(bool) })]
        [HarmonyPrefix]
        public static void Deserialize()//GameMain __instance, ref bool __result, int f_nSaveNo, bool scriptExec = true)
        {
            loaded = false;
        }

        /// <summary>
        /// 세이브 파일 로딩 끝
        /// </summary>
        /// <param name="__result"></param>
        [HarmonyPatch(typeof(GameMain), "Deserialize", new Type[] { typeof(int), typeof(bool) })]
        [HarmonyPostfix]
        public static void Deserialize(bool __result)//GameMain __instance, ref bool __result, int f_nSaveNo, bool scriptExec = true)
        {
            if (__result)
            {
                loaded = true;
            }
        }

        #endregion

        #region GameUty

        internal static ConfigEntry<bool> GameUtyLog;

        // public static bool IsExistFile(string fileName, AFileSystemBase priorityFileSystem = null)
        [HarmonyPatch(typeof(GameUty), "IsExistFile")]
        [HarmonyPostfix]
        public static void IsExistFile(string fileName, bool __result)
        {
            if (GameUtyLog.Value)
                log.LogMessage($"GameUty.IsExistFile: {fileName} , {__result}");
        }

        #endregion

        #region KagScript

        internal static ConfigEntry<bool> KagScript_Log;
        internal static ConfigEntry<bool> KagScript_TagCallLog;
        internal static ConfigEntry<bool> KagScript_ExecLog;
        internal static ConfigEntry<bool> KagScript_OnScenarioLoadEventLog;

        [HarmonyPatch(typeof(KagScript), "Exec")]
        [HarmonyPrefix]
        public static void KagScript_Exec(string ___prev_tag_name_)
        {
            if (!KagScript_ExecLog.Value) return;
            log.LogMessage($"KagScript.Exec: {___prev_tag_name_}");
        }

        /// <summary>
        /// KagScript.AddTagCallBack: faceblend
        /// </summary>
        /// <param name="tag_name"></param>
        [HarmonyPatch(typeof(KagScript), "AddTagCallBack")]
        [HarmonyPrefix]
        public static void KagScript_AddTagCallBack(string tag_name)
        {
            if (!KagScript_TagCallLog.Value) return;
            log.LogMessage($"KagScript.AddTagCallBack: {tag_name}");
        }

        /// <summary>
        ///  KagScript.RemoveTagCallBack: faceblend
        /// </summary>
        /// <param name="tag_name"></param>
        [HarmonyPatch(typeof(KagScript), "RemoveTagCallBack")]
        [HarmonyPrefix]
        public static void KagScript_RemoveTagCallBack(string tag_name)
        {
            if (!KagScript_TagCallLog.Value) return;
            log.LogMessage($"KagScript.RemoveTagCallBack: {tag_name}");
        }

        [HarmonyPatch(typeof(KagScript), "RemoveTagCallBackAll")]
        [HarmonyPrefix]
        public static void KagScript_RemoveTagCallBackAll()
        {
            if (!KagScript_TagCallLog.Value) return;
            log.LogMessage($"KagScript.RemoveTagCallBackAll");
        }

        [HarmonyPatch(typeof(KagScript), "OnScenarioLoadEvent")]
        [HarmonyPrefix]
        public static void KagScript_OnScenarioLoadEvent(string file_name, string label_name)
        {
            if (!KagScript_OnScenarioLoadEventLog.Value) return;
            log.LogMessage($"KagScript.OnScenarioLoadEvent : {file_name} , {label_name}");
        }



        #endregion

        #region Maid

        internal static ConfigEntry<bool> Maid_SetProp_log;
        internal static ConfigEntry<bool> Maid_EyeToCamera_log;

        [HarmonyPatch(typeof(Maid), "EyeToCamera", typeof(Maid.EyeMoveType), typeof(float))]
        [HarmonyPrefix]
        public static void EyeToCamera(ref Maid.EyeMoveType f_eType, float f_fFadeTime = 0f)
        {
            if (Maid_EyeToCamera_log.Value)
            {
                log.LogInfo($"EyeToCamera {f_eType} , {f_fFadeTime} ");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mp"></param>
        /// <param name="filename"></param>
        /// <param name="f_nFileNameRID"></param>
        /// <param name="f_bTemp"></param>
        /// <param name="f_bNoScale"></param>
        //private void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
        [HarmonyPatch(typeof(Maid), "SetProp", typeof(MaidProp), typeof(string), typeof(int), typeof(bool), typeof(bool))]
        [HarmonyPrefix]
        public static void SetProp(MaidProp mp, string filename, int f_nFileNameRID, bool f_bTemp, bool f_bNoScale = false)
        {
            if (Maid_SetProp_log.Value && loaded)
            {
                log.LogInfo($"SetProp {mp.name} , {mp.strFileName} , {filename}");
            }
        }

        #endregion

        #region PrivateMaidMode

        internal static ConfigEntry<bool> PrivateMaidModeLog;

        [HarmonyPatch(typeof(ScenePrivateEventModeAwake), "SettingChildrenList")]
        [HarmonyPrefix]
        public static void ScenePrivateEventModeAwake_SettingChildrenList(Dictionary<string, WfScreenChildren> children_dic)
        {
            if (!PrivateMaidModeLog.Value) return;

            foreach (var child in children_dic)
                log.LogMessage($"ScenePrivateEventModeAwake.SettingChildrenList , {child.Key} , {child.Value.name} , {child.Value.tag} , {child.Value.enabled}");
        }

        [HarmonyPatch(typeof(PrivateSettingManager), "SettingChildrenList")]
        [HarmonyPrefix]
        public static void PrivateSettingManager_SettingChildrenList(Dictionary<string, WfScreenChildren> children_dic)
        {
            if (!PrivateMaidModeLog.Value) return;

            foreach (var child in children_dic)
                log.LogMessage($"PrivateSettingManager.SettingChildrenList , {child.Key} , {child.Value.name} , {child.Value.tag} , {child.Value.enabled}");
        }

        #endregion

        #region ScriptManager

        internal static ConfigEntry<bool> ScriptManagerLog;
        internal static ConfigEntry<bool> ScriptManager_EvalScript_Log;
        internal static ConfigEntry<bool> ScriptManager_ReplacePersonal_Log;

        // public void EvalScript(string eval_str, TJSVariant result)
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", typeof(string))]
        [HarmonyPatch(typeof(ScriptManager), "EvalScript", new[] { typeof(string), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Normal })]
        [HarmonyPrefix]
        public static void ScriptManager_EvalScript(string eval_str)
        {
            if (ScriptManager_EvalScript_Log.Value)
                log.LogMessage($"ScriptManager.EvalScript , {eval_str}");
        }


        [HarmonyPatch(typeof(ScriptManager), "EvalScriptFile", typeof(string))]
        [HarmonyPatch(typeof(ScriptManager), "EvalScriptFile", new[] { typeof(string), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Normal })]
        [HarmonyPrefix]
        public static void ScriptManager_EvalScriptFile(string file_name)
        {
            if (ScriptManager_EvalScript_Log.Value)
                log.LogMessage($"ScriptManager.EvalScriptFile , {file_name}");
        }


        [HarmonyPatch(typeof(ScriptManager), "ExecScript", typeof(string))]
        [HarmonyPatch(typeof(ScriptManager), "ExecScript", new[] { typeof(string), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [HarmonyPrefix]
        public static void ScriptManager_ExecScript(string exec_str)
        {
            if (ScriptManagerLog.Value)
                log.LogMessage($"ScriptManager.ExecScript , {exec_str}");
        }


        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile", typeof(string))]
        [HarmonyPatch(typeof(ScriptManager), "ExecScriptFile", new[] { typeof(string), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [HarmonyPrefix]
        public static void ScriptManager_ExecScriptFile(string file_name)
        {
            if (ScriptManagerLog.Value)
                log.LogMessage($"ScriptManager.ExecScriptFile , {file_name}");
        }

        // public static string ReplacePersonal(Maid maid, string text)
        // public static string ReplacePersonal(Maid[] maid_array, string text)
        [HarmonyPatch(typeof(ScriptManager), "ReplacePersonal", typeof(Maid[]), typeof(string))]
        [HarmonyPrefix]
        public static void ReplacePersonal_pre(Maid[] maid_array, string text)
        {
            if (ScriptManager_ReplacePersonal_Log.Value)
                log.LogMessage($"ScriptManager.ReplacePersonal_pre , {maid_array.Length} , {text}");
        }

        [HarmonyPatch(typeof(ScriptManager), "ReplacePersonal", typeof(Maid[]), typeof(string))]
        [HarmonyPostfix]
        public static void ReplacePersonal_post(string __result, Maid[] maid_array, string text)
        {
            if (ScriptManager_ReplacePersonal_Log.Value)
            {
                log.LogMessage($"ScriptManager.ReplacePersonal_post , {maid_array.Length} , {text} , {__result} , {GameUty.IsExistFile(__result)}");
                ;
            }

        }

        #endregion

        #region TJSScript

        internal static ConfigEntry<bool> TJSScriptLog;

        [HarmonyPatch(typeof(TJSScript), "ExecScript", typeof(string))]
        [HarmonyPatch(typeof(TJSScript), "ExecScript", new[] { typeof(string), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [HarmonyPrefix]
        public static void TJSScript_ExecScript1(string exec_str)
        {
            if (!TJSScriptLog.Value) return;
            log.LogMessage($"TJSScript.ExecScript1: {exec_str}");
        }

        [HarmonyPatch(typeof(TJSScript), "ExecScript", typeof(AFileBase))]
        [HarmonyPatch(typeof(TJSScript), "ExecScript", new[] { typeof(AFileBase), typeof(TJSVariant) }, new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [HarmonyPrefix]
        public static void TJSScript_ExecScript2(AFileBase file)
        {
            if (!TJSScriptLog.Value) return;
            log.LogMessage($"TJSScript.ExecScript2: ");
        }

        #endregion

        #region YotogiPlayManager

        internal static ConfigEntry<bool> YotogiPlayManagerLog;

        [HarmonyPatch(typeof(YotogiOldPlayManager), "CallCommandFile")]
        [HarmonyPrefix]
        public static void YotogiOldPlayManager_CallCommandFile(string file, string label)
        {
            if (YotogiPlayManagerLog.Value)
                log.LogMessage($"YotogiOldPlayManager_CallCommandFile , {file} , {label}");
        }

        [HarmonyPatch(typeof(YotogiPlayManager), "CallCommandFile")]
        [HarmonyPrefix]
        public static void YotogiPlayManager_CallCommandFile(string file, string label, bool lockRRUpdate)
        {
            if (YotogiPlayManagerLog.Value)
                log.LogMessage($"YotogiPlayManager_CallCommandFile , {file} , {label} , {lockRRUpdate}");
        }


        #endregion
    }
}
