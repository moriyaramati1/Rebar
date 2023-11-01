using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Rebar.Models
{
    public class ReportModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime date { get; set; }
        public int ordersAmount { get; set; }
        public double totalDailyIncome { set; get; }

    }    
}

