using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;

namespace AlphaGenes
{
    public class Gene_Resource_Metal : Gene_Resource, IGeneResourceDrain
    {
        public Gene_Resource Resource => this;

        public bool CanOffset => !pawn.Suspended;

        public float ResourceLossPerDay => def.resourceLossPerDay;

        public Pawn Pawn => pawn;

        public string DisplayLabel => def.resourceLabel;

        public override float InitialResourceMax => 1f;

        public override float MinLevelForAlert => 0.15f;

        public override float MaxLevelOffset => base.MaxLevelOffset;

        public override void Tick()
        {
            base.Tick();
            //Dont use utility resource offset because it will add the hemogencraving hediff even if the calling resource is not hemogen
            //This is basically just the utility done right. Might move to own utility class if doing multiple
            if (CanOffset)
            {
                float before = Value;
                float loss = (-ResourceLossPerDay / 60000f);
                Value += loss;
                if(before>0f && Value <= 0f)
                {
                    //Add hediff here
                }
            }
        }

        //Dont know what color codes to use
        protected override Color BarColor =>new ColorInt(145, 42, 42).ToColor;

        protected override Color BarHighlightColor => new ColorInt(145, 42, 42).ToColor;

        //This would only work with things that are already ingestible so for something like metal that is not ingestible it has to be called via its own Jobdrivers//have the resource offset be done directly in that jobgiver
        //putting it here as it can be call by every giver
        //For now making the amount restored based on mass. Will 100% need to be tweaks and might need to be a different stat
        public override void Notify_IngestedThing(Thing thing, int numTaken)
        {
            ThingDef metalDef = null;
            int count = numTaken;
            //whacky idea but it would be amusing if they could eat forged/crafted metal objects as well
            if (!thing.def.IsStuff)
            {
                if(thing.Stuff?.IsMetal ?? false)
                {
                    metalDef = thing.Stuff;
                    count = thing.def.CostStuffCount;
                }
                else
                {
                    foreach (var resource in thing.def.CostList)
                    {
                        if (resource.thingDef.IsMetal && resource.count >= count) //Just taking what ever is the most abundant metal
                        {
                            count = resource.count;
                            metalDef = resource.thingDef;
                        }
                    }
                }
            }
            else
            {
                metalDef = thing.def;
            }
            //No null check here should be confirmed before the job is even created
            float resourceRestore = metalDef.statBases.First(x => x.stat == StatDefOf.Mass).value /10;//dividing by 10 so 1 steel is 0.05 "nutrition"
            resourceRestore *= count;
            Value += resourceRestore;
            //Hemogen has a stat factor to change how much is gained. Not going to add one right now as it would depend on implementation of gene
            //GeneResourceDrainUtility.OffsetResource(this, resourceRestore); //**This utility is not useful I dont think. theres some weirdness with Gene_HemogenDrain vs Gene_Hemogen that I dont quite understand. I think HemogenDrain is just where the hediff is added for not having enough. Which is kind of weird but either way all this utility does is do what I'm doing then calls something that adds specifically the hemogen drain hediff with no additional options
            //This also means adding a hediff/doing things based on the tick resource drain is a bit of a pain, or rather just has to be handled in this tick
        }
        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos()) //Base adds the actual gizmo, would want to not use base if new resource is more complicated then hemogen bar
            {
                yield return gizmo;
            }
            foreach(Gizmo gizmo in GeneResourceDrainUtility.GetResourceDrainGizmos(this)) //This utility is to add Dev gizmos only
            {
                yield return gizmo;
            }
            yield break;
        }
    }
}
