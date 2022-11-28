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
            var ids = _context.MediaBookmarks.Where(x => x.UserId == user.Result.Id).Select(x=>int.Parse(x.MediaId)).ToArray();
            BookmarkItems = _context.Media.Where(x => ids.Contains(x.ID)).ToList();
        }
    }
}
