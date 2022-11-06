using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class settingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        
        public string newName { get; set; }
        public string newEmail { get; set; }
        public IFormFile newImg { get; set; }
        
        public settingsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task OnPostAccount()
        {
            var user = _userManager.GetUserAsync(User);
            var currentUser = await _userManager.FindByIdAsync(user.Result.Id);

            if (newName != user.Result.UserName)
            {
                currentUser.UserName = newName;
                var resultForName = await _userManager.UpdateAsync(currentUser);
            }

            if (newEmail != user.Result.Email)
            {
                currentUser.Email = newEmail;
                var resultForEmail = await _userManager.UpdateAsync(currentUser);
            }

            if (newImg != null)
            {
                if (newImg.Length > 0 && newImg.Length!=2097152)
                {
                    var filePath = Path.Combine("../Asset/UserProfiles/", 
                        Path.GetRandomFileName());
                    await using var stream = System.IO.File.Create(filePath);
                    await newImg.CopyToAsync(stream);
                }
            }
        }
    }
}
