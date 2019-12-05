using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace glossary.Models {
    /// <summary>
    /// A Glossary Model representing an entity / entry, an associated term and definition
    /// </summary>
    public class Glossary
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } //< Required ID for mapping Common Language Runtime (CLR) object to collection
        public string Term { get; set; } //< The term, a single word or short phrase
        public string Definition { get; set; } //< A paragraph of text that defines the term
    }
}