using LarpQuestSystem.Data.Model.Utilities;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public enum ItemType
    {
        [StringValue("Без типа")]
        NoType=0,
        [StringValue("Артефакт")]
        Artifact =1,
    }
}
