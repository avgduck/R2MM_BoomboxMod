using System.Collections.Generic;

namespace R2MM_BoomboxMod;

public static class PluginDetails
{
    
    public const string PLUGIN_GUID = "avgduck.plugins.llb.r2mm-boomboxmod";
    public const string PLUGIN_NAME = "R2MM_BoomboxMod";
    public const string PLUGIN_VERSION = "1.0.1";
    
    public const string DEPENDENCY_LLBMODDINGLIB = "fr.glomzubuk.plugins.llb.llbml";
    public const string DEPENDENCY_MODMENU = "no.mrgentle.plugins.llb.modmenu";

    public static readonly List<string> MODMENU_TEXT = [
        "This mod allows the user to toggle both the music text and timer text of the in-game boombox HUD"
    ];
    
    
    
}