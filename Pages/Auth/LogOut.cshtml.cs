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
        }
    }
}
