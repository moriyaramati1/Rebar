namespace Rebar.Models
{
    public enum SizeUnit { small,medium,large}
    public struct OrderItem
    {
        public Guid id { get; set; }
        public SizeUnit size { get; set; }
        public double price { get; set; }
        public double? discount { get; set; }

    }
}

