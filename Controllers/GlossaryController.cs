using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using glossary.Services;
using System.Linq;
using glossary.Models;

namespace glossary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GlossaryController : Controller
    {
        private readonly GlossaryService _glossaryService; //< Instance of GlossaryService to performs CRUD operations
        public GlossaryController(GlossaryService _glossaryService)
        {
            this._glossaryService = _glossaryService;
        }
        /// <summary>
        /// Gets a list of all the terms stored in the Persistent Data Storage and returns them in alphabetical order
        /// </summary>
        /// <returns>
        /// A list of terms in alphabetical order
        /// </returns>
        [HttpGet("[action]")]
        public IEnumerable<String> GetTerms()
        {
            List<string> terms = _glossaryService.Get() //< Gets all the glosarries
                                    .Select(_glossary => _glossary.Term) //< Selects only the term
                                    .ToList(); //< Converts to a list
            return terms;
        }
        /// <summary>
        /// Gets the definition for a specific term
        /// </summary>
        /// <param name="term">
        /// The term that is being searched for
        /// </param>
        /// <returns>
        /// A string representation of the definition for the term, or an empty string if no found
        /// </returns>
        [HttpGet("[action]")]
        public string GetDefinition(string term)
        {
            Glossary glossary = _glossaryService.Find(term);
            if (glossary == null)
                return String.Empty;
            else
                return glossary.Definition;
        }

        /// <summary>
        /// Updates the definition for a specific term, or deletes the term if it's empty
        /// </summary>
        /// <param name="term">
        /// The term that is being updated
        /// </param>
        /// <returns>
        /// A return code, 0 being success, and -1 being an error has occured
        /// </returns>
        [HttpPut("{id:length(24)}")]
        public int UpdateTerm(Glossary glossary)
        {
            if (glossary == null)
                return -1;
            string defintion = glossary.Definition;
            if (String.IsNullOrEmpty(glossary.Definition))
                return DeleteTerm(glossary);
            else
                _glossaryService.Update(glossary.Id, glossary);
            return 0;
        }

        /// <summary>
        /// Deletes the term
        /// </summary>
        /// <param name="term">
        /// The term that is being deleted
        /// </param>
        /// <returns>
        /// A return code, 0 being success, and -1 being an error has occured
        /// </returns>
        [HttpDelete("{id:length(24")]
        public int DeleteTerm(Glossary glossary)
        {
            try {
                _glossaryService.Remove(glossary);
                return 0;
            }
            catch (Exception) {
                // @todo Add more descriptive error message handling / logging
                return -1;
            }
        }
    }
}
