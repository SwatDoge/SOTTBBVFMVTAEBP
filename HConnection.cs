using HarmonyLib;
using Networking.Core;
using System.Linq;

namespace AgeOfShovels;

[HarmonyPatch(typeof(Connection))]
class HConnection
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(Connection.SendReliable))]
    public static bool PostSendReliable(ObjectPublicObByInBoInQu1InObBoUnique pck)
    {
        var packet = pck.ToString().Split("-");

        string[] bannedViews = new string[9] { "202", "213", "209", "211", "212", "214", "215", "204", "206" };

        
        if (!bannedViews.Contains(packet[3]))
        {
            if (packet[3].Equals("218") && packet[5] == "004")
            {
                Plugin.Log.LogInfo("Place block packet:");
            }

            if (packet[3].Equals("218") && packet[5] == "005")
            {
                Plugin.Log.LogInfo("Repair block packet:");
            }

            Plugin.Log.LogInfo(string.Join("-", packet));
        }


        return true;
    }
}