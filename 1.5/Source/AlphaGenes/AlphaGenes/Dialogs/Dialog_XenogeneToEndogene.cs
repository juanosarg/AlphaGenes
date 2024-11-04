using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using AlphaGenes;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace AlphaMemes
{
    [StaticConstructorOnStartup]
    public class Dialog_XenogeneToEndogene : Window
    {

        public Pawn pawn;
        public Thing tool;
        private Vector2 scrollPosition = new Vector2(0, 0);
        public int columnCount = 8;
        List<GeneDef> genes = new List<GeneDef>();


        public Dialog_XenogeneToEndogene(Pawn pawn, Thing tool)
        {
            this.pawn = pawn;
            this.tool = tool;
            doCloseX = true;
            doCloseButton = true;
            closeOnClickedOutside = true;
            foreach(Gene gene in pawn.genes.Xenogenes)
            {
                genes.Add(gene.def);
            }

        }

        public override Vector2 InitialSize => new Vector2(620f, 500f);



        public override void DoWindowContents(Rect inRect)
        {
            Text.Font = GameFont.Small;
            var outRect = new Rect(inRect);
            outRect.yMin += 30f;
            outRect.yMax -= 40f;


           if(pawn.genes != null) { 

                Widgets.Label(new Rect(0, 10, 300f, 30f), "AG_ChooseXenogeneToConvert".Translate());

                var viewRect = new Rect(0f, 30f, outRect.width - 16f, (genes.Count / columnCount) * 64 + 64);


                Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);

                
                               
                for (var i = 0; i < genes.Count; i++)
                {


                    Rect rectIcon = new Rect((64 * (i % columnCount)) + 10, (64 * (i / columnCount)) + 100, 64, 64);
                   
                    
                    GUI.DrawTexture(rectIcon, genes[i].Icon, ScaleMode.StretchToFill, alphaBlend: true, 0f, genes[i].IconColor, 0f, 0f);
                    if (Widgets.ButtonInvisible(rectIcon))
                    {

                        for (int j = 0; j < 20; j++)
                        {
                            IntVec3 c;
                            CellFinder.TryFindRandomReachableCellNearPosition(pawn.Position, pawn.Position, pawn.Map, 2, TraverseParms.For(TraverseMode.NoPassClosedDoors, Danger.Deadly, false), null, null, out c);
                            FilthMaker.TryMakeFilth(c, pawn.Map, ThingDefOf.Filth_Blood);

                        }

                        Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(InternalDefOf.AG_GeneRemovalComa);

                        if (hediff == null)
                        {
                            
                            
                            if (AlphaGenes_Mod.settings.AG_GeneRemovalComa)
                            {
                                pawn.health.AddHediff(InternalDefOf.AG_GeneRemovalComa);
                            }
                            pawn.genes.Xenogenes.Remove(pawn.genes.GetGene(genes[i]));
                            pawn.genes.AddGene(genes[i], false);
                            tool.Destroy();
                            
                        }

                        Close();


                    }
                    TooltipHandler.TipRegion(rectIcon, genes[i].LabelCap);



                }



                Widgets.EndScrollView();


            }

        }
    }
}