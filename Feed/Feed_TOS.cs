using MongoDB.Bson;

namespace Feed
{
    internal class Feed_TOS
    {
        public BsonObjectId _id { get; set; }
        public BsonDateTime localTime { get; set; }
        public BsonDateTime marketTime { get; set; }
        public string message { get; set; }
        public string symbol { get; set; }
        public string type { get; set; }
        public double price { get; set; }
        public int size { get; set; }
        public string source { get; set; }
        public string condition { get; set; }
        public string tick { get; set; }
        public string mmid { get; set; }
        public string sub_market_id { get; set; }
    }
}
