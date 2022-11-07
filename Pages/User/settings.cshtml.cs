using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MovieTimeStreaming.Pages.User
{
    [Authorize]
    public class settingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _host;

        public string userName { get; set; }
        public string userEmail{ get; set; }
        public string userImage { get; set; }
        public IFormFile newImg { get; set; }
        
        public settingsModel(UserManager<ApplicationUser> userManager,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _host = env;
        }

        public async Task OnGet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);
            userName = currentUser.UserName;
            userEmail = currentUser.Email;
            userImage = currentUser.ProfileImage;
        }
        
        public async Task<RedirectToPageResult> OnPostAccount()
        {
            var newName = HttpContext.Request.Form["newName"];
            var newEmail= HttpContext.Request.Form["newEmail"];

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (newName != currentUser.UserName)
            {
                currentUser.UserName = newName;
                var resultForName = await _userManager.UpdateAsync(currentUser);
                Debug.WriteLine(resultForName);
            }

            if (newEmail != currentUser.Email)
            {
                currentUser.Email = newEmail;
                var resultForEmail = await _userManager.UpdateAsync(currentUser);
                Debug.WriteLine(resultForEmail);
            }

            if (newImg != null)
            {
                if (newImg.Length > 0 && newImg.Length!=2097152)
                {
                    string ImageName= Guid.NewGuid().ToString() + Path.GetExtension(newImg.FileName);

                    await using var stream = new FileStream(Path.Combine(_host.WebRootPath, "Asset/UserProfiles", ImageName), FileMode.Create);
                    await newImg.CopyToAsync(stream);
                    currentUser.ProfileImage = "../Asset/UserProfiles/"+ImageName;
                    var resultForImg = await _userManager.UpdateAsync(currentUser);
                    Debug.WriteLine(resultForImg);
                }
            }
            return RedirectToPage("settings");
        }
    }
}
