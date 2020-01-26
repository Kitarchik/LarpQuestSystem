namespace LarpQuestSystem.Data.Model
{
    public enum QuestComplexity
    {
        [StringValue("Очень простой")]
        VeryEasy = 0,
        [StringValue("Простой")]
        Easy = 1,
        [StringValue("Обычный")]
        Normal = 2,
        [StringValue("Сложный")]
        Hard = 3,
        [StringValue("Очень сложный")]
        VeryHard = 4,
    }
}
