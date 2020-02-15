using System;
using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class Quest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ArtisticTextLink { get; set; }
        public bool IsArtisticTextReady { get; set; }
        public string TechnicalDescriptionLink { get; set; }
        public bool IsTechnicalDescriptionReady { get; set; }
        public int AmountToPrint { get; set; }
        public bool IsPrinted { get; set; }
        public int Complexity { get; set; }
        public int Grade { get; set; }
        public bool IsCompleted { get; set; }
        public string CompletionComment { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int QuestGiverId { get; set; }
        public virtual Npc QuestGiver { get; set; }
        public int QuestEndingId { get; set; }
        public virtual Npc QuestEnding { get; set; }
        public virtual ICollection<QuestItem> QuestItems { get; set; }
        public virtual ICollection<QuestChain> QuestChains { get; set; }
        public virtual ICollection<QuestPlayer> QuestPlayers { get; set; }
    }
}
