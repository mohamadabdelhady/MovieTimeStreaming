using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Auth
{
    public class LogInModel : PageModel
    {
         private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogInModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public LogInModel(SignInManager<ApplicationUser> signInManager, ILogger<LogInModel> logger,UserManager<ApplicationUser>userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
        public string ReturnUrl { get; set; }
        
        [TempData]
        public string ErrorMessage { get; set; }
        
        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            // ReSharper disable once UseConfigureAwaitFalse
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var user = await _userManager.FindByEmailAsync(Input.Email);
                var result = await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }
            string allErrors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
            Debug.WriteLine(allErrors);
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
