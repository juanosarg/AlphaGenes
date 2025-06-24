
using System.Collections.Generic;
using System.Linq;
using AlphaGenes;
using RimWorld;
using Verse;

namespace AlphaGenes
{
    public class HediffComp_RecreationOnDeath : HediffComp
    {
        private HediffCompProperties_RecreationOnDeath Props => (HediffCompProperties_RecreationOnDeath)props;


        public override void Notify_KilledPawn(Pawn victim, DamageInfo? dinfo)
        {
            base.Notify_KilledPawn(victim, dinfo);

            parent.pawn.needs?.joy?.GainJoy(0.1f, JoyKindDefOf.Social);
        }



    }
}