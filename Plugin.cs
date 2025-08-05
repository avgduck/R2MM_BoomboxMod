using BepInEx;
using BepInEx.Logging;
using LLBML.Utils;

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
    }

    private void Start()
    {
        ModDependenciesUtils.RegisterToModMenu(base.Info, PluginDetails.MODMENU_TEXT);
    }
}
