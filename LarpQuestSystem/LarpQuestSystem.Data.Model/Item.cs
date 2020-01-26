﻿using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AmountReady { get; set; }
        public virtual ICollection<QuestItem> QuestItems { get; set; }
    }
}