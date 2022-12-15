using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class DeleteAdminModel : PageModel
    
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IQueryable AdminList { get; set; }

        public DeleteAdminModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        

        public void OnGet()
        {
            var CurrentUser = _userManager.GetUserAsync(User);
            var query3 = from user in _context.Users
                join role in _context.UserRoles on user.Id equals role.UserId
                where user.Id!=CurrentUser.Result.Id.ToString()
                select new { user.Id, user.UserName };
            AdminList = query3;

        }
        public async Task<RedirectToPageResult> OnPostRemove()
        {
            var id = HttpContext.Request.Form["id"];
            var CurrentUser = _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(await CurrentUser.ConfigureAwait(false), "Admin");
            return RedirectToPage("AddNewAdmin");
        }
    }
}
