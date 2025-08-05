using BepInEx.Configuration;

namespace R2MM_BoomboxMod;

public class Configs
{

    internal static ConfigEntry<bool> IsMusicTextEnabled { get; private set; }
    internal static ConfigEntry<bool> IsTimerTextEnabled { get; private set; }

    internal static void BindConfigs(ConfigFile config)
    {
        IsMusicTextEnabled = config.Bind<bool>("Toggles", "musicText", false);
        IsTimerTextEnabled = config.Bind<bool>("Toggles", "timerText", false);
    }
    
}