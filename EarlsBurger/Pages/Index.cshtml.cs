using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EarlsBurger.Data;
using Microsoft.AspNetCore.Authorization;
using EarlsBurger.Models;
using Microsoft.EntityFrameworkCore;

namespace EarlsBurger.Pages
{
    [Authorize (Roles = "Admin, Member")] 
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, EarlsBurgerContext context)
        {
            _logger = logger;
            _context = context;
        }


        [BindProperty]
        public string Search { get; set; }

        public IActionResult OnPostSearch()
        {
            FoodItem = _context.FoodItems
                .FromSqlRaw("SELECT * FROM FoodItem WHERE Item_name LIKE '" + Search + "%'").ToList();
            return Page();
        }

        private readonly EarlsBurgerContext _context;

        // public void OnGet()
        // {
        //
        // }

       
        

        public IList<FoodItem> FoodItem { get; set; } = default!;

        public void OnGet()
        {
            FoodItem = _context.FoodItems.FromSqlRaw("Select * FROM FoodItem").ToList();
        }
    }
}
