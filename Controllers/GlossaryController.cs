using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace glossary.Controllers
{
    [Route("api/[controller]")]
    public class GlossaryController : Controller
    {
        /// <summary>
        /// Gets a list of all the terms stored in the Persistent Data Storage and returns them in alphabetical order
        /// </summary>
        /// <returns>
        /// A list of terms in alphabetical order
        /// </returns>
        [HttpGet("[action]")]
        public IEnumerable<String> GetTerms()
        {
            //@todo   
            return null;
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
            //@todo
            return "";
        }

        /// <summary>
        /// Updates the definition for a specific term
        /// </summary>
        /// <param name="term">
        /// The term that is being updated
        /// </param>
        /// <returns>
        /// A return code, 0 being success, and -1 being an error has occured
        /// </returns>
        [HttpPost("[action]")]
        public int UpdateDefinition(string term)
        {
            //@todo
            return -1;
        }        

        /// <summary>
        /// A Glossary Model representing an entity / entry, an associated term and definition
        /// </summary>
        public class Glossary
        {
            public string term { get; set; } //< The term, a single word or short phrase
            public string definition { get; set; } //< A paragraph of text that defines the term
        }
    }
}
