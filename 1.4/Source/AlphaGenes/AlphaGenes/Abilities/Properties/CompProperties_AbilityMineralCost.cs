using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaGenes
{


 

    public class CompProperties_AbilityMineralCost : CompProperties_AbilityEffect
    {
        public float mineralCost;

        public CompProperties_AbilityMineralCost()
        {
            compClass = typeof(CompAbilityEffect_MineralCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return (string)("AG_AbilityMineralCost".Translate() + ": ") + Mathf.RoundToInt(mineralCost * 100f);
        }
    }
}