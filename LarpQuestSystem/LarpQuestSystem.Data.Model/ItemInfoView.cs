using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class ItemInfoView
    {
        public Item Item { get; set; }
        public List<QuestItem> QuestItems { get; set; }
    }
}
