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

    public class CompRandomRaidSpawner : ThingComp
    {
        public CompProperties_RandomRaidSpawner Props => (CompProperties_RandomRaidSpawner)this.props;

       

      

        public override void CompTick()
        {
            base.CompTick();
            if (this.parent.IsHashIntervalTick(500))
            {

                int num = GenRadial.NumCellsInRadius(12);
                for (int i = 0; i < num; i++)
                {
                    IntVec3 current = this.parent.Position + GenRadial.RadialPattern[i];
                    if (current.InBounds(this.parent.Map))
                    {
                        Pawn pawn = current.GetFirstPawn(this.parent.Map);
                        if ((pawn != null) && (pawn.Faction == Faction.OfPlayer))
                        {
                            SpawnHostileRaid();
                            break;
                        }
                    }
                }


            }
        }

        public void SpawnHostileRaid()
        {
            float points = StorytellerUtility.DefaultThreatPointsNow(this.parent.Map);

            Faction faction = Find.FactionManager.RandomRaidableEnemyFaction(allowHidden: false, allowDefeated: false, allowNonHumanlike: true);

            IncidentParms incidentParms = new IncidentParms
            {
               
                target = this.parent.Map,
                points = points,
                faction = faction,
                raidStrategy = RaidStrategyDefOf.ImmediateAttack

            };

            IncidentDefOf.RaidEnemy.Worker.TryExecute(incidentParms);

            foreach (Pawn pawn in this.parent.Map.mapPawns.FreeColonistsSpawned)
            {
                pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(InternalDefOf.AG_RaidedBioLab);
            }

            this.parent.Destroy();

        }

    }
}
