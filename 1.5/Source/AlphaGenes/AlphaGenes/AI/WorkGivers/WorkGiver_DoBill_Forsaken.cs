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
    public class WorkGiver_DoBill_Forsaken: WorkGiver_DoBill
    {

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            if (!pawn.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge))
            {             
                return true;
            }

            return base.ShouldSkip(pawn, forced);
        }


    }
}
