using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Auth
{
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogOutModel> _logger;

        public LogOutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogOutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnPost()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return Redirect("/");
            // if (returnUrl != null)
            // {
            //     return LocalRedirect(returnUrl);
            // }
            // else
            // {
            //     // This needs to be a redirect so that the browser performs a new
            //     // request and the identity for the user gets updated.
            //     return RedirectToPage();
            // }
        }
    }
}
