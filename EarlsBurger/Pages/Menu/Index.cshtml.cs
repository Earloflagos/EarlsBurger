using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EarlsBurger.Data;
using EarlsBurger.Models;

namespace EarlsBurger.Pages.Menu
{
    public class IndexModel : PageModel
    {
        private readonly EarlsBurger.Data.EarlsBurgerContext _context;

        public IndexModel(EarlsBurger.Data.EarlsBurgerContext context)
        {
            _context = context;
        }

        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FoodItem = await _context.FoodItems.ToListAsync();
        }
    }
}
