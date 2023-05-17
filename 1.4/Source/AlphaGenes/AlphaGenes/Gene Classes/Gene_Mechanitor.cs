
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.IO;



namespace AlphaGenes
{

	public class Gene_Mechanitor : Gene
	{
		public override bool Active
		{
			get
			{
				if (pawn!=null&&!MechanitorUtility.IsMechanitor(pawn))
				{
					return false;
				}
				if (Overridden)
				{
					return false;
				}
				if (pawn?.ageTracker != null && (float)pawn.ageTracker.AgeBiologicalYears < def.minAgeActive)
				{
					return false;
				}
				return true;
			}
		}

	}
}
