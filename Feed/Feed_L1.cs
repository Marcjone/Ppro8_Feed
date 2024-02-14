using MongoDB.Bson;

namespace Feed
{
    internal class Feed_L1
    {
        public BsonObjectId _id { get; set; }
        public BsonDateTime localTime { get; set; }
        public BsonDateTime marketTime { get; set; }
        public string message { get; set; }
        public string symbol { get; set; }
        public double bidPrice { get; set; }
        public int bidSize { get; set; }
        public double askPrice { get; set; }
        public int askSize { get; set; }
        public string tick { get; set; }
    }
}
