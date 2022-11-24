using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class videoPlayerModel : PageModel
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
        public string MediaSrc { get; set; }
        public int MediaWatchCount { get; set; }
        public int MediaLikes { get; set; }
        public int MediaDisLikes { get; set; }
        public videoPlayerModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            var MediaItem=_context.Media.Find(MediaId);
            MediaTitle = MediaItem.Title;
            MediaGenre = MediaItem.Genre;
            MediaAbout = MediaItem.about;
            MediaType = MediaItem.mediaType;
            MediaImg = MediaItem.mediaImg;
            rating = MediaItem.Rating;
            MediaSrc = MediaItem.MediaSrc;
            MediaWatchCount = MediaItem.WatchCount;
            MediaLikes = MediaItem.Likes;
            MediaLikes = MediaItem.DisLikes;
        }
    }
}
