using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EarlsBurger.Pages;

public class Checkout : PageModel
{
   
        public List<CartItem> CartItems { get; set; }
        public decimal Total { get; set; }

        public void OnGet()
        {
            CartItems = GetCartItems();
            Total = CartItems.Sum(item => item.Subtotal);
        }

        private List<CartItem> GetCartItems()
        {
            
            return new List<CartItem>
            {
                new CartItem { Name = "Burger", Quantity = 2, Price = 5.99m },
                new CartItem { Name = "Fries", Quantity = 1, Price = 2.99m }
            };
        }
    }

    public class CartItem
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Subtotal => Quantity * Price;
    }
