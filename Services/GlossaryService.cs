using System.IO;
using glossary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System;
using glossary.Extensions;
using System.ComponentModel;

namespace glossary.Services
{
    public class GlossaryService : IDisposable
    {
        private readonly GlossaryCollection _glossary; //< Collection of all the Glossaries in-memory
        private readonly string DatabaseFilePath; //< Location of the database path file for loading/saving
        /** <summary>
            Constructor that loads the DatabaseFile from the DatabaseFilePath and serializes it into the GlossaryCollection.
            </summary>
        */
        private bool _disposed = false; //< Boolean flag to prevent double disposing
        public GlossaryService(IGlossaryDatabaseSettings settings)
        {
            DatabaseFilePath = settings.DatabaseFilePath;
            try
            {
                XDocument doc = XDocument.Load(DatabaseFilePath);
                _glossary = doc.Deserialize();
            }
            catch (FileNotFoundException)
            {
                _glossary = new GlossaryCollection(new List<Glossary>());
            }
        }

        /** <summary>
            A hacky approach to saving the data into XML, so that this will get called even when there are errors.
            Note: This is not ideal, since you want to check for write/save errors prior to the destructor call.
            </summary>
        */
        ~GlossaryService() {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    syncDBFile();
                }
                _disposed = true;
            }
        }
        /** <summary>
            Helper function to save and write to the DatabaseFile at the DatabaseFilePath
            </summary>
        */
        public int syncDBFile()
        {
            try
            {
                XDocument doc = new XDocument();
                doc.Serialize(_glossary);
                doc.Save(DatabaseFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // @todo Add more descriptive error message handling / logging
                return -1;
            }
            return 0;
        }

        /** <summary>
            Gets the max ID of all the Glossaries
            </summary>
            <returns>
            A sorted list of Glossary in ascending order
            </returns>    
        */
        public string GetMaxID() => _glossary.glossaryList.Max(x => x.Id);

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
            _glossary.Find(glossary => glossary.Id == id).FirstOrDefault();

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
            _glossary.Find(glossary => glossary.Term == term).FirstOrDefault();

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



    [XmlRoot]
    public class GlossaryCollection
    {
        public List<Glossary> glossaryList; //< List of In-Memory Glossaries
        public GlossaryCollection()
        {
            glossaryList = new List<Glossary>(); //< Empty list of Glossaries
        }
        public GlossaryCollection(List<Glossary> glossaryList)
        {
            this.glossaryList = glossaryList;
        }

        public FindGlossary Find(Func<Glossary, bool> filter)
        {
            FindGlossary findGlossary = new FindGlossary(glossaryList);
            return findGlossary.Find(filter);
        }
        /** <summary>
            Sorts the list of Glossaries by a keySelector
            </summary>
        */
        public IOrderedEnumerable<Glossary> SortBy(Func<Glossary, string> keySelector)
        {
            if (glossaryList == null)
                throw new ArgumentNullException("glossaryList");
            if (keySelector == null)
                throw new ArgumentNullException("keySelector");
            return glossaryList.OrderBy(keySelector);
        }
        /** <summary>
            Gets the First Glossary or the default Glossary from its default constructor
            </summary>
        */
        public Glossary FirstOrDefault()
        {
            using (IEnumerator<Glossary> iterator = glossaryList.GetEnumerator())
            {
                return iterator.MoveNext() ? iterator.Current : default(Glossary);
            }
        }
        /** <summary>
            Inserts a glossary at the end
            </summary>
        */
        public void InsertOne(Glossary glossary)
        {
            glossaryList.Add(glossary);
        }
        /** <summary>
            Replaces the Glossary matching the predicate
            </summary>
        */
        public GlossaryCollection ReplaceOne(Func<Glossary, bool> predicate, Glossary glossaryIn)
        {
            int index = IndexOf(predicate);
            glossaryList = glossaryList.Replace(index, glossaryIn).ToList();
            return this;
        }
        /** <summary>
            Deletes the Glossary matching the predicate
            </summary>
        */
        public GlossaryCollection DeleteOne(Func<Glossary, bool> predicate)
        {
            int index = IndexOf(predicate);
            glossaryList = glossaryList.Where((x, i) => index != i).ToList();
            return this;
        }
        /** <summary>
            Returns the location of the Glossary matching the predicate, 0 based
            </summary>
        */
        private int IndexOf(Func<Glossary, bool> predicate)
        {
            int current = 0;
            foreach (Glossary glossary in glossaryList)
            {
                if (predicate(glossary))
                    return current;
                ++current;
            }
            return current;
        }
    }

    /** <summary>
        A subclass of the GlossaryCollection used as a Fluent Interface design to allow method changing 
        </summary>
    */
    public class FindGlossary : GlossaryCollection
    {
        public FindGlossary(List<Glossary> glossaryList) : base(glossaryList)
        {
        }
        public new FindGlossary Find(Func<Glossary, bool> filter)
        {
            glossaryList = glossaryList.Where(filter ?? (s => true)).ToList();
            return this;
        }
    }

}