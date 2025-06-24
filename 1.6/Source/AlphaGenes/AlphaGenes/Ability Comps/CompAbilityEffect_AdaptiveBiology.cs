using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace AlphaGenes
{
    public class CompAbilityEffect_AdaptiveBiology : CompAbilityEffect
    {


        new public CompProperties_AdaptiveBiology Props
        {
            get
            {
                return (CompProperties_AdaptiveBiology)this.props;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {


                List<string> blackListedTraits = new List<string>();
                List<BlackListedTraitsDef> allBlackListedTraits = DefDatabase<BlackListedTraitsDef>.AllDefsListForReading;
                foreach (BlackListedTraitsDef individualList in allBlackListedTraits)
                {
                    blackListedTraits.AddRange(individualList.blackListedTraits);
                }
                               
                List<Trait> allTraitListWithoutBlacklist = pawn.story.traits.allTraits.Where(x => !blackListedTraits.Contains(x.def.defName)).ToList();

                List<Trait> traitsToRemove = new List<Trait>();

                int numberToRemove = Props.numberOfTraits;
                if(numberToRemove > allTraitListWithoutBlacklist.Count()) { numberToRemove = allTraitListWithoutBlacklist.Count(); }

                for (int i = 0; i < numberToRemove; i++)
                {
                    traitsToRemove.Add(allTraitListWithoutBlacklist.Where(x => !traitsToRemove.Contains(x)).RandomElement());                   
       
                }
                foreach (Trait trait in traitsToRemove) {
                    pawn.story.traits.RemoveTrait(trait);
                }

                for (int i = 0; i < Props.numberOfTraits; i++)
                {
                    TraitDef addedTrait = DefDatabase<TraitDef>.AllDefsListForReading.Where(x => !blackListedTraits.Contains(x.defName) && !pawn.story.traits.HasTrait(x)).RandomElement();

                    pawn.story.traits.GainTrait(new Trait(addedTrait, addedTrait.degreeDatas.RandomElement().degree, forced: true));

                }

            }
        }





    }
}