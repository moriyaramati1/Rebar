namespace Rebar.Models
{
    public struct OrderItem
    {
        public readonly Guid id;
        public readonly string size;
        public readonly double price;
        public readonly double? discount;
    }
}

