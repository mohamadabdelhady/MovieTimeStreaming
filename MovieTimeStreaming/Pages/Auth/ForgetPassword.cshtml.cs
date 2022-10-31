using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace MovieTimeStreaming.Pages.Auth
{
    public class ForgetPasswordModel : PageModel
    {
       private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgetPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("/Auth/ForgetPasswordConfirmation");
                }
                
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Auth/ResetPassword",
                    pageHandler: null,
                    values: new {code = code },
                    protocol: Request.Scheme);
                
                string message = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                var client = new SmtpClient("smtp.mailtrap.io",587)
                {
                    Credentials = new NetworkCredential("15f40ac89aab5a","a84122b19d4d79"),
                    EnableSsl = true
                };
                                     
                MailMessage mailMessage=new MailMessage("MovieTime@example.com", Input.Email,"Reset password",message);
                                     
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                    $"<div style=margin:10px;padding:10px'><img src='https://raw.githubusercontent.com/mohamadabdelhady/MovieTimeStreaming/main/MovieTimeStreaming/wwwroot/Asset/MovieTimeLogo.png'><br><p class='m-2 p-2'>{message}</p></div>",
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
                    
                }
                
                return RedirectToPage("/Auth/ForgetPasswordConfirmation");
            }

            return Page();
        }
    }
}
