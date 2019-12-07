using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using glossary.Services;
using System.Linq;
using glossary.Models;

namespace glossary.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetTerms()
        {
            List<string> terms = _glossaryService.Get() //< Gets all the glosarries
                                    .Select(_glossary => _glossary.Term) //< Selects only the term
                                    .ToList(); //< Converts to a list
            return Ok(terms);
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
        public IActionResult GetDefinition(string term)
        {
            Glossary glossary = _glossaryService.Find(term);
            string result = String.Empty;
            if (glossary != null)
                result = glossary.Definition;
            return Ok( result );
        }

        /// <summary>
        /// Updates the definition for a specific term, or deletes the term if it's empty
        /// </summary>
        /// <param name="term">
        /// The term that is being updated
        /// </param>
        /// <param name="definition">
        /// The definition of the term
        /// </param>
        /// <returns>
        /// A return code, 0 being success, and -1 being an error has occured
        /// </returns>
        [HttpPut("[action]")]
        public int UpdateTerm(string term, string definition)
        {
            if (String.IsNullOrEmpty(term))
                return -1;
            try {
                Glossary glossary = _glossaryService.Find(term);
                //Glossary does not exist, so create it
                if (glossary == null) {
                    glossary = new Glossary();
                    glossary.Term = term;
                    glossary.Definition = definition;
                    _glossaryService.Create(glossary);
                }
                else {
                    // Glossary exists, but empty definition, so delete
                    if (String.IsNullOrEmpty(definition)) {
                        return DeleteTerm(glossary);
                    }
                    // Gossary exists, and valid definition, so update
                    else {
                        glossary.Definition = definition;
                        _glossaryService.Update(glossary.Id, glossary);
                    }
                }
                return 0;
            }
            catch (Exception) {
                // @todo Add more descriptive error message handling / logging
                return -1;
            }
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
        [HttpDelete("{id:length(24)}")]
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
