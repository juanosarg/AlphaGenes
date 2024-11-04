
using HarmonyLib;
using RimWorld;
using Verse;
using System;
using System.Collections.Generic;


namespace AlphaGenes
{
    [HarmonyPatch(typeof(EquipmentUtility), "CanEquip", new Type[] { typeof(Thing), typeof(Pawn), typeof(string), typeof(bool) }, new ArgumentType[]
        {0,0,ArgumentType.Out,0})]
    internal class AlphaGenes_EquipmentUtility_CanEquip_Postfix
    {

        public static HashSet<ThingDef> blockedWeapons = new HashSet<ThingDef>() { InternalDefOf.AG_ForsakenSniper, InternalDefOf.AG_ForsakenShotgun,
        InternalDefOf.AG_ForsakenAssaultRifle, InternalDefOf.AG_ForsakenLongBlade, InternalDefOf.AG_ForsakenBattleAxe, InternalDefOf.AG_Forsaken_Hood,
        InternalDefOf.AG_ForsakenBulkSword,InternalDefOf.AG_ForsakenSpear, InternalDefOf.AG_ForsakenBow, InternalDefOf.AG_ForsakenHydra,InternalDefOf.AG_Forsaken_Hood_Child,
        InternalDefOf.AG_ForsakenCloak,InternalDefOf.AG_ForsakenCloak_Child,InternalDefOf.AG_ForsakenMarineHelmet,InternalDefOf.AG_ForsakenMarineHelmetPsy,
        InternalDefOf.AG_ForsakenMarineArmor
    };

        [HarmonyPostfix]
        private static void PostFix(ref bool __result,Thing thing, Pawn pawn, ref string cantReason)
        {
            if (blockedWeapons.Contains(thing.def) && !pawn.HasActiveGene(InternalDefOf.AG_ForsakenKnowledge))
            {
                __result = false;
                cantReason = "AG_NeedsForsakenKnowledgeToWield".Translate();
            }
           

        }
    }
}