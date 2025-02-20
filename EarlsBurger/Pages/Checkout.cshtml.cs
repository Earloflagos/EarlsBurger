using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EarlsBurger.Data;
using EarlsBurger.Models;

namespace EarlsBurger.Pages;

public class Checkout : PageModel
{
   private readonly EarlsBurgerContext _db;
   private readonly UserManager<IdentityUser> _UserManager;
   public IList<CheckoutItem> Items { get; private set; }

   public decimal Total;
   public long AmountPayable;

   public Checkout(EarlsBurgerContext db, UserManager<IdentityUser> UserManager)
   {
      _db = db;
      _UserManager = UserManager;
   }

   public async Task OnGetAsync()
   {
      var user = await _UserManager.GetUserAsync(User);
      EarlsBurgerContext.CheckoutCustomer customer = await _db.CheckoutCustomers.FindAsync(user.Email);
      Items = _db.CheckoutItems.FromSqlRaw(
         "SELECT FoodItem.ID, FoodItem.Price, " +
         "FoodItem.Item_name, " +
         "BasketItems.BasketID, BasketItems.Quantity " +
         "FROM FoodItem INNER JOIN BasketItems " +
         "ON FoodItem.ID = BasketItems.StockID " + 
         "WHERE BasketID {0}", customer.BasketID
         ).ToList();
      Total = 0;
      foreach (var item in Items)
      {
         Total += (item.Quantity * item.Price);
      }
      AmountPayable = (long)Total;
   }
}
