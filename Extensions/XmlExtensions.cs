using glossary.Services;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace glossary.Extensions {
    public static class XmlExtensions {
        public static GlossaryCollection Deserialize(this XDocument doc) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GlossaryCollection));
            using (var reader = doc.Root.CreateReader()) {
                return (GlossaryCollection)xmlSerializer.Deserialize(reader);
            }
        }
        public static XDocument Serialize(this XDocument doc, GlossaryCollection glossary) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GlossaryCollection));
            using (var writer = doc.CreateWriter()) {
                xmlSerializer.Serialize(writer, glossary);
            }
            return doc;
        }
    }

}