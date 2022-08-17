﻿using HarmonyLib;

namespace SCore.Harmony.Blocks
{
    public class UpdateCurrentBlockposAndValueFire
    {
        [HarmonyPatch(typeof(EntityAlive))]
        [HarmonyPatch("updateCurrentBlockPosAndValue")]
        public class SCoreBlock_updateCurrentBlockPosAndValue
        {
            public static void Postfix(EntityAlive __instance)
            {
                if (FireManager.Instance.Enabled == false) return;

                if (__instance == null) return ;
                if (__instance is EntityVehicle) return;

                Vector3i blockPosition = __instance.GetBlockPosition();
                if (FireManager.Instance.isBurning(blockPosition))
                {
                    var buff = Configuration.GetPropertyValue("FireManagement", "BuffOnFire");
                    if (!string.IsNullOrEmpty(buff))
                        __instance.Buffs.AddBuff(buff);
                }
            }
        }

    }
}
