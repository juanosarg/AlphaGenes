using HarmonyLib;
using RimWorld;
using Verse.Grammar;
using Verse;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Verse.AI;

namespace AlphaGenes
{

    [HarmonyPatch(typeof(Pawn_InteractionsTracker))]
    [HarmonyPatch("StartSocialFight")]
    public static class AlphaGenes_Pawn_InteractionsTracker_StartSocialFight_Patch
    {


        [HarmonyPostfix]
        public static void ReceiveRecreation(Pawn ___pawn, Pawn otherPawn)

        {

            if (___pawn?.HasActiveGene(InternalDefOf.AG_Rowdy) == true)
            {
                ___pawn.needs?.joy?.GainJoy(0.3f, JoyKindDefOf.Social);
            }

            if (otherPawn?.HasActiveGene(InternalDefOf.AG_Rowdy) == true)
            {
                otherPawn.needs?.joy?.GainJoy(0.3f, JoyKindDefOf.Social);
            }



        }


    }
}
