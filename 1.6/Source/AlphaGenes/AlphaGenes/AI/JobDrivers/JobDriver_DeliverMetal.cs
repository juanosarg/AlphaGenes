using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using Verse.AI;
using UnityEngine;

namespace AlphaGenes
{
    public class JobDriver_DeliverMetal : JobDriver_HaulToCell
    {


        public Pawn Deliveree => job.targetC.Pawn;
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(Deliveree, job) && base.TryMakePreToilReservations(errorOnFailed);
        }


    }
}
