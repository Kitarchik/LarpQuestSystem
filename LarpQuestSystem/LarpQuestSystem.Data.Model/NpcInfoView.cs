using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class NpcInfoView
    {
        public Npc Npc { get; set; }
        public Location Location { get; set; }
        public List<Quest> StartingQuests { get; set; }
        public List<Quest> EndingQuests { get; set; }
        public List<QuestItem> QuestItems { get; set; }
    }
}
