using LarpQuestSystem.Data.Model.Utilities;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public enum QuestGrade
    {
        [StringValue("Микроквест")]
        MicroQuest = 0,
        [StringValue("Средний квест")]
        AverageQuest = 1,
        [StringValue("Макро квест")]
        MacroQuest = 2,
    }
}
