using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
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
