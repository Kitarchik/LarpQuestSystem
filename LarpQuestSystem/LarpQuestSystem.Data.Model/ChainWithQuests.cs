using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class ChainWithQuests
    {
        public Chain Chain { get; set; }
        public List<Quest> Quests { get; set; }
    }
}
