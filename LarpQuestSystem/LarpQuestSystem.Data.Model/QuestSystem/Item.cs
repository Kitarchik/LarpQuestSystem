using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.QuestSystem
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemType { get; set; }
        public string PictureLink { get; set; }
        public int AmountReady { get; set; }
        public virtual ICollection<QuestItem> QuestItems { get; set; }
    }
}
