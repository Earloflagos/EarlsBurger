using EarlsBurger.Data;
using EarlsBurger.Models;

namespace EarlsBurger.Data
{
    public class DbInitializer
    {
        public static void Initialize(EarlsBurgerContext context)
        {
            // Look for any food itmes.
            if (context.FoodItems.Any())
            {
                return;   // DB has been seeded
            }

            var fooditems = new FoodItem[]
            {
                new FoodItem{Item_name="Earls CheeseBurger",Item_desc="Our tasty shepherds pie packed full of lean minced lamb and an assortment of vegetables",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Earls ChickenBurger",Item_desc="Our tasty cottage pie packed full of lean minced beef and an assortment of vegetables",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Earls VeggieBurger",Item_desc="Scotland national Haggis dish. Sheep’s heart, liver, and lungs are minced, mixed with suet and oatmeal, then seasoned with onion, cayenne, and our secret spice. Served with boiled turnips and potatoes (‘neeps and tatties’)",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Earls Double",Item_desc="Succulent sausages nestled on a bed of buttery mashed potatoes and drenched in a rich onion gravy",Available=true,Vegetarian=false},
                new FoodItem{Item_name="Earls FishBurger",Item_desc="Ultimate toad-in-the-hole with caramelised onion gravy",Available=true,Vegetarian=false}
            };

            context.FoodItems.AddRange(fooditems);
            context.SaveChanges();

        }
    }
}