using System.ComponentModel.DataAnnotations;
namespace EarlsBurger.Data;

public class OrderItems
{
    [Required]
    public int OrderNo  { get; set; }
    [Required]
    public int StockID  { get; set; }
    [Required]
    public int Quantity  { get; set; }
}