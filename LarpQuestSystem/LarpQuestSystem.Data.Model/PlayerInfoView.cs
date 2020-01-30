﻿using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model
{
    public class PlayerInfoView
    {
        public Player Player { get; set; }
        public Location Location { get; set; }
        public List<Quest> Quests { get; set; }
    }
}
