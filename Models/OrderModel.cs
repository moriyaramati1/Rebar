using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Rebar.Models
{
    public class OrderModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime startTime { get; set; }
        public DateTime finishTime { get; set; }
        public List<Guid>? shakesId { get; set; }
        public double totalPrice { get; set; }
        public Guid orderId { get; set; }
        public string? customerName { set; get; }

    }
}
