using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class ViewMediaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        [FromQuery(Name = "id")]
        public int MediaId { get; set; }

        public string MediaTitle { get; set; }
        public string MediaGenre { get; set; }
        public string MediaType { get; set; }
        public string MediaAbout { get; set; }
        public string MediaImg { get; set; }
        public float rating { get; set; }

        public ViewMediaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            var MediaItem=_context.Media.Find(MediaId);
            MediaTitle = MediaItem.Title;
            MediaGenre = MediaItem.Genre;
            MediaAbout = MediaItem.about;
            MediaImg = MediaItem.mediaImg;
            rating = MediaItem.Rating;
        }
    }
}
