using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTimeStreaming.Models;
using MovieTimeStreaming.Data;

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
        public async Task<IQueryable> GetAllReviews(string id,[FromQuery(Name = "page")] int page)
        {
            var PageNum = page;
            var user = _userManager.GetUserAsync(User);


            var results = _context.Reviews.Include(x=>x.User)
                .Select(x => new
                {
                    x.User.UserName, x.CreatedAt, x.User.ProfileImage, x.UserReview, x.MediaId,
                    User_Id = x.UserId, x.UserRating
                })
                .Where(x => x.MediaId == id && x.User_Id != user.Result.Id)
                .Skip((PageNum - 1) * 10);
             return results;
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
        [Route("delete")]
        [HttpDelete]
        public void DeleteUserReview(string id)
        {
            var user = _userManager.GetUserAsync(User);
            var review=_context.Reviews.First(x => x.UserId == user.Result.Id&&x.MediaId==id);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}
