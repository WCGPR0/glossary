namespace glossary.Models
{
    public class GlossaryDatabaseSettings : IGlossaryDatabaseSettings
    {
        public string GlossaryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IGlossaryDatabaseSettings
    {
        string GlossaryCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}