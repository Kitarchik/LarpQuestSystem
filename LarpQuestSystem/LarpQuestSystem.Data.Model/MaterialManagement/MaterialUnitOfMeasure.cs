using LarpQuestSystem.Data.Model.Utilities;

namespace LarpQuestSystem.Data.Model.MaterialManagement
{
    public enum MaterialUnitOfMeasure
    {
        [StringValue("м.")]
        Meters = 0,
        [StringValue("л.")]
        Litres = 1,
        [StringValue("м.кв.")]
        SquareMeters = 2,
        [StringValue("м.куб.")]
        CubicMeters = 3,
        [StringValue("шт.")]
        Pieces = 4,
    }
}
