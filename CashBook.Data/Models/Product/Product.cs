namespace CashBook.Data
{
    public class Product : EntityBase<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}