using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Admin
{
    public class AdminPanelModel : PageModel
    {
        [Authorize(Roles = "SuperAdmin,Admin")]
        public void OnGet()
        {
        }
    }
}
