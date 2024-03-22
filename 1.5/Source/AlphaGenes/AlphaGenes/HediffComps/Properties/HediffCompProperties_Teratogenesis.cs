
using Verse;
namespace AlphaGenes
{
    public class HediffCompProperties_Teratogenesis : HediffCompProperties
    {
       
        public IntRange period;

        public HediffCompProperties_Teratogenesis()
        {
            compClass = typeof(HediffComp_Teratogenesis);
        }
    }
}
