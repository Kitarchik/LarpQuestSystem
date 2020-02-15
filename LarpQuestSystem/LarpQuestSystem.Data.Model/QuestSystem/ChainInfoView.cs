using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class ChainInfoView
    {
        public Chain Chain { get; set; }
        public List<QuestChain> QuestChains { get; set; }
    }
}
