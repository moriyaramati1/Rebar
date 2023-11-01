using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Rebar.Models
{
    public class Shake
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Guid uId { set; get; }
        public string name { set; get; }
        public string description { set; get; }
        public record struct Sizes(double small, double medium, double large);
        public Sizes sizesPrice { set; get; }
        public double totalPrice { set; get; }

        public Shake()
        {
            uId = Guid.NewGuid();

        }
        public Shake(string name, string description, double smallPrice = 20.0, double mediumPrice = 30.0, double largePrice = 40.0)
        {
            uId = Guid.NewGuid();
            this.name = name;
            this.description = description;
            sizesPrice = new Sizes(smallPrice, mediumPrice, largePrice);

        }

        public Shake(string name, string description)
        : this(name, description, 20.0, 30.0, 40.0)
        {
        }
    }

}

