using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class Chain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<QuestChain> QuestChains { get; set; }
    }
}
