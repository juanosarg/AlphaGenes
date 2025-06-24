using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaGenes
{

    public class CompTempNodeBuilding : ThingComp
    {
        public CompProperties_TempNodeBuilding Props => (CompProperties_TempNodeBuilding)this.props;


        public Pawn pawn;

        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            Hediff hediff = pawn?.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_TempNodeEffect);
            pawn?.health.RemoveHediff(hediff);
            base.PostDestroy(mode, previousMap);
        }


        public override void PostExposeData()
        {
            Scribe_References.Look(ref pawn, "pawn");

            base.PostExposeData();

        }

    }
}
