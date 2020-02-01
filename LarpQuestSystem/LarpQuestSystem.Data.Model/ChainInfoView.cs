using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class ChainInfoView
    {
        public Chain Chain { get; set; }
        public List<QuestChain> QuestChains { get; set; }
    }
}
