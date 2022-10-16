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



    public class CompRandomItemSpawner : ThingComp
    {
        public CompProperties_RandomItemSpawner Props => (CompProperties_RandomItemSpawner)this.props;


        public override void CompTick()
        {
            base.CompTick();
            if (this.parent.IsHashIntervalTick(500))
            {

                if (Find.CurrentMap.mapPawns.FreeColonistsSpawnedCount > 0)
                {
                    SpawnItemAndDelete();
                }

            }
        }

        public void SpawnItemAndDelete()
        {
            Thing thing = GenSpawn.Spawn(Props.items.RandomElement(), this.parent.Position, this.parent.Map);
            thing.stackCount = 1;
            this.parent.Destroy();

        }

    }
}
