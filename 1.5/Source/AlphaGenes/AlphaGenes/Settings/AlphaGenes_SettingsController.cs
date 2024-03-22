using RimWorld;
using UnityEngine;
using Verse;


namespace AlphaGenes
{




    public class AlphaGenes_Mod : Mod
    {
        public static AlphaGenes_Settings settings;

        public AlphaGenes_Mod(ModContentPack content) : base(content)
        {
            settings = GetSettings<AlphaGenes_Settings>();
        }
        public override string SettingsCategory() => "Alpha Genes";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.DoWindowContents(inRect);
        }





    }
}

