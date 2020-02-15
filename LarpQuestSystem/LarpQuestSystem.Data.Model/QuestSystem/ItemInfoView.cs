using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class ItemInfoView
    {
        public Item Item { get; set; }
        public List<QuestItem> QuestItems { get; set; }
    }
}
