using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class QuestChain
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public int QuestId { get; set; }
        public int ChainId { get; set; }
        public virtual Quest Quest { get; set; }
        public virtual Chain Chain { get; set; }
    }
}
