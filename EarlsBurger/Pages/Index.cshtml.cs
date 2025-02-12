using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EarlsBurger.Data;
using Microsoft.AspNetCore.Authorization;

namespace EarlsBurger.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private readonly EarlsBurgerContext _db;

        public void OnGet()
        {

        }
    }
}
