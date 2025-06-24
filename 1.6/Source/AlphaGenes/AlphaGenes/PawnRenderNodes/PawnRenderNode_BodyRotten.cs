
using RimWorld;
using UnityEngine;
using Verse;
namespace AlphaGenes
{
    public class PawnRenderNode_BodyRotten : PawnRenderNode
    {
        public PawnRenderNode_BodyRotten(Pawn pawn, PawnRenderNodeProperties props, PawnRenderTree tree)
            : base(pawn, props, tree)
        {
        }

        public override Graphic GraphicFor(Pawn pawn)
        {
            Shader shader = ShaderFor(pawn);
            if (shader == null)
            {
                return null;
            }

            Graphic baseGraphic= GraphicDatabase.Get<Graphic_Multi>(pawn.story.bodyType.bodyNakedGraphicPath, shader, Vector2.one, ColorFor(pawn));

            return baseGraphic.GetColoredVersion(ShaderDatabase.Cutout, MutantUtility.GetShamblerColor(baseGraphic.Color), MutantUtility.GetShamblerColor(baseGraphic.ColorTwo));




           
        }

       /* public override Graphic GraphicFor(Pawn pawn)
        {
            PawnKindLifeStage curKindLifeStage = pawn.ageTracker.CurKindLifeStage;


            Graphic graphic = ((pawn.gender == Gender.Female && curKindLifeStage.femaleGraphicData != null) ? curKindLifeStage.femaleGraphicData.Graphic : curKindLifeStage.bodyGraphicData.Graphic);
            if (curKindLifeStage.corpseGraphicData != null)
            {
                graphic = ((pawn.gender == Gender.Female && curKindLifeStage.femaleCorpseGraphicData != null) ? curKindLifeStage.femaleCorpseGraphicData.Graphic.GetColoredVersion(curKindLifeStage.femaleCorpseGraphicData.Graphic.Shader, graphic.Color, graphic.ColorTwo) : curKindLifeStage.corpseGraphicData.Graphic.GetColoredVersion(curKindLifeStage.corpseGraphicData.Graphic.Shader, graphic.Color, graphic.ColorTwo));
            }



            switch (pawn.Drawer.renderer.CurRotDrawMode)
            {
                case RotDrawMode.Fresh:




                case RotDrawMode.Rotting:
                    return graphic.GetColoredVersion(ShaderDatabase.Cutout, PawnRenderUtility.GetRottenColor(graphic.Color), PawnRenderUtility.GetRottenColor(graphic.ColorTwo));
                case RotDrawMode.Dessicated:
                    if (curKindLifeStage.dessicatedBodyGraphicData != null)
                    {
                        Graphic graphic2;
                        if (pawn.RaceProps.FleshType != FleshTypeDefOf.Insectoid)
                        {
                            graphic2 = ((pawn.gender == Gender.Female && curKindLifeStage.femaleDessicatedBodyGraphicData != null) ? curKindLifeStage.femaleDessicatedBodyGraphicData.GraphicColoredFor(pawn) : curKindLifeStage.dessicatedBodyGraphicData.GraphicColoredFor(pawn));
                        }
                        else
                        {
                            Color dessicatedColorInsect = PawnRenderUtility.DessicatedColorInsect;
                            graphic2 = ((pawn.gender == Gender.Female && curKindLifeStage.femaleDessicatedBodyGraphicData != null) ? curKindLifeStage.femaleDessicatedBodyGraphicData.Graphic.GetColoredVersion(ShaderDatabase.Cutout, dessicatedColorInsect, dessicatedColorInsect) : curKindLifeStage.dessicatedBodyGraphicData.Graphic.GetColoredVersion(ShaderDatabase.Cutout, dessicatedColorInsect, dessicatedColorInsect));
                        }
                        if (pawn.IsMutant)
                        {
                            graphic2.ShadowGraphic = graphic.ShadowGraphic;
                        }

                        return graphic2;
                    }
                    break;
            }
            return null;
        }*/
    }
}