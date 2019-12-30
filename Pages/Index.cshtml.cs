using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using glossary.Models;
using glossary.Services;

namespace glossary.Pages
{
    public class IndexModel : PageModel
    {
        private readonly GlossaryService _glossaryService; //< Instance of GlossaryService to performs CRUD operations
        public List<Glossary> glossaryList;
        public IndexModel(GlossaryService _glossaryService) {
            this._glossaryService = _glossaryService;
        }
        public void OnGet()
        {
            glossaryList = _glossaryService.Get();
        }
    }
}