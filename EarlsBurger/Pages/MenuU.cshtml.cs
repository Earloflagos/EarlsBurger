using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EarlsBurger.Data;
using EarlsBurger.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EarlsBurger.Pages.MenuU
{
    public class MenuU : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly EarlsBurgerContext _db;

        public MenuU(EarlsBurgerContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // public IndexModel(EarlsBurger.Data.EarlsBurgerContext context)
        // {
        //     _db = db;
        // }

        public IList<FoodItem> FoodItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FoodItem = await _db.FoodItems.ToListAsync();
        }

        public async Task<IActionResult> OnPostBuyAsync(int itemID)
        {
            var user = await _userManager.GetUserAsync(User);
            EarlsBurgerContext.CheckoutCustomer customer = await _db
                .CheckoutCustomers 
                .FindAsync(user.Email);

            var item = _db.BasketItems
                .FromSqlRaw("Select * from BasketItems where StockID = {0}" +
                            " AND BasketID = {1}", itemID, customer.BasketID)
                .ToList()
                .FirstOrDefault();

            if (item == null)
            {
                EarlsBurgerContext.BasketItem newItem = new EarlsBurgerContext.BasketItem()
                {
                    BasketID = customer.BasketID,
                    StockID = itemID,
                    Quantity = 1
                };
                _db.BasketItems.Add(newItem);
                await _db.SaveChangesAsync();
            }
            else
            {
                item.Quantity = item.Quantity + 1;
                _db.Attach(item).State = EntityState.Modified;
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException e)
                {
                    throw new Exception($"Basket not found!", e); 
                }
            }

            return RedirectToPage();
        }
    }
}
