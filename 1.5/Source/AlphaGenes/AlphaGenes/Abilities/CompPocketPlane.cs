using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompPocketPlane : CompAbilityEffect
    {

        private new CompProperties_PocketPlane Props => (CompProperties_PocketPlane)props;
        public Map pocketMap;
        public Map originMap;
        public IntVec3 originLocation;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);

            Pawn pawn = this.parent.pawn;
            parent.AddEffecterToMaintain(EffecterDefOf.Skip_Entry.Spawn(target.Thing, pawn.Map), target.Thing.Position, 60);
            SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(target.Cell, parent.pawn.Map));
            Map pocketMap = GeneratePocketMap();
            Map originalMap = StoreOriginalMap();

            SoundDefOf.TraversePitGate.PlayOneShot(pawn);

            if (pawn.Map == pocketMap)
            {
                pawn.DeSpawnOrDeselect();
                GenSpawn.Spawn(pawn, originLocation, originMap, Rot4.Random);
                pawn.GetLord()?.Notify_PawnLost(pawn, PawnLostCondition.ExitedMap);

            }
            else
            {
                pawn.DeSpawnOrDeselect();
                GenSpawn.Spawn(pawn, pocketMap.Center, pocketMap, Rot4.Random);
                pawn.GetLord()?.Notify_PawnLost(pawn, PawnLostCondition.ExitedMap);
            }



        }


        public Map GeneratePocketMap()
        {
            if (pocketMap == null)
            {
                pocketMap = PocketMapUtility.GeneratePocketMap(new IntVec3(10, 1, 10), InternalDefOf.AG_PocketPlane, null, null);              
            }
            return pocketMap;
           
            
        }
        public Map StoreOriginalMap()
        {
           
            if (originMap == null)
            {
                originMap = this.parent.pawn.Map;
                originLocation = this.parent.pawn.Position;
            }
            return originMap;

        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_References.Look(ref pocketMap, "pocketMap");
            Scribe_References.Look(ref originMap, "originMap");
            Scribe_Values.Look(ref originLocation, "originLocation");

        }




    }
}