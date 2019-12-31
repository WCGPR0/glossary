using System.Xml.Serialization;
using System;
using glossary.Services;

namespace glossary.Models {
    /// <summary>
    /// A Glossary Model representing an entity / entry, an associated term and definition
    /// </summary>
    [Serializable]
    public class Glossary
    {
        [XmlAttribute("ID", DataType = "string")]
        public string Id { get; set; } //< Required ID for mapping Common Language Runtime (CLR) object to collection (Unnecessary for non-database storage)
        public string Term { get; set; } //< The term, a single word or short phrase
        public string Definition { get; set; } //< A paragraph of text that defines the term
    }
}