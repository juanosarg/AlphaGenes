

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Linq;
using Verse.Sound;
using UnityEngine;
using AlphaGenes;
using Verse.AI;

namespace AlphaGenes
{
    class HediffComp_StageByMood : HediffComp
    {


        public HediffCompProperties_StageByMood Props
        {
            get
            {
                return (HediffCompProperties_StageByMood)this.props;
            }
        }


        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);


            if (this.parent.pawn.IsHashIntervalTick(500) && this.parent.pawn.Map != null && ModsConfig.RoyaltyActive)
            {
                float severity = 0f;
                if (parent.pawn.needs?.mood?.thoughts?.memories?.GetFirstMemoryOfDef(ThoughtDefOf.Catharsis) != null)
                {
                    severity = 0.1f;
                }
                if (parent.pawn.needs?.mood?.CurLevel<Pawn.mindState.mentalBreaker.BreakThresholdExtreme)
                {
                    severity = 0.3f;
                }else if (parent.pawn.needs?.mood?.CurLevel < Pawn.mindState.mentalBreaker.BreakThresholdMajor)
                {
                    severity = 0.5f;
                }
                else if (parent.pawn.needs?.mood?.CurLevel < Pawn.mindState.mentalBreaker.BreakThresholdMinor)
                {
                    severity = 0.7f;
                }



                this.parent.Severity = severity;
            }


        }



    }
}
