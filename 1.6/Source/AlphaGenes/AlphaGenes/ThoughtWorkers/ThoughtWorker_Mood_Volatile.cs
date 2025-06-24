


using Verse;
using System.Collections.Generic;
using System.Linq;
using RimWorld;


namespace AlphaGenes
{


    public class ThoughtWorker_Mood_Volatile : ThoughtWorker
    {

        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (StaticCollectionsClass.colonist_and_random_mood.ContainsKey(p))
            {

                switch (StaticCollectionsClass.colonist_and_random_mood[p])
                {

                    case 0:
                        return ThoughtState.ActiveAtStage(0);
                    case 1:
                        return ThoughtState.ActiveAtStage(1);
                    
                    default:
                        return ThoughtState.ActiveAtStage(0);



                }





            }
            else return false;







        }

    }
}

