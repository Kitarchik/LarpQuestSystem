using LarpQuestSystem.Data.Model.Utilities;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public enum QuestComplexity
    {
        [StringValue("Очень простой (100 опыта)")]
        VeryEasy = 0,
        [StringValue("Простой (200 опыта)")]
        Easy = 1,
        [StringValue("Обычный (400 опыта)")]
        Normal = 2,
        [StringValue("Сложный (800 опыта)")]
        Hard = 3,
        [StringValue("Очень сложный (1500 опыта)")]
        VeryHard = 4,
    }
}
