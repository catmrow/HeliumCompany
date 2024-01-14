using HarmonyLib;

namespace HeliumCompany.Patches;

[HarmonyPatch(typeof(StartOfRound))]
public class StartOfRoundPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetShipReadyToLand")]
    public static void SetShipReadyToLand_Postfix()
    {
        if (Plugin.OverTime.Value)
            Plugin.OverTimePitch = 0.0f;
    }

    [HarmonyPostfix]
    [HarmonyPatch("ResetShip")]
    public static void ResetShip_Postfix(StartOfRound __instance)
    {
        if (Plugin.OverTime.Value)
            Plugin.OverTimePitch = 0.0f;
    }
}
