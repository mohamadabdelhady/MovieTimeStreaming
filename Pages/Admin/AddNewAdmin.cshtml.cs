using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class AddNewAdminModel : PageModel
    
    {
        public void OnGet()
        {
        }
    }
}
