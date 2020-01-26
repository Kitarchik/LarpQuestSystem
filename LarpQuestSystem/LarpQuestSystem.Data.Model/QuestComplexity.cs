namespace LarpQuestSystem.Data.Model
{
    public enum QuestComplexity
    {
        [StringValue("Очень простой (100 опыта)")]
        VeryEasy = 0,
        [StringValue("Простой (200 опыта)")]
        Easy = 1,
        [StringValue("Обычный (600 опыта)")]
        Normal = 2,
        [StringValue("Сложный (1000 опыта)")]
        Hard = 3,
        [StringValue("Очень сложный (2000 опыта)")]
        VeryHard = 4,
    }
}
