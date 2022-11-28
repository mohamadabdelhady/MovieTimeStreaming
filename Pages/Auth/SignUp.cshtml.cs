using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace MovieTimeStreaming.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<SignUpModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IConfiguration _configuration;

        public SignUpModel(IUserStore<ApplicationUser> userStore,SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager,ILogger<SignUpModel> logger,IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _configuration = configuration;
        }
        [BindProperty]
        public InputModel Input { get; set; }
         public string ReturnUrl { get; set; }
         public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
        public class InputModel
        {
            
            [Required]                                
            [Display(Name = "User name")]             
            public string UserName { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        
        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl;
             ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            Debug.WriteLine("one");
             returnUrl ??= Url.Content("~/");
             ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                Debug.WriteLine("two");
                // var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email };
                var user = CreateUser();
                await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Auth/SignUpConfirmation",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);
                    string message = "Hello!</p>Please click the button below to verify your email address"+
                                     $" <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>";
                                     var client = new SmtpClient("smtp.mailtrap.io",587)
                                     {
                                         Credentials = new NetworkCredential(_configuration["smtp:userName"],_configuration["smtp:password"]),
                                         EnableSsl = true
                                     };
                                     
                                     MailMessage mailMessage=new MailMessage("MovieTime@example.com", Input.Email,"Email Verification",message);
                                     
                                     AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                                         $"<div style=margin:10px;padding:10px'><img src='https://i.ibb.co/Y3r5qD4/Movie-Time-Logo.png'><br><p class='m-2 p-2'>{message}</p></div>",
                                         null,
                                         "text/html"
                                     );
                                     mailMessage.AlternateViews.Add(htmlView);
                                     try
                                     {
                                         client.Send(mailMessage);
                                     }
                                     catch (SmtpException ex)
                                     {
                                         throw new InvalidOperationException(
                                             "Something went wrong and we couldn't send your email confirmation.");
                                     }
                                     
                                     if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("SignUpConfirmation", 
                            new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                
                //return error if found
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            string allErrors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
             Debug.WriteLine(allErrors);
            return Page();
        }
        private ApplicationUser CreateUser()
        {
            try
            {
                Debug.WriteLine("three");
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                                                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
