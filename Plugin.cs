using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LLBML.States;
using LLBML.Utils;
using LLScreen;

namespace R2MM_BoomboxMod;

[BepInPlugin(PluginDetails.PLUGIN_GUID, PluginDetails.PLUGIN_NAME, PluginDetails.PLUGIN_VERSION)]
[BepInProcess("LLBlaze.exe")]
[BepInDependency(PluginDetails.DEPENDENCY_LLBMODDINGLIB)]
[BepInDependency(PluginDetails.DEPENDENCY_MODMENU, BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BaseUnityPlugin
{
    
    internal static Plugin Instance { get; private set; }
    internal static ManualLogSource GlobalLogSource { get; private set; }
        
    private void Awake()
    {
        Instance = this;
        
        GlobalLogSource = base.Logger;
        GlobalLogSource.LogInfo($"Plugin {PluginDetails.PLUGIN_NAME} is loaded!");
        Harmony.CreateAndPatchAll(typeof(Plugin), PluginDetails.PLUGIN_GUID);
        
        Configs.BindConfigs(Config);
    }

    private void Start()
    {
        ModDependenciesUtils.RegisterToModMenu(base.Info, PluginDetails.MODMENU_TEXT);
    }

    [HarmonyPatch(typeof(ScreenGameHud), "SetTrack")]
    [HarmonyPrefix]
    private static void HideTrack(ref string trackName)
    {
        if (!Configs.IsMusicTextEnabled.Value)
        {
            GlobalLogSource.LogInfo("Music text DISABLED -> hiding boombox music text");
            trackName = "";
        }
        else
        {
            GlobalLogSource.LogInfo("Music text ENABLED -> showing boombox music text");
        }
    }

    [HarmonyPatch(typeof(UIScreen), "Open", new Type[] {typeof(ScreenType), typeof(int), typeof(ScreenTransition), typeof(bool)})]
    [HarmonyPostfix]
    private static void HideTimer()
    {
        GameState currentGameState = GameStates.GetCurrent();
        if (currentGameState != GameState.GAME)
        {
            return;
        }
        
        ScreenGameHud hudScreen = (ScreenGameHud)UIScreen.GetScreen(ScreenType.GAME_HUD);
        if (hudScreen == null)
        {
            return;
        }

        if (!Configs.IsTimerTextEnabled.Value)
        {
            GlobalLogSource.LogInfo("Timer text DISABLED -> hiding boombox timer text");
            hudScreen.imtextTime.gameObject.SetActive(false);
        }
        else
        {
            GlobalLogSource.LogInfo("Timer text ENABLED -> showing boombox timer text");
            hudScreen.imtextTime.gameObject.SetActive(true);
        }
    }
    
}
