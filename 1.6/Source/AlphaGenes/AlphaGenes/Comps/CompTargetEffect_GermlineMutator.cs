﻿
using RimWorld;
using Verse;
using Verse.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.Sound;


namespace AlphaGenes
{
    public class CompTargetEffect_GermlineMutator : CompTargetEffect
    {




        public CompProperties_TargetEffect_GermlineMutator Props
        {
            get
            {
                return (CompProperties_TargetEffect_GermlineMutator)this.props;
            }
        }





        public override void DoEffectOn(Pawn user, Thing target)
        {


            if (user.IsColonistPlayerControlled && user.CanReserveAndReach(target, PathEndMode.Touch, Danger.Deadly))
            {
                Pawn pawn = target as Pawn;


                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_GeneRemovalComa);

                if (hediff == null)
                {
                    GeneDef geneToCreate = DefDatabase<GeneDef>.AllDefs.Where((GeneDef x) => x.exclusionTags?.Contains("AG_OnlyOnCharacterCreation") == false 
                    && !x.defName.Contains("VREA_") && x.prerequisite==null).RandomElement();
                    pawn.genes.AddGene(geneToCreate,false);
                    user.carryTracker.DestroyCarriedThing();
                    if (AlphaGenes_Mod.settings.AG_GeneRemovalComa)
                    {
                        pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
                    }
                    
                }





            }





        }





    }
}
