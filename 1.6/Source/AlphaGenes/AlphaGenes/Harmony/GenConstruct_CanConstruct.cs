
using HarmonyLib;
using RimWorld;
using Verse;
using System;
using Verse.AI;


namespace AlphaGenes
{
    [HarmonyPatch(typeof(GenConstruct), "CanConstruct", new Type[] { typeof(Thing), typeof(Pawn), typeof(bool), typeof(bool), typeof(JobDef) })]
    internal class AlphaGenes_GenConstruct_CanConstruct_Postfix
    {
        [HarmonyPostfix]
        private static void PostFix(ref bool __result, Thing t, Pawn p)
        {
            
            if(__result && t.def.entityDefToBuild == InternalDefOf.AG_ForsakenForge)
            {
               
                if (!p.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge))
                {
                   
                    JobFailReason.Is("AG_NeedsForsakenKnowledge".Translate());
                    __result = false;
                }
                
            }

        }
    }
}