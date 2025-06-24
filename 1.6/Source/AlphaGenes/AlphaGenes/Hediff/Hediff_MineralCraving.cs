using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    public class Hediff_MineralCraving : HediffWithComps
    {
        public override string SeverityLabel => Severity.ToStringPercent();

        public override bool Visible => true;


    }
}
