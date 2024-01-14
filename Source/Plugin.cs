using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace HeliumCompany;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInProcess("Lethal Company.exe")]
public class Plugin : BaseUnityPlugin
{
    public static Plugin Instance;

    public static ConfigEntry<float> PitchValue = null!;
    public static ConfigEntry<bool> OverridePitch = null!;
    public static ConfigEntry<bool> RuntimeConfig = null!;

    public static ConfigEntry<bool> OverTime = null!;
    public static ConfigEntry<float> OverTimeElapse = null!;
    public static ConfigEntry<float> OverTimeAmount = null!;
    public static ConfigEntry<float> OverTimeMaximum = null!;

    public static float OverTimePitch = 0.0f;

    public void Awake()
    {
        Instance = this;

        PitchValue = Config.Bind("General", "Pitch", 3.0f, "The (base) pitch the voice chat is set to.");
        OverridePitch = Config.Bind("General", "Override", true, "Overrides any pitch value assigned by the game.\nRecommended due to the TZP-Inhalant increasing player pitch.");
        RuntimeConfig = Config.Bind("General", "RuntimeConfig", false, "Allows you to edit the config in runtime, applying changes whenever you leave the settings menu.\nIf disabled during runtime, this will not function again until restart.");

        OverTime = Config.Bind("OverTime", "Enable", false, "Increases the voice chat pitch over time, resetting each round.");
        OverTimeElapse = Config.Bind("OverTime", "Seconds", 1.0f, "How many seconds before the pitch increases.\nThe lower the value, the more robotic the voice chat becomes.");
        OverTimeAmount = Config.Bind("OverTime", "Amount", 0.005f, "The amount the pitch increases by.");
        OverTimeMaximum = Config.Bind("OverTime", "Maximum", 9.0f, "The maximum the pitch can go up to.");

        Harmony harmony = new(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }

    public static float ConvertPitch(float pitch)
    {
        return (pitch / 12.0f) + 1.0f;
    }

    public static float GetPitch()
    {
        return ConvertPitch(OverTime.Value ? OverTimePitch : PitchValue.Value);
    }
}
