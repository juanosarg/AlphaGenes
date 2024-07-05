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
    public class CompPsychicAbsorption : CompAbilityEffect
    {

        private new CompProperties_PsychicAbsorption Props => (CompProperties_PsychicAbsorption)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            base.Apply(target, dest);
            Pawn pawn = target.Pawn;

            if (!pawn.Dead)
            {
                List<Thought> allThoughts = GetAllThoughts(pawn);
                Thought worstThought = allThoughts?.Where(x => (x as Thought_Memory != null && x.MoodOffset()>-40&& x.MoodOffset()<0)).OrderBy(x => x.MoodOffset())?.FirstOrFallback();
                if (worstThought != null && worstThought as Thought_Memory != null)
                {
                    pawn.needs?.mood?.thoughts?.memories?.RemoveMemory((Thought_Memory)worstThought);
                    this.parent.pawn.needs?.mood?.thoughts?.memories?.TryGainMemory(worstThought.def);
                }
                else
                {
                    Messages.Message("AG_PsychicAbsorptionNoMoods".Translate(pawn), pawn, MessageTypeDefOf.RejectInput, historical: false);
                }
            }
        }

        public List<Thought> GetAllThoughts(Pawn pawn)
        {
            List<Thought> outThoughts = new List<Thought>();
            pawn.needs?.mood?.thoughts?.GetAllMoodThoughts(outThoughts);
            return outThoughts;
        }


    }
}