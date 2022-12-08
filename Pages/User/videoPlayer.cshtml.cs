using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.User
{
    public class videoPlayerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        [FromQuery(Name = "id")]
        public string MediaId { get; set; }
        [FromQuery(Name = "series")]
        public string IsSeries { get; set; }

        public string MediaTitle { get; set; }
        public string MediaGenre { get; set; }
        public string MediaType { get; set; }
        public string MediaAbout { get; set; }
        public string MediaImg { get; set; }
        public float rating { get; set; }
        public string MediaSrc { get; set; }
        public int MediaWatchCount { get; set; }
        
        public DateOnly MediaReleaseDate { get; set; }
        public double MediaResumeTime { get; set; }
       
        public videoPlayerModel(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public void OnGet()
        {
            var id = int.Parse(MediaId);
            if (IsSeries=="1")
            {
                var MediaItem=_context.MediaEpisodes.Find(id);
                var series = _context.Media.FirstOrDefault(x => x.ID == int.Parse(MediaItem.SeriesMediaId));
                MediaTitle = series.Title;
                MediaGenre = series.Genre;
                MediaAbout = series.about;
                MediaType = series.mediaType;
                MediaImg = series.mediaImg;
                rating = series.Rating;
                MediaSrc = MediaItem.MediaSrc;
                MediaWatchCount = MediaItem.WatchCount;
            
                MediaReleaseDate = series.ReleaseDate;
            }
            else
            {

                var MediaItem = _context.Media.Find(id);
                MediaTitle = MediaItem.Title;
                MediaGenre = MediaItem.Genre;
                MediaAbout = MediaItem.about;
                MediaType = MediaItem.mediaType;
                MediaImg = MediaItem.mediaImg;
                rating = MediaItem.Rating;
                MediaSrc = MediaItem.MediaSrc;
                MediaWatchCount = MediaItem.WatchCount;

                MediaReleaseDate = MediaItem.ReleaseDate;
            }

            var History = _context.WatchHistory.FirstOrDefault(x => x.MediaId == MediaId);
            if (History!=null)
                MediaResumeTime = History.StopTime;
             StartedWatching();
            
        }

        public void StartedWatching()
        {
            var user = _userManager.GetUserAsync(User);
            var userId = user.Result.Id.ToString();
            var WatchHistory = _context.WatchHistory.FirstOrDefault(x => x.MediaId == MediaId && x.UserId == userId);
            if (WatchHistory == null)
            {

                var watch = new WatchHistory()
                {
                    UserId = user.Result.Id,
                    MediaId = MediaId,
                    LastWatchDate = DateTime.Now
                };
                _context.Entry(watch).State = EntityState.Added;
                _context.SaveChanges();
            }
        }
    }
}
