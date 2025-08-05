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
    }

    private void Start()
    {
        ModDependenciesUtils.RegisterToModMenu(base.Info, PluginDetails.MODMENU_TEXT);
    }

    [HarmonyPatch(typeof(ScreenGameHud), "SetTrack")]
    [HarmonyPrefix]
    private static void HideTrack(ref string trackName)
    {
        trackName = "";
    }

    [HarmonyPatch(typeof(UIScreen), "Open", new Type[] {typeof(ScreenType), typeof(int), typeof(ScreenTransition), typeof(bool)})]
    [HarmonyPostfix]
    private static void HideTime()
    {
        GameState currentGameState = GameStates.GetCurrent();
        if (currentGameState != GameState.GAME)
        {
            //GlobalLogSource.LogInfo("Current game state is not GAME");
            return;
        }
        
        ScreenGameHud hudScreen = (ScreenGameHud)UIScreen.GetScreen(ScreenType.GAME_HUD);
        if (hudScreen == null)
        {
            //GlobalLogSource.LogWarning("Could not fetch ScreenGameHud");
            return;
        }
        
        //GlobalLogSource.LogInfo("Got ScreenGameHud");

        if (hudScreen.imtextTime.gameObject.activeSelf)
        {
            hudScreen.imtextTime.gameObject.SetActive(false);
        }
    }
    
}
