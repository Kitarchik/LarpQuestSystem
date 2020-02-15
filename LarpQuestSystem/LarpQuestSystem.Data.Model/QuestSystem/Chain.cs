using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class Chain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<QuestChain> QuestChains { get; set; }
    }
}
