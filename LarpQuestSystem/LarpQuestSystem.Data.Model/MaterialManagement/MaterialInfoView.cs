using System.Collections.Generic;

namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public class MaterialInfoView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int AmountPerPayedRequestInLocation { get; set; }
        public List<SingleMaterialRequestInfoView> SingleMaterialRequestInfoViews { get; set; }
    }
}
