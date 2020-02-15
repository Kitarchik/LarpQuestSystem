using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class LocationInfoView
    {
        public Location Location { get; set; }
        public List<Npc> Npcs { get; set; }
        public List<Player> Players { get; set; }
    }
}
