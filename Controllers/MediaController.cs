using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/Media")]
    public class MediaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        

        public MediaController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Media>> GetAllMedia([FromQuery(Name = "page")] int page)
        {
            var PageNum = page;
            var count = _context.Media.Count();
            bool IsLastPage = count - (10 * PageNum) < 0;
            return _context.Media.OrderBy(x=>x.CreatedAt).Skip((PageNum-1)*10).Take(10).ToList();
        }
        [Route("{genre}/getAll")]
        [HttpGet]
        public async Task<List<Media>> GetAllMedia([FromQuery(Name = "page")] int page, string genre)
        {
            var PageNum = page;
            
            return _context.Media.Where(x=>x.mediaType==genre).OrderBy(x=>x.CreatedAt).Skip((PageNum-1)*10).Take(10).ToList();
        }

        [Route("{id}/watchedTime")]
        [HttpPost]
        public void SetWatchedTime(string id)
        {
            var user = _userManager.GetUserAsync(User);
            var userId = user.Result.Id;
            var WatchHistory = _context.WatchHistory.FirstOrDefault(x => x.MediaId == id&&x.UserId==userId);
            var MediaId = int.Parse(id);
            var media = _context.Media.FirstOrDefault(x => x.ID == MediaId);
            if(WatchHistory!=null)
            {
                var watchedTime=double.Parse(HttpContext.Request.Form["watchedTime"]);
                WatchHistory.StopTime = watchedTime;
                WatchHistory.LastWatchDate = DateTime.Now;
                if (!WatchHistory.Watched)
                {
                    var SevntyPercentOfVideo = (media.MediaDuration * 70) / 100;
                    if (watchedTime >= SevntyPercentOfVideo)
                    {
                        WatchHistory.Watched = true;
                        media.WatchCount += 1;
                    }
                }
            }
            _context.SaveChanges();
        }

        [Route("search")]
        [HttpPost]
        public RedirectToPageResult search()
        {
            var key = HttpContext.Request.Form["key"];
            var keyNormalize = (key.ToString()).ToUpper();
            var results = _context.Media.Where(x => x.Title.ToUpper().Contains(keyNormalize)).ToList();
            return RedirectToPage("searchResults",new {results});
        }
        
    }
}
