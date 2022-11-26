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
    [Route("api/RateSys/{id}")]
    [ApiController]
    public class RatingSystemController : ControllerBase
    {
        
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RatingSystemController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("like")]
        [HttpGet]
        public void LikeMedia(string id)
        {
            var MediaId = int.Parse(id);
            var user = _userManager.GetUserAsync(User);
            var RateAction = _context.RateActions.FirstOrDefault(x => x.UserId == user.Result.Id);
            var media = _context.Media.FirstOrDefault(x => x.ID == MediaId);
            if (RateAction!=null)
            {
                RateAction.RateType = true;
                media.DisLikes -= 1;
                media.Likes += 1;
                _context.SaveChanges();
            }
            else
            {
                var action = new RateActions()
                {
                    UserId = user.Result.Id,
                    MediaId = id,
                    RateType = true,
                    CreatedAt = DateTime.Now,
                };
                _context.Entry(action).State = EntityState.Added;
                _context.SaveChanges();
            }
        }
        [Route("disLike")]
        [HttpGet]
        public void DisLikeMedia(string id)
        {
            var MediaId = int.Parse(id);
            var user = _userManager.GetUserAsync(User);
            var RateAction = _context.RateActions.FirstOrDefault(x => x.UserId == user.Result.Id);
            var media = _context.Media.FirstOrDefault(x => x.ID == MediaId);
            if (RateAction!=null)
            {
                
                RateAction.RateType = false;
                media.Likes -= 1;
                media.DisLikes += 1;
                _context.SaveChanges();
            }
            else
            {
                var action = new RateActions()
                {
                    UserId = user.Result.Id,
                    MediaId = id,
                    RateType = false,
                    CreatedAt = DateTime.Now,
                };
                _context.Entry(action).State = EntityState.Added;
                _context.SaveChanges();
            }
        }

        [Route("update")]
        [HttpGet]
        public IQueryable update(string id)
        {
            var MediaId = int.Parse(id);
            var RateData = _context.Media.Select(x => new
            {
                x.Likes,
                x.DisLikes,
                x.ID
            }).Where(x=>x.ID==MediaId);
            return RateData;
        }
    }
}
