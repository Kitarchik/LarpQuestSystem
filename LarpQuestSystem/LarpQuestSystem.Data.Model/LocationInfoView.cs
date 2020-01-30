using System;
using System.Collections.Generic;
using System.Text;

namespace LarpQuestSystem.Data.Model
{
    public class LocationInfoView
    {
        public Location Location { get; set; }
        public List<Npc> Npcs { get; set; }
        public List<Player> Players { get; set; }
    }
}
