using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.User
{
    public class searchResultsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        

        public List<Media> results { get; set; }
        [FromQuery(Name = "key")]
        public string key { get; set; }
        public searchResultsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var keyNormalize = (key.ToString()).ToUpper();
            results = _context.Media.Where(x => x.Title.ToUpper().Contains(keyNormalize)).ToList();
        }

        
    }
}
