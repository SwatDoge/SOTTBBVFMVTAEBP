
using HarmonyLib;
using Networking;
using UnityEngine;

namespace AgeOfShovels;

[HarmonyPatch(typeof(VoxelManager))]
static class HVoxelManager
{
    public static byte buildHP = 50;
    public static byte maxHP = 250;
    public static byte repairHP = 50;

    [HarmonyPostfix]
    [HarmonyPatch(MethodType.Constructor)]
    public static void Constructor()
    {
        VoxelManager.MaxHP = maxHP;
    }
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(VoxelManager.mReceivedPlaceBlockRequest))]
    static bool PremReceivedPlaceBlockRequest(ObjectPublicObByInBoInQu1InObBoUnique ser)
    {
        ObjectPublicStBoUIBoCoUIDaObSiInUnique owner = AttributePublicObObUnique.field_Public_Static_ObjectPublicStBoUIBoCoUIDaObSiInUnique_0;
        Vector3Int position = ser.Method_Public_Vector3Int_PDM_0();

        if (!VoxelManager.AnyColliderExist(position) && VoxelManager.IsValid(position))
        {
            float distSqr = (position - owner.field_Public_PlayerNetworkState_0.field_Public_Vector3_5).sqrMagnitude;
            if (!MonoBehaviourPublicSiCoSiLiCo1SiObCoInUnique.InAnySafeZone(position) && distSqr < 36)
            {
                VoxelBlockData data = new VoxelBlockData() { TextureID = VoxelTextures.Default };
                ObjectPublicQu1ObBoObBoObObUnique pck = VoxelManager.Instance.mView.CreatePackageForRPC(20);

                pck.Method_Public_Void_UInt64_0(owner.prop_UInt64_1);
                pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.Method_Public_Void_Vector3Int_PDM_0(position);
                data.Write(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0);
                pck.Method_Public_Void_Byte_0(buildHP);

                pck.prop_Boolean_0 = true;
                pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[2] = (byte)EnumPublicSealedvaCl2vUnique.Clients;
                pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[5] = (byte)Plugin.RPCMethodId.PlaceBlock;

                ServerHandler.BroadcastReliable(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0, 0);
                ObjectPublicQu1ObBoObBoObObUnique.field_Private_Static_Queue_1_ObjectPublicQu1ObBoObBoObObUnique_0.Enqueue(pck);

                VoxelManager.Instance.mOnBlockPlaced(owner, position, data, buildHP);
            }
        }
        return false;
    }



    [HarmonyPrefix]
    [HarmonyPatch(nameof(VoxelManager.mReceivedToRepairBlockRequest))]
    static bool PremReceivedToRepairBlockRequest(ObjectPublicObByInBoInQu1InObBoUnique ser)
    {
        Plugin.Log.LogInfo("test");
        ObjectPublicStBoUIBoCoUIDaObSiInUnique owner = AttributePublicObObUnique.field_Public_Static_ObjectPublicStBoUIBoCoUIDaObSiInUnique_0;
        Vector3Int position = ser.Method_Public_Vector3Int_PDM_0();
        byte currentBlockHP;
        if (VoxelManager.TryGetHealt(position, out currentBlockHP))
        {
            if (currentBlockHP > 0)
            {
                float distSqr = (position - owner.field_Public_PlayerNetworkState_0.field_Public_Vector3_5).sqrMagnitude;
                if (distSqr < 36)
                {
                    if (currentBlockHP + repairHP > 150)
                    {
                        VoxelBlockData data = new VoxelBlockData() { TextureID = VoxelTextures.Supporter };
                        ObjectPublicQu1ObBoObBoObObUnique pck = VoxelManager.Instance.mView.CreatePackageForRPC(20);

                        pck.Method_Public_Void_UInt64_0(owner.prop_UInt64_1);
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.Method_Public_Void_Vector3Int_PDM_0(position);
                        data.Write(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0);
                        pck.Method_Public_Void_Byte_0(maxHP);

                        pck.prop_Boolean_0 = true;
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[2] = (byte)EnumPublicSealedvaCl2vUnique.Clients;
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[5] = (byte)Plugin.RPCMethodId.PlaceBlock;

                        ServerHandler.BroadcastReliable(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0, 0);
                        ObjectPublicQu1ObBoObBoObObUnique.field_Private_Static_Queue_1_ObjectPublicQu1ObBoObBoObObUnique_0.Enqueue(pck);

                        VoxelManager.Instance.mOnBlockPlaced(owner, position, data, maxHP);
                    }
                    else
                    {
                        ObjectPublicQu1ObBoObBoObObUnique pck = VoxelManager.Instance.mView.CreatePackageForRPC(20);

                        pck.Method_Public_Void_UInt64_0(owner.prop_UInt64_1);
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.Method_Public_Void_Vector3Int_PDM_0(position);
                        pck.Method_Public_Void_Byte_0(repairHP);

                        pck.prop_Boolean_0 = true;
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[2] = (byte)EnumPublicSealedvaCl2vUnique.Clients;
                        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[5] = (byte)Plugin.RPCMethodId.HealBlock;

                        ServerHandler.BroadcastReliable(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0, 0);
                        ObjectPublicQu1ObBoObBoObObUnique.field_Private_Static_Queue_1_ObjectPublicQu1ObBoObBoObObUnique_0.Enqueue(pck);

                        VoxelManager.Instance.mApplyHeal(position, owner, repairHP);
                    }
                }
            }
        }
        return false;
    }

    public static void placeVoxel(Vector3Int position, VoxelBlockData VoxelData)
    {
        ObjectPublicStBoUIBoCoUIDaObSiInUnique owner = ObjectPublicStBoUIBoCoUIDaObSiInUnique.prop_ObjectPublicStBoUIBoCoUIDaObSiInUnique_0;
        const byte hp = 250;
        ObjectPublicQu1ObBoObBoObObUnique pck = VoxelManager.Instance.mView.CreatePackageForRPC(20);

        pck.Method_Public_Void_UInt64_0(owner.prop_UInt64_1);
        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.Method_Public_Void_Vector3Int_PDM_0(position);
        VoxelData.Write(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0);
        pck.Method_Public_Void_Byte_0(hp);

        pck.prop_Boolean_0 = true;
        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[2] = (byte)EnumPublicSealedvaCl2vUnique.Clients;
        pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0.field_Public_ArrayOf_Byte_0[5] = (byte)Plugin.RPCMethodId.PlaceBlock;

        ServerHandler.BroadcastReliable(pck.field_Public_ObjectPublicObByInBoInQu1InObBoUnique_0, 0);
        ObjectPublicQu1ObBoObBoObObUnique.field_Private_Static_Queue_1_ObjectPublicQu1ObBoObBoObObUnique_0.Enqueue(pck);

        VoxelManager.Instance.mOnBlockPlaced(owner, position, VoxelData, hp);            
    }

    public static void destroyVoxel(Vector3Int position)
    {
        VoxelManager.Server_DestroyBlock(position, new ObjectPublicStBoUIBoCoUIDaObSiInUnique());
    }
}