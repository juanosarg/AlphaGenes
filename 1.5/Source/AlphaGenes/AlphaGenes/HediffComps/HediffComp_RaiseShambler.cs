using RimWorld;
using Verse;
using Verse.Sound;
using System;
using AnimalBehaviours;
using System.Collections.Generic;
using RimWorld.Planet;
using System.Linq;

namespace AlphaGenes
{
    public class HediffComp_RaiseShambler : HediffComp
    {
       

        public HediffCompProperties_RaiseShambler Props
        {
            get
            {
                return (HediffCompProperties_RaiseShambler)this.props;
            }
        }

       


        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {

            float severityToTurn = Props.severityToTurn;

            Map map = this.parent.pawn.Corpse.Map;
            if (map != null && this.parent.Severity > severityToTurn)
            {
                for (int i = 0; i < 20; i++)
                {
                    IntVec3 c;
                    CellFinder.TryFindRandomReachableCellNearPosition(this.parent.pawn.Corpse.Position, this.parent.pawn.Corpse.Position, map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);

                    FilthMaker.TryMakeFilth(c, map, ThingDefOf.Filth_CorpseBile);

                }

                StaticCollectionsClass.pawnsToRise.Add(this.parent.pawn);



                InternalDefOf.Hive_Spawn.PlayOneShot(new TargetInfo(this.parent.pawn.Corpse.Position, map, false));
               



            }

        }

      


    }
}


