using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using MovieTimeStreaming.Services;

namespace MovieTimeStreaming.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<SignUpModel> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        // private MyEmailSender _MyEmailSender;

        public SignUpModel(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,ILogger<SignUpModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
            // _MyEmailSender = MyEmailSender;
        }
        [BindProperty]
        public InputModel Input { get; set; }
         // public string ReturnUrl { get; set; }
        // public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
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

        
        // public async Task OnGetAsync(string returnUrl = null)
        // {
        //     ReturnUrl = returnUrl;
        // }
        public async Task<IActionResult> OnPostAsync()
        {
             // returnUrl ??= Url.Content("~/Auth/SignUp");
            if (ModelState.IsValid)
            {
                Debug.WriteLine("two");
                var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    // var callbackUrl = Url.Page(
                    //     "/Auth/SignUpConfirmation",
                    //     pageHandler: null,
                    //     values: new { userId = user.Id, code = code },
                    //     protocol: Request.Scheme);
                    string message = "Hello!</p>Please click the button below to verify your email address";
                                     // $"<a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>";
                                     var client = new SmtpClient("smtp.mailtrap.io",587)
                                     {
                                         Credentials = new NetworkCredential("15f40ac89aab5a","a84122b19d4d79"),
                                         EnableSsl = true
                                     };
                                     
                                     MailMessage mailMessage=new MailMessage("MovieTime@example.com", Input.Email,"Email Verification",message);
                                     
                                     AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                                         $"<div style=margin:10px;padding:10px'><img src='https://raw.githubusercontent.com/mohamadabdelhady/MovieTimeStreaming/main/MovieTimeStreaming/wwwroot/Asset/MovieTimeLogo.png'><br><p class='m-2 p-2'>{message}</p></div><div class='m-2 p-2'><button class='btn' style='background-color: #2A6274; color: white;' onclick='window.location.href='''>Verify email</button><div>",
                                         null,
                                         "text/html"
                                     );
                                    
                                      // LinkedResource Link = new LinkedResource("./wwwroot/css/site.css");
                                      // Link.ContentId = "Wedding";
                                      // htmlView.LinkedResources.Add(Link);
                                     mailMessage.AlternateViews.Add(htmlView);
                                     
                                     try
                                     {
                                         client.Send(mailMessage);
                                     }
                                     catch (SmtpException ex)
                                     {
                                         
                                     }
                                     
                                     // if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    // {
                    //     return RedirectToPage("SignUpConfirmation", 
                    //         new { email = Input.Email });
                    // }
                    // else
                    // {
                    //     await _signInManager.SignInAsync(user, isPersistent: false);
                    //     return LocalRedirect(returnUrl);
                    // }
                }
                
                //return error if found
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
