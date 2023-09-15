using HarmonyLib;

namespace AgeOfShovels;

[HarmonyPatch(typeof(AntiCheatManager))]
class HAntiCheatManager
{
    [HarmonyPrefix]
    [HarmonyPatch("Method_Public_Static_Boolean_PDM_0")]
    public static bool ac1(ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch("Method_Public_Static_Boolean_1")]
    public static bool ac2(ref bool __result)
    {
        __result = true;
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch("Method_Public_Static_Boolean_2")]
    public static bool ac3(ref bool __result)
    {   
        __result = true;
        return false;
    }
}