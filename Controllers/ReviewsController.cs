using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/review/{id}")]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       

        public ReviewsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<List<Reviews>> GetAllReviews(string id,[FromQuery(Name = "page")] int page)
        {
            var PageNum = page;
            var users = await _userManager.Users.ToListAsync().ConfigureAwait(false);
            return _context.Reviews.Where(x => x.MediaId == id).OrderBy(x => x.CreatedAt).Skip((PageNum - 1) * 10).Take(10).ToList();
        }

        [Route("get")]
        [HttpGet]
        public async Task<Reviews> GetUserReview(string id)
        {
            
            var user = _userManager.GetUserAsync(User);
            return _context.Reviews.FirstOrDefault(x => x.MediaId==id&&x.UserId==user.Result.Id);
        }

        [Route("add")]
        [HttpPost]
        public void AddUserReview(string id)
        {
            var user = _userManager.GetUserAsync(User);
            var review = new Reviews()
            {
                UserId = user.Result.Id,
                MediaId=id,
                UserReview=HttpContext.Request.Form["review"],
                UserRating=HttpContext.Request.Form["rating"],
            };
            _context.Entry(review).State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
