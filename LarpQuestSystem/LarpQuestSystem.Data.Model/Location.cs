using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Npc> Npcs { get; set; }
    }
}
