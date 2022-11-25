using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        

        public MediaController(ApplicationDbContext context)
        {
            _context = context;
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
            var count = _context.Media.Count();
            bool IsLastPage = count - (10 * PageNum) < 0;
            return _context.Media.Where(x=>x.mediaType==genre).OrderBy(x=>x.CreatedAt).Skip((PageNum-1)*10).Take(10).ToList();
        }
    }
}
