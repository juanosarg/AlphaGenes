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

    [HarmonyPatch(typeof(Pawn_MechanitorTracker))]
    [HarmonyPatch("DrawCommandRadius")]
    public static class AlphaGenes_Pawn_MechanitorTracker_DrawCommandRadius_Patch
    {

        public static Pawn pawn;

        [HarmonyPostfix]
        public static void DrawExtraCommandRadius(Pawn_MechanitorTracker __instance, Pawn ___pawn)

        {
            pawn = ___pawn;
            if (___pawn.health?.hediffSet?.HasHediff(InternalDefOf.AG_IncreasedCommandRange) == true)
            {

                if (___pawn.Spawned && AnySelectedDraftedMechs)
                {
                    GenDraw.DrawRadiusRing(___pawn.Position, 35f, Color.green, (IntVec3 c) => ___pawn.mechanitor.CanCommandTo(c));
                }

            }
            if (___pawn.health?.hediffSet?.HasHediff(InternalDefOf.AG_DecreasedCommandRange) == true)
            {

                if (___pawn.Spawned && AnySelectedDraftedMechs)
                {
                    GenDraw.DrawRadiusRing(___pawn.Position, 15f, Color.red, (IntVec3 c) => ___pawn.mechanitor.CanCommandTo(c));
                }

            }


        }

        public static bool AnySelectedDraftedMechs
        {
            get
            {
                List<Pawn> selectedPawns = Find.Selector.SelectedPawns;
                for (int i = 0; i < selectedPawns.Count; i++)
                {
                    if (selectedPawns[i].GetOverseer() == pawn && selectedPawns[i].Drafted)
                    {
                        return true;
                    }
                }
                return false;
            }
        }


    }
}
