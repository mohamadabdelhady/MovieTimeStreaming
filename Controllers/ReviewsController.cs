using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Controllers
{
    [Produces("application/json")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Route("api/review/getAll")]
        [HttpGet]
        public async Task<List<Reviews>> GetAllReviews()
        {
            return _context.Reviews.OrderBy(x=>x.CreatedAt).Take(10).ToList();
        }
    }
}
