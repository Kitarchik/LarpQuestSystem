namespace LarpQuestSystem.Data.Model.QuestSystem
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
