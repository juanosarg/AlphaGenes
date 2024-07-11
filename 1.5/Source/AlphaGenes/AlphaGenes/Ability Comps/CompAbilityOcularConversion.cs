using System;
using System.Collections.Generic;
using RimWorld.Planet;
using RimWorld;
using Verse;
using AlphaGenes;

namespace AlphaGenes
{
    public class CompAbilityOcularConversion : CompAbilityEffect
    {
        private System.Random rand = new System.Random();

        new public CompProperties_AbilityOcularConversion Props
        {
            get
            {
                return (CompProperties_AbilityOcularConversion)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);
            FleckMaker.Static(target.Cell, this.parent.pawn.Map,DefDatabase<FleckDef>.GetNamed("PsycastPsychicEffect"));

            HashSet<Thing> hashSet = new HashSet<Thing>(target.Cell.GetThingList(this.parent.pawn.Map));
            if (hashSet != null)
            {


                foreach (Thing current in hashSet)
                {
                    Plant plantTarget = current as Plant;
                    if (plantTarget != null)
                    {


                        PlantProperties plant = plantTarget.def.plant;
                        bool flag = (plant != null);
                        if (flag)
                        {
                            if (plant.IsTree && (plantTarget.def.defName != "GU_AlienTree") && (plantTarget.def.defName != "AA_AlienTree") && (plantTarget.def.defName != "Plant_TreeAnima") && (plantTarget.def.defName != "Plant_TreeGauranlen"))
                            {
                                Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named(Props.plantList[0]), plantTarget.Position, plantTarget.Map, WipeMode.Vanish);
                                Plant thingToDestroy = (Plant)plantTarget;
                                thing2.Growth = thingToDestroy.Growth;
                                plantTarget.Destroy();
                            }
                            else if (!plant.IsTree && (plantTarget.def.defName != "GU_AlienGrass") && (plantTarget.def.defName != "GU_RedLeaves") && (plantTarget.def.defName != "GU_RedPlantsTall")
                               && (plantTarget.def.defName != "AA_AlienGrass") && (plantTarget.def.defName != "AA_RedLeaves") && (plantTarget.def.defName != "AA_RedPlantsTall") && (plantTarget.def.defName != "Plant_GrassAnima")
                               )
                            {
                                if (rand.NextDouble() < 0.4)
                                {
                                    Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named(Props.plantList[1]), plantTarget.Position, plantTarget.Map, WipeMode.Vanish);
                                    Plant thingToDestroy = (Plant)plantTarget;
                                    thing2.Growth = thingToDestroy.Growth;
                                    plantTarget.Destroy();
                                }
                                else if (rand.NextDouble() > 0.4 && rand.NextDouble() < 0.7)
                                {
                                    Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named(Props.plantList[2]), plantTarget.Position, plantTarget.Map, WipeMode.Vanish);
                                    Plant thingToDestroy = (Plant)plantTarget;
                                    thing2.Growth = thingToDestroy.Growth;
                                    plantTarget.Destroy();
                                }
                                else if (rand.NextDouble() > 0.7)
                                {
                                    Plant thing2 = (Plant)GenSpawn.Spawn(ThingDef.Named(Props.plantList[3]), plantTarget.Position, plantTarget.Map, WipeMode.Vanish);
                                    Plant thingToDestroy = (Plant)plantTarget;
                                    thing2.Growth = thingToDestroy.Growth;
                                    plantTarget.Destroy();
                                }
                            }


                        }

                    }

                }
            }








        }



    }
}
