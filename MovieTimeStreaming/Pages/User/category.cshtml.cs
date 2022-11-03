using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MovieTimeStreaming.Pages.User
{
    public class categoryModel : PageModel
    {
        public string selectedCategory { get; set; }
        public void OnGet(string category)
        {
            selectedCategory = category is not ("Movies" or "Tv Shows" or "Documentaries" or "Anime") ? "Movies" : category;
        }
    }
}
