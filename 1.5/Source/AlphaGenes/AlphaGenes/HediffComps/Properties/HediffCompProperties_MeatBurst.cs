

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Text;

namespace AlphaGenes
{
    public class HediffCompProperties_MeatBurst : HediffCompProperties
    {


        public List<PawnKindDef> dividePawnKindOptions = new List<PawnKindDef>();

        public int dividePawnCount;

        public List<PawnKindDef> dividePawnKindAdditionalForced = new List<PawnKindDef>();

        public IntRange divideBloodFilthCountRange;

        public HediffCompProperties_MeatBurst()
        {
            this.compClass = typeof(HediffComp_MeatBurst);
        }

        
    }
}
