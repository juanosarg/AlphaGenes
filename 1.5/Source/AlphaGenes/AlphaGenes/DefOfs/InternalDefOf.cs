
using RimWorld;
using Verse;


namespace AlphaGenes
{
	[DefOf]
	public static class InternalDefOf
	{
		public static ThingDef AG_Alphapack;
		public static ThingDef AG_Mixedpack;
        public static ThingDef AG_TemporaryBandNode;
        public static ThingDef AG_Filth_CryoVomitPermanent;
        public static ThingDef AG_Filth_CryoBloodPermanent;
        public static ThingDef AG_Filth_CryoResiduePermanent;

        public static PawnKindDef Rat;
		public static PawnKindDef Wolf_Timber;
		public static PawnKindDef Bear_Polar;

        public static SoundDef Hive_Spawn;


        public static AbilityDef AG_MinorSummon;

        public static ThoughtDef AG_PukedRats;
		public static ThoughtDef AG_Implanted;
		public static ThoughtDef AG_Implanted_Social;
        public static ThoughtDef AG_Parasite;
        public static ThoughtDef AG_Parasite_Social;
        public static ThoughtDef AG_RaidedBioLab;
        public static ThoughtDef AG_UsedXenotypeInjector;


        public static QuestScriptDef AG_OpportunitySite_AbandonedBiotechLab;

		public static GeneDef AG_BrittleBones;
		public static GeneDef AG_UnsettlingAppearance;
		public static GeneDef AG_LungRotStrength;
		public static GeneDef AG_FastGestation;
		public static GeneDef AG_SlowGestation;
		public static GeneDef AG_MetalEater;
		public static GeneDef AlphaGenes_Randomizer;
        public static GeneDef AG_Thalassophobia;
		public static GeneDef AG_BloodVomit;
		public static GeneDef AG_RatVomit;
		public static GeneDef AG_Beauty_Angelic;
        public static GeneDef AlphaGenes_ExoticOrganism;
		public static GeneDef AG_Male;
		public static GeneDef AG_Female;
		public static GeneDef AG_Sensitivity_Lethal;
		public static GeneDef AG_Teratogenesis;
		public static GeneDef AG_EldritchVisage;
        public static GeneDef AG_ExpertMechRepair;
        public static GeneDef AG_Rowdy;
        public static GeneDef AG_VolatileMood;
        public static GeneDef AG_PsychicRemapping;
        public static GeneDef AG_Teratophilia;
        public static GeneDef AG_HeatImmunity;


        public static GeneDef AG_MinorAnimalSummon_Randomizer;
        public static GeneDef AG_AnimalSummon_Randomizer;
        public static GeneDef AG_MajorAnimalSummon_Randomizer;

        public static GeneDef AG_UnstableMutation;
        public static GeneDef AG_UnstableMutationMajor;
        public static GeneDef AG_UnstableMutationCatastrophic;

        public static GeneDef AG_NoRaidPresence;
        public static GeneDef AG_ReducedRaidPresence;
        public static GeneDef AG_DangerousRaidPresence;
        public static GeneDef AG_DeadlyRaidPresence;


        public static XenotypeIconDef AG_CustomXenotypeIcon10;

        public static HediffDef AG_LungRotStrengthHediff;
		public static HediffDef AG_GeneRemovalComa;
		public static HediffDef AG_MineralCraving;
		public static HediffDef AG_MineralFueled;
		public static HediffDef AG_ReactiveArmourHediff;
		public static HediffDef AG_MineralOverdriveHediff;
		public static HediffDef AG_XenogerminationComa;
		public static HediffDef AG_TemporarySummon;
        public static HediffDef AG_ScrambledIFF;
        public static HediffDef AG_TemporarySummonMech;
        public static HediffDef AG_TempNodeEffect;
        public static HediffDef AG_IncreasedCommandRange;
        public static HediffDef AG_DecreasedCommandRange;
        public static HediffDef AG_MechRepairBoost;
        public static HediffDef AG_EndoimplantationComa;
        public static HediffDef AG_VFEI_Antenna;
        public static HediffDef AG_VFEI_VenomGland;
        public static HediffDef AG_VFEI_PredatorStomach;
        public static HediffDef HeartAttack;
        public static HediffDef AG_FreezingBreath;

        public static HediffDef AG_DevouredShooting;
        public static HediffDef AG_DevouredMelee;
        public static HediffDef AG_DevouredConstruction;
        public static HediffDef AG_DevouredMining;
        public static HediffDef AG_DevouredCooking;
        public static HediffDef AG_DevouredPlants;
        public static HediffDef AG_DevouredAnimals;
        public static HediffDef AG_DevouredCrafting;
        public static HediffDef AG_DevouredArtistic;
        public static HediffDef AG_DevouredMedical;
        public static HediffDef AG_DevouredSocial;
        public static HediffDef AG_DevouredIntellectual;    

        public static RulePackDef AG_NamerAlphapack;
		public static RulePackDef AG_NamerMixedpack;

		public static JobDef AG_ConsumeMetal;
		public static JobDef AG_FeedMetal;
		public static JobDef AG_DeliverMetal;

        public static BodyPartDef Torso;
        public static BodyPartDef Shoulder;
        public static BodyPartDef Arm;
        public static BodyPartDef Hand;
        public static BodyPartDef Finger;
        public static BodyPartDef Toe;
        public static BodyPartDef Ear;
        public static BodyPartDef Head;
        public static BodyPartDef Nose;
        public static BodyPartDef Neck;
        public static BodyPartDef Leg;
        public static BodyPartDef Foot;
        public static BodyPartDef Tongue;

        public static MentalStateDef AG_SelectiveManhunter;

        [MayRequire("sarg.alphamechs")]
		[MayRequireBiotech]
        public static ThingDef AM_WarEmpress;
        [MayRequire("sarg.alphamechs")]
        [MayRequireBiotech]
        public static ThingDef AM_Infernus;
        [MayRequire("sarg.alphamechs")]
        [MayRequireBiotech]
        public static ThingDef AM_Apoptosis;      
        [MayRequireBiotech]
        public static ThingDef Mech_Warqueen;      
        [MayRequireBiotech]
        public static ThingDef Mech_Diabolus;

        [MayRequireAnomaly]
        public static ThingDef Bioferrite;

        static InternalDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(InternalDefOf));
		}
	}
}
