using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class QuestPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int QuestId { get; set; }
        public virtual Player Player { get; set; }
        public virtual Quest Quest { get; set; }
    }
}
