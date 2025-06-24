using RimWorld;
using System.Collections.Generic;
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

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (!DebugSettings.ShowDevGizmos)
            {
                yield break;
            }

            yield return new Command_Action
            {
                defaultLabel = "DEV: Reset scorpion counter",
                defaultDesc = "Only use if the ocular slingers system seems to be somehow failing. This will reset the current scorpion counter to zero. I recommend deleting (or killing) the currently existing scorpions before doing this",
                action = delegate
                {
                    Current.Game.World.GetComponent<ScorpionCounter_WorldComponent>().totalScorpions = 0;
                },

            };
        }


    }
}
