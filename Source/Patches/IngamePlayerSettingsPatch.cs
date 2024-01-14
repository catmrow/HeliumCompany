using HarmonyLib;
using UnityEngine;

namespace HeliumCompany.Patches;

[HarmonyPatch(typeof(IngamePlayerSettings))]
public class IngamePlayerSettingsPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SaveChangedSettings")]
    public static void SaveChangedSettings_Postfix()
    {
        ReloadConfig();
    }

    [HarmonyPostfix]
    [HarmonyPatch("DiscardChangedSettings")]
    public static void DiscardChangedSettings_Postfix()
    {
        ReloadConfig();
    }

    public static void ReloadConfig()
    {
        if (Plugin.RuntimeConfig.Value)
        {
            Plugin.Instance.Config.Reload();
            Debug.Log($"[{PluginInfo.PLUGIN_GUID}] Config has been reloaded.");
        }
    }
}
