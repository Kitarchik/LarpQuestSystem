using System.Collections.Generic;
using LarpQuestSystem.Data.Model.MaterialManagement;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RequestsPayed { get; set; }
        public virtual ICollection<Npc> Npcs { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<MaterialsRequest> MaterialsRequests { get; set; }
    }
}
