﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Harmony;
using RimWorld;
using RimWorld.BaseGen;
using RimWorld.Planet;
using Verse;

namespace TheThirdAge
{
    [StaticConstructorOnStartup]
    public static class RemoveModernStuffHarmony
    {
        static RemoveModernStuffHarmony()
        {
            HarmonyInstance harmony = HarmonyInstance.Create("rimworld.removemodernstuff");

            harmony.Patch(AccessTools.Method(typeof(PawnUtility), "IsTravelingInTransportPodWorldObject"),
                new HarmonyMethod(typeof(RemoveModernStuffHarmony).GetMethod("IsTravelingInTransportPodWorldObject")), null);
        }

        //No one travels in transport pods in the medieval times
        public static bool IsTravelingInTransportPodWorldObject(Pawn pawn, ref bool __result)
        {
            if (RemoveModernStuff.maxTechLevel <= TechLevel.Industrial)
            {
                __result = false;
                return false;
            }
            return true;
        }
    }
}