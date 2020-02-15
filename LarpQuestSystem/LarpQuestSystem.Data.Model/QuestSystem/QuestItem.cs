namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class QuestItem
    {
        public int Id { get; set; }
        public int AmountNeeded { get; set; }
        public bool IsReady { get; set; }
        public string TechnicalDocumentForNpc { get; set; }
        public bool IsTechnicalDocumentReady { get; set; }
        public int QuestId { get; set; }
        public int ItemId { get; set; }
        public int StartingNpcId { get; set; }
        public virtual Quest Quest { get; set; }
        public virtual Item Item { get; set; }
        public virtual Npc StartingNpc { get; set; }
    }
}
