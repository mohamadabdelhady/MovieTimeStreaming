using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class settingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public string userImg { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public settingsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            userImg = user.ProfileImage;
            userName = user.UserName;
            userEmail = user.Email;
        }
    }
}
