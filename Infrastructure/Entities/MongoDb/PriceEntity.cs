namespace Infrastructure.Entities.MongoDb
{
    public class PriceEntity
    {
        public string Price { get; set; } = null!;
        public string? DiscountPrice { get; set; }
    }
}
