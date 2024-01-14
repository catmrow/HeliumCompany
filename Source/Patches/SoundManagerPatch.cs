using HarmonyLib;
using UnityEngine;

namespace HeliumCompany.Patches;

[HarmonyPatch(typeof(SoundManager))]
public class SoundManagerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetPlayerPitch")]
    public static void SetPlayerPitch_Postfix(SoundManager __instance, float pitch, int playerObjNum)
    {
        float realPitch = pitch;

        if (Plugin.OverridePitch.Value)
            realPitch = Plugin.GetPitch();
        else
            realPitch *= Plugin.GetPitch();

        __instance.diageticMixer.SetFloat($"PlayerPitch{playerObjNum}", realPitch);
    }

    [HarmonyPostfix]
    [HarmonyPatch("SetPlayerVoiceFilters")]
    public static void SetPlayerVoiceFilters_Postfix(SoundManager __instance)
    {
        for (int i = 0; i < StartOfRound.Instance.allPlayerScripts.Length; i++)
            __instance.SetPlayerPitch(__instance.playerVoicePitches[i], i);
    }

    static float TimeElapsed = 0.0f;

    [HarmonyPrefix]
    [HarmonyPatch("Update")]
    public static void Update_Prefix()
    {
        if (Plugin.OverTime.Value)
        {
            bool negativeMaximum = Plugin.OverTimeMaximum.Value < 0;

            if (!negativeMaximum ? Plugin.OverTimePitch <= Plugin.OverTimeMaximum.Value : Plugin.OverTimePitch >= Plugin.OverTimeMaximum.Value)
            {
                if (TimeElapsed < Plugin.OverTimeElapse.Value)
                {
                    TimeElapsed += Time.deltaTime;
                }
                else
                {
                    Plugin.OverTimePitch += Plugin.OverTimeAmount.Value;
                    TimeElapsed = 0.0f;
                }
            }
            else if (!negativeMaximum ? Plugin.OverTimePitch > Plugin.OverTimeMaximum.Value : Plugin.OverTimePitch < Plugin.OverTimeMaximum.Value)
            {
                Plugin.OverTimePitch = Plugin.OverTimeMaximum.Value;
            }
        }
        else
        {
            if (Plugin.OverTimePitch != 0.0f)
                Plugin.OverTimePitch = 0.0f;
        }
    }
}
