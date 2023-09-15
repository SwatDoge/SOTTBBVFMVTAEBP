using Gameplay.Gamemode;
using HarmonyLib;
using System.IO;
using UnityEngine;
using VoxReader;
using VoxReader.Interfaces;

namespace AgeOfShovels;

[HarmonyPatch(typeof(VoxelFortify))]
class HVoxelFortify
{
    [HarmonyPrefix]
    [HarmonyPatch(nameof(VoxelFortify.Activate))]
    static bool Activate(MonoBehaviourPublicVeVoTrBoInVoTrBoVoTrUnique __instance)
    {
        MonoBehaviourPublicVeVoTrBoInVoTrBoVoTrUnique PrebuiltVoxelStructureInstance = __instance.GetComponentInChildren<MonoBehaviourPublicVeVoTrBoInVoTrBoVoTrUnique>(true);
        PrebuiltVoxelStructureInstance.field_Public_ArrayOf_Vector3Int_0 = new Vector3Int[0];
        
        IVoxFile VoxelMap = VoxReader.VoxReader.Read(@"C:\Users\swat\Desktop\battlebits_server\AgeOfShovels\levels\with_ramps.vox");

        int voxelIndex = 0;
        
        foreach (Model model in VoxelMap.Models)
        {
            for (int i = 0; i < model.Voxels.Length; i++)
            {
                Voxel voxel = model.Voxels[i];
                PrebuiltVoxelStructureInstance.field_Public_ArrayOf_Vector3Int_0[voxelIndex] = new Vector3Int(voxel.GlobalPosition.X, voxel.GlobalPosition.Z + 1, voxel.GlobalPosition.Y);
                voxelIndex++;
            }
        }

        return true;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(MethodType.Getter)]
    [HarmonyPatch(nameof(VoxelFortify.prop_Int32_0))]
    static bool PreGetCountdown(ref int __result)
    {
        __result = 0;
        return false;
    }
}