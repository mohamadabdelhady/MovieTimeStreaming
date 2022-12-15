using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AddMediaModel : PageModel
    {
        
        private readonly ApplicationDbContext _context;
        
        public AddMediaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            
        }
    }
}
