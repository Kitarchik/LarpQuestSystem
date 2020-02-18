namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public class SingleMaterialRequest
    {
        public int Id { get; set; }
        public int QuantityOrdered { get; set; }
        public int QuantityServed { get; set; }
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
        public int MaterialsRequestId { get; set; }
        public virtual MaterialsRequest MaterialsRequest { get; set; }
    }
}
