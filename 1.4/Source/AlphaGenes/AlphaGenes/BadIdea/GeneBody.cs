using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;


namespace AlphaGenes
{
    public class GeneBody : IExposable
    {
        public GeneBody(Pawn pawn)
        {
            this.pawn = pawn;
        }
        public BodyDef BodyDef => cachedBodyDef;
        public Pawn pawn;
        public string bodyDefSaveID;
        public BodyDef BodyDefFullCopy(BodyDef body)
        {
            //do the copy
            BodyDef newBody = new();
            newBody.corePart = RebuildPartsRecursive(body.corePart);
            newBody.generated = true;
            newBody.label = "BadFuckingIdeaHuman";
            newBody.ResolveReferences();
            return newBody;
        }
        public BodyPartRecord RebuildPartsRecursive(BodyPartRecord oldPart, BodyPartRecord parent = null)
        {
            BodyPartRecord newPart = new();
            newPart.parent=parent;
            Traverse.IterateFields(oldPart, newPart, (trv1, trv2) =>
            {
                var value = trv1.GetValue();
                if (value is List<BodyPartRecord> list)
                {
                    List<BodyPartRecord> newParts = new();
                    if (list.Count > 0)
                    {                        
                        for (int i = 0; i < list.Count; i++)
                        {
                            newParts.Add(RebuildPartsRecursive(list[i],newPart));
                        }
                    }
                    value = newParts;
                }
                trv2.SetValue(value);
            });
            return newPart;
        }

        public void GenerateBodyDef()
        {
            List<GeneDef> bodyPartGenes = new();
            bool isGeneBody = false;
            if(pawn.genes != null)
            {
                foreach (var gene in pawn.genes?.GenesListForReading)
                {
                    var def = gene.def;
                    var ext = def.GetModExtension<GeneExtensionBadIdea>();
                    if (ext != null && ext.attachTo != null)
                    {
                        isGeneBody = true;
                        bodyPartGenes.Add(def);
                    }
                }
            }
            if (isGeneBody)
            {
                var body = BodyDefFullCopy(BodyDefOf.Human);       
                bodyDefSaveID = body.defName + "_" + pawn.GetUniqueLoadID();
                body.defName = bodyDefSaveID;
                foreach(var def in bodyPartGenes)
                {
                    var ext = def.GetModExtension<GeneExtensionBadIdea>();
                    foreach(var part in ext.newParts)
                    {
                        AddPartToBody(body, ext.attachTo, part);
                    }                    
                }
                cachedBodyDef = body;
            }
            else
            {
                cachedBodyDef = BodyDefOf.Human;
            }
        }
        public void AddPartToBody(BodyDef body, BodyPartDef attachTo, BodyPartRecord newPart)
        {
            foreach(var part in body.AllParts.ToList())
            {
                if(part.def == attachTo)
                {
                    part.parts.Add(newPart);
                    body.AllParts.Add(newPart);
                    if(newPart.def.frostbiteVulnerability > 0f)
                    {
                        body.AllPartsVulnerableToFrostbite.Add(newPart);
                    }
                }
            }
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref bodyDefSaveID, "bodyDefPlaceholder");
            Scribe_References.Look(ref pawn, "pawn");
            
        }

        private BodyDef cachedBodyDef;
    }
}
