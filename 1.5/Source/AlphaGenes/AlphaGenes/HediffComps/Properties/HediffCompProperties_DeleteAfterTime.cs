using System.Collections.Generic;
using Verse;
namespace AlphaGenes
{
    public class HediffCompProperties_DeleteAfterTime : HediffCompProperties
    {
        public int disappearsAfterTicks;
        public bool revertToMechanoid = false;
        public bool justDeletePawn = false;


        public HediffCompProperties_DeleteAfterTime()
        {
            compClass = typeof(HediffComp_DeleteAfterTime);
        }
    }
}
