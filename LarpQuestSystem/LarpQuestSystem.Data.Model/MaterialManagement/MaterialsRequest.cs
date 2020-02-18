using LarpQuestSystem.Data.Model.QuestSystem;
using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public class MaterialsRequest
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public bool IsPayed { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<SingleMaterialRequest> SingleMaterialRequests { get; set; }
    }
}