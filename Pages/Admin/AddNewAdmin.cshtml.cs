using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class AddNewAdminModel : PageModel
    
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public IQueryable AdminList { get; set; }
        public IQueryable usersList { get; set; }

        public AddNewAdminModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        

        public void OnGet()
        {
           
            var query3 = from user in _context.Users
                join role in _context.UserRoles on user.Id equals role.UserId
                select new { user.Id, user.UserName };
            var e = query3.Select(x => x.Id).ToArray();
            var q = _context.Users
                .Where(x => !e.Contains(x.Id));
            usersList = q;

        }
        public async Task<RedirectToPageResult> OnPostAdd()
        {
            var id = HttpContext.Request.Form["id"];
            var CurrentUser = _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(await CurrentUser.ConfigureAwait(false), "Admin");
            return RedirectToPage("AddNewAdmin");
        }
    }
}
