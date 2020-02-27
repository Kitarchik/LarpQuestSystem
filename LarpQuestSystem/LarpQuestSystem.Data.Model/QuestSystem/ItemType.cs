using LarpQuestSystem.Data.Model.Utilities;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public enum ItemType
    {
        [StringValue("Без типа")]
        NoType=0,
        [StringValue("Артефакт")]
        Artifact =1,
        [StringValue("Квестовый предмет")]
        SimpleItem = 2,
        [StringValue("Квестовая информация")]
        QuestInfo = 3,
    }
}
