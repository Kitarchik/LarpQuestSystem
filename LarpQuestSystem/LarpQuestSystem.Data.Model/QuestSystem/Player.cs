using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPayedAccounts { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<QuestPlayer> QuestPlayers { get; set; }
    }
}
