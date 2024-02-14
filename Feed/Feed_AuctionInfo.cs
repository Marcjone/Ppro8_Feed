using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feed
{
    internal class Feed_AuctionInfo
    {
        public BsonObjectId _id { get; set; }
        public BsonDateTime localTime { get; set; }
        public string marketTime { get; set; }
        public string message { get; set; }
        public string symbol { get; set; }
        public double indicativePrice { get; set; }
        public int indicativeVolume { get; set; }
        public string auctionType { get; set; }
    }
}
