using System;
using System.Collections.Generic;
using System.Text;
using LarpQuestSystem.Data.Model.QuestSystem;

namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public class SingleMaterialRequestInfoView
    {
        public int Id { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityServed { get; set; }
        public string Customer { get; set; }
        public bool IsPayed { get; set; }
        public string LocationName { get; set; }
        public int MaterialsRequestId { get; set; }
    }
}
