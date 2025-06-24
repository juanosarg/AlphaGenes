

using RimWorld;
using System.Collections.Generic;
using Verse;
using System.Text;

namespace AlphaGenes
{
    public class HediffCompProperties_Exploder : HediffCompProperties
    {


        public float radius;
        public DamageDef damageType;
        public int damageAmount = -1;
        public float damagePenetration = -1;
        public SoundDef soundCreated = null;
        public ThingDef thingCreated = null;
        public float thingCreatedChance = 0;
        public float chanceToStartFire = 0;

        public bool damageUser = true;

        public HediffCompProperties_Exploder()
        {
            this.compClass = typeof(HediffComp_Exploder);
        }
    }
}
