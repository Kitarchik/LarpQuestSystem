using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class QuestInfoView
    {
        public Quest Quest { get; set; }
        public Npc QuestGiver { get; set; }
        public Npc QuestEnding { get; set; }
        public List<Chain> Chains { get; set; }
        public List<Player> Players { get; set; }
        public List<QuestItem> QuestItems { get; set; }
    }
}
