using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MovieTimeStreaming.Pages.User
{
    [Authorize]
    [BindProperties]
    public class settingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _host;

        [Required]                                
        [Display(Name = "User name")]   
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail{ get; set; }
        public string UserImage { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
        public IFormFile NewImg { get; set; }
        
        public settingsModel(UserManager<ApplicationUser> userManager,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _host = env;
        }

        public async Task<RedirectToPageResult> OnPostAccount()
        {
            var user = _userManager.GetUserAsync(User);
            var currentUser = await _userManager.FindByIdAsync(user.Result.Id);
            if (UserName != currentUser.UserName)
            {
                currentUser.UserName = UserName;
                var resultForName = await _userManager.UpdateAsync(currentUser);
                Debug.WriteLine(resultForName);
            }

            if (UserEmail != currentUser.Email)
            {
                currentUser.Email = UserEmail;
                var resultForEmail = await _userManager.UpdateAsync(currentUser);
                Debug.WriteLine(resultForEmail);
            }

            if (NewImg != null)
            {
                if (NewImg.Length > 0 && NewImg.Length!=2097152)
                {
                    string ImageName= Guid.NewGuid().ToString() + Path.GetExtension(NewImg.FileName);

                    await using var stream = new FileStream(Path.Combine(_host.WebRootPath, "Asset/UserProfiles", ImageName), FileMode.Create);
                    await NewImg.CopyToAsync(stream);
                    currentUser.ProfileImage = "../Asset/UserProfiles/"+ImageName;
                    var resultForImg = await _userManager.UpdateAsync(currentUser);
                    Debug.WriteLine(resultForImg);
                }
            }
            return RedirectToPage("settings");
        }

        public async Task<RedirectToPageResult> OnPostPassword()
        {
            var user = _userManager.GetUserAsync(User);
            var currentUser = await _userManager.FindByIdAsync(user.Result.Id);
            var result= await _userManager.ChangePasswordAsync(currentUser,OldPassword, NewPassword);
            
            return RedirectToPage("settings");
        }
    }
}
