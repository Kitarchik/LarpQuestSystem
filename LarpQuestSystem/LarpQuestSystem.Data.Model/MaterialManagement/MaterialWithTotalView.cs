namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public class MaterialWithTotalView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitOfMeasure { get; set; }
        public int Price { get; set; }
        public int TotalOrdered { get; set; }
        public int TotalServed { get; set; }
        public int TotalPayed { get; set; }
    }
}
