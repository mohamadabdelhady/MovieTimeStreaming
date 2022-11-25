using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class MainModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public int MediaLastPage { get; set; }
        public MainModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            var MediaCount=_context.Media.Count();
            var temp = (float)MediaCount / 10;
            MediaLastPage = (int)Math.Ceiling(temp);
        }
    }
}
