using MongoDB.Bson;

namespace Feed
{
    internal class Feed_L2
    {
        //Wpisywanie od razu po otrzymaniu feeda
        public BsonObjectId _id { get; set; }
        public BsonDateTime localTime { get; set; }
        public BsonDateTime marketTime { get; set; }
        public string message { get; set; }
        public string symbol { get; set; }
        public string mmid { get; set; }
        public string side { get; set; }
        public double price { get; set; }
        public int volume { get; set; }
        public int depth { get; set; }
        public int sequenceNumber { get; set; }       
    }
}
