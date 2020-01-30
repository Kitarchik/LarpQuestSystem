using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
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
