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

                List<Trait> traitList = pawn.story.traits.allTraits;

                List<Trait> allTraitListWithoutBlacklist = traitList.Where(x => !blackListedTraits.Contains(x.def.defName)).ToList();

                List<Trait> traitsToRemove = new List<Trait>();

                for (int i = 0; i < Props.numberOfTraits; i++)
                {
                    Trait removedTrait = null;
                    if (allTraitListWithoutBlacklist.Count() > 0)
                    {
                        removedTrait = allTraitListWithoutBlacklist.RandomElement();
                    }

                    if (removedTrait != null)
                    {
                        pawn.story.traits.RemoveTrait(removedTrait);

                    }
                }

                

               

                TraitDef addedTrait = DefDatabase<TraitDef>.AllDefsListForReading.Where(x => !blackListedTraits.Contains(x.defName) && !pawn.story.traits.HasTrait(x)).RandomElement();

                pawn.story.traits.GainTrait(new Trait(addedTrait, addedTrait.degreeDatas.RandomElement().degree, forced: true));

            }
        }











    }
}