namespace glossary.Models
{
    public class GlossaryDatabaseSettings : IGlossaryDatabaseSettings
    {
        public string DatabaseFilePath { get; set; }
    }

    public interface IGlossaryDatabaseSettings
    {
        string DatabaseFilePath { get; set; }
    }
}