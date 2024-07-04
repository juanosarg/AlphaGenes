using System;
using Verse;
using System.Collections.Generic;

namespace AlphaGenes
{
    public class HediffCompProperties_LimbRegeneration : HediffCompProperties
    {

        public IntRange rateInTicks;
        public float healAmount = 1f;


        public HediffCompProperties_LimbRegeneration()
        {
            this.compClass = typeof(HediffComp_LimbRegeneration);
        }
    }
}
