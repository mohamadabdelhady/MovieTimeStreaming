using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.Admin
{
    public class DeleteMediaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Media> MediaList { get; set; }

        public DeleteMediaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            MediaList = _context.Media.ToList();
        }
    }
}
