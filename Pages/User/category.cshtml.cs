using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class categoryModel : PageModel
    {
        public string selectedCategory { get; set; }
        private readonly ApplicationDbContext _context;
        public int MediaLastPage { get; set; }
        public categoryModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet(string category)
        {
            var MediaCount=_context.Media.Count();
            var temp = (float)MediaCount / 10;
            MediaLastPage = (int)Math.Ceiling(temp);
            selectedCategory = category is not ("Movies" or "Tv" or "Documentary" or "Anime") ? "Movie" : category;
        }
    }
}
