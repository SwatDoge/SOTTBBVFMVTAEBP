using HarmonyLib;
using Networking;
using UnityEngine;

namespace AgeOfShovels;


[HarmonyPatch(typeof(ServerHandler))]
class HServerHandler
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ServerHandler.OnPlayerSpawn))]
    public static bool PreOnClientAskingToSpawn()
    {
        Plugin.Log.LogInfo("Asking to spawn");
        return true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ServerHandler.OnConsoleCommand))]
    static void PostOnConsoleCommand(string cmd)
    {
        Plugin.Log.LogInfo(cmd);
        string[] args = cmd.Split(" ");

        if (args[0].Equals("voxel") && args[1].Equals("place"))
        {
            var position = new Vector3Int(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));

            HVoxelManager.placeVoxel(position, default(VoxelBlockData));
        }
        else if (args[0].Equals("voxel") && args[1].Equals("destroy"))
        {
            var position = new Vector3Int(int.Parse(args[2]), int.Parse(args[3]), int.Parse(args[4]));

            HVoxelManager.destroyVoxel(position);
        }
        else if (args[0].Equals("getpos"))
        {
            if (!ulong.TryParse(args[1], out ulong networkClient))
                return;

            if (ObjectPublicStBoUIBoCoUIDaObSiInUnique.Method_Public_Static_Boolean_UInt64_byref_ObjectPublicStBoUIBoCoUIDaObSiInUnique_0(networkClient, out ObjectPublicStBoUIBoCoUIDaObSiInUnique client))
            {
                Plugin.Log.LogInfo("Position:" + client.field_Public_PlayerNetworkState_0.field_Public_Vector3_5.ToString());
                Plugin.Log.LogInfo("Grid Position:" + VoxelManager.GetGrid(client.field_Public_PlayerNetworkState_0.field_Public_Vector3_5));
                Plugin.Log.LogInfo("Voxel grid:" + VoxelManager.ConvertWorldGridToVoxelGrid(VoxelManager.GetGrid(client.field_Public_PlayerNetworkState_0.field_Public_Vector3_5)));
            }
        }
        else if (args[0].Equals("buildHP"))
        {
            byte.TryParse(args[1], out byte hp);

            HVoxelManager.buildHP = hp;
        }
        else if (args[0].Equals("clear"))
        {
            for (byte i = 0; i < 255; i++)
            {
                Plugin.Log.LogInfo(" ");
            }
        }
    }
}