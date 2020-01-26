using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class Npc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Quest> StartingQuests { get; set; }
        public virtual ICollection<Quest> EndingQuests { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
