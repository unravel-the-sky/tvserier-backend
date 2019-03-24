using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models
{
    public class TvShow
    {
        [BsonId]
        public string id { get; set; }

        [BsonElement("url")]
        public string url { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("type")]
        public string type { get; set; }

        [BsonElement("language")]
        public string language { get; set; }

        [BsonElement("genres")]
        public ICollection<string> genres { get; set; }

        [BsonElement("status")]
        public string status { get; set; }

        [BsonElement("runtime")]
        public int runtime { get; set; }

        [BsonElement("premiered")]
        public string premiered { get; set; }

        [BsonElement("officialSite")]
        public string officialSite { get; set; }

        [BsonElement("days")]
        public ICollection<string> days { get; set; }

        [BsonElement("rating")]
        public float rating { get; set; }

        [BsonElement("weight")]
        public int weight { get; set; }

        [BsonElement("network")]
        public string network { get; set; }

        [BsonElement("imageUrl")]
        public string imageUrl { get; set; }

        [BsonElement("summary")]
        public string summary { get; set; }

        [BsonElement("Episodes")]
        public ICollection<Episode> Episodes { get; set; }
    }
    public class Episode
    {
        [BsonId]
        public string id { get; set; }

        [BsonElement("url")]
        public string url { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("season")]
        public int season { get; set; }

        [BsonElement("number")]
        public int number { get; set; }

        [BsonElement("airtime")]
        public DateTime airtime { get; set; }

        [BsonElement("runtime")]
        public int runtime { get; set; }

        [BsonElement("imageUrl")]
        public string imageUrl { get; set; }

        [BsonElement("summary")]
        public string summary { get; set; }
    }
}