using glossary.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace glossary.Services
{
    public class GlossaryService
    {
        private readonly IMongoCollection<Glossary> _glossary;

        public GlossaryService(IGlossaryDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _glossary = database.GetCollection<Glossary>(settings.GlossaryCollectionName);
        }

        /** <summary>
            Gets all the Glossary and sorts it by the Term in ascending order
            </summary>
            <returns>
            A sorted list of Glossary in ascending order
            </returns>    
        */
        public List<Glossary> Get() =>
            _glossary.Find(glossary => true).SortBy(glossary => glossary.Term).ToList();

        /** <summary>
            Finds the first Glossary matching the id. Note: ID should be uniquely constrainted seperately
            </summary>
            <param name="id">
            The unique ID of the Glossary to find
            </param>
            <returns>
            The first Glossary that matches the ID
            </returns>    
        */
        public Glossary Get(string id) =>
            _glossary.Find<Glossary>(glossary => glossary.Id == id).FirstOrDefault();

        /** <summary>
            Finds the first Glossary matching the term. Note: Term should be uniquely constrainted seperately
            </summary>
            <param name="term">
            The unique term of the Glossary to find
            </param>
            <returns>
            The first Glossary that matches the term
            </returns>    
        */
        public Glossary Find(string term) =>
            _glossary.Find<Glossary>(glossary => glossary.Term == term).FirstOrDefault();

        /** <summary>
            Creates the Glossary
            </summary>
            <param name="glossary">
            The Glossary that is to be created in the DB
            </param>
            <returns>
            The Glossary object
            </returns>    
        */
        public Glossary Create(Glossary glossary)
        {
            _glossary.InsertOne(glossary);
            return glossary;
        }

        /** <summary>
            Updates the Glossary
            </summary>
            <param name="id">
            The unique ID of the Glossary to update
            </param>
            <param name="glossaryIn">
            The new Glossary that will replace the old Glossary
            </param>
        */
        public void Update(string id, Glossary glossaryIn) =>
            _glossary.ReplaceOne(glossary => glossary.Id == id, glossaryIn);

        /** <summary>
            Deletes a Glossary object
            </summary>
            <param name="glossaryIn">
            The Glossary object to be deleted
            </param>
        */
        public void Remove(Glossary glossaryIn) =>
            _glossary.DeleteOne(glossary => glossary.Id == glossaryIn.Id);

        /** <summary>
            Deletes a Glossary object
            </summary>
            <param name="id">
            The unique ID of the Glossary to be deleted
            </param>
        */
        public void Remove(string id) =>
            _glossary.DeleteOne(glossary => glossary.Id == id);
    }
}