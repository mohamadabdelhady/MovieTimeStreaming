using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.User
{
    public class UserBookmarksModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public List<Media> BookmarkItems { get; set; }
        
        public UserBookmarksModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void OnGet()
        {
            var user = _userManager.GetUserAsync(User);
            BookmarkItems =  _context.Media
                .Where(x =>
                    _context.MediaBookmarks.Any(b => b.UserId == user.Result.Id)
                )
                .ToList();
        }
    }
}
