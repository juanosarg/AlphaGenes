using RimWorld;
using UnityEngine;
using Verse;

namespace AlphaGenes
{
    public class CompScorpionCounter : ThingComp
    {


        public CompProperties_ScorpionCounter Props
        {
            get
            {
                return (CompProperties_ScorpionCounter)this.props;
            }
        }



        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            if (this.parent.Faction == Faction.OfPlayer)
            {
                Current.Game.World.GetComponent<ScorpionCounter_WorldComponent>().totalScorpions++;
            }

        }



        public override void PostDestroy(DestroyMode mode, Map previousMap)
        {
            if (this.parent.Faction == Faction.OfPlayer)
            {
                Current.Game.World.GetComponent<ScorpionCounter_WorldComponent>().totalScorpions--;
            }
        }


    }
}
