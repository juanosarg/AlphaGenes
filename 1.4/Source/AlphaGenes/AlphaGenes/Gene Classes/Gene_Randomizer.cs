
using RimWorld;
using Verse;
using System.Collections.Generic;
using System.IO;



namespace AlphaGenes
{

	public class Gene_Randomizer : Gene
	{
		public List<CustomXenotype> xenotypes = new List<CustomXenotype>();

		public override void PostAdd()
		{
			base.PostAdd();

				
			foreach (FileInfo allCustomXenotypeFile in GenFilePaths.AllCustomXenotypeFiles)
			{
				string filePath = Path.Combine(FolderUnderSaveData("Xenotypes"), allCustomXenotypeFile.Name);
			
				PreLoadUtility.CheckVersionAndLoad(filePath, ScribeMetaHeaderUtility.ScribeHeaderMode.Xenotype, delegate
				{
					if (GameDataSaveLoader.TryLoadXenotype(filePath, out CustomXenotype xenotype))
					{
						xenotypes.Add(xenotype);
					}
					

				},true);
			}
			pawn.genes.SetXenotype(XenotypeDefOf.Baseliner);

			if (xenotypes.Count > 0)
            {
				CustomXenotype xenotype = xenotypes.RandomElement();

				if (!xenotype.genes.Contains(InternalDefOf.AlphaGenes_ExoticOrganism))
				{
                    pawn.genes.xenotypeName = xenotype.name;
                    pawn.genes.iconDef = xenotype.IconDef;
                    foreach (GeneDef geneDef in xenotype.genes)
                    {
                        if (geneDef != InternalDefOf.AlphaGenes_Randomizer)
                        {
                            pawn.genes.AddGene(geneDef, !xenotype.inheritable);
                        }

                    }
                }

				
				



            }
            
			pawn.genes.RemoveGene(this);






		}

		public static string FolderUnderSaveData(string folderName)
		{
			string text = Path.Combine(GenFilePaths.SaveDataFolderPath, folderName);
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			if (!directoryInfo.Exists)
			{
				directoryInfo.Create();
			}
			return text;
		}


	}
}
