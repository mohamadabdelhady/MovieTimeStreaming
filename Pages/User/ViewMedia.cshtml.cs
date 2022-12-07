using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieTimeStreaming.Models;

namespace MovieTimeStreaming.Pages.User
{
    [BindProperties]
    public class ViewMediaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        [FromQuery(Name = "id")]
        public int MediaId { get; set; }

        public string MediaTitle { get; set; }
        public string MediaGenre { get; set; }
        public string MediaType { get; set; }
        public string MediaAbout { get; set; }
        public string MediaImg { get; set; }
        public float rating { get; set; }
        public int IsBookmarked { get; set; }
        public string SeasonNum { get; set; }
        public List<MediaEpisodes> EpisodesList { get; set; }
        public int ChosenSeason { get; set; }

        public ViewMediaModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var user = _userManager.GetUserAsync(User);
            var bookmark=_context.MediaBookmarks.FirstOrDefault(x => x.UserId == user.Result.Id&&x.MediaId==MediaId.ToString());
            IsBookmarked = bookmark!=null ? 1 : 0;
            SeasonNum = MediaItem.SeasonsCount;
            if (SeasonNum!=null)
            {
                EpisodesList = _context.MediaEpisodes.Where(x => x.SeriesMediaId == MediaId.ToString()).OrderBy(x=>x.EpisodeNum).ToList();
                
            }
        }

      
    }
}
