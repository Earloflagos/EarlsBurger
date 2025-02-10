using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace EarlsBurger.Pages
{
    public class ContactModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
            SmtpClient smtpClient = new SmtpClient("your_smtp_server")
            {
                Port = 587, // or your port
                Credentials = new NetworkCredential("your_email@example.com", "your_password"),
                EnableSsl = true,
            };
            
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("your_email@example.com"),
                Subject = "New Contact Form Submission",
                Body = $"Name: {Name}\nEmail: {Email}\nMessage: {Message}",
                IsBodyHtml = false,
            };
            mailMessage.To.Add("your_email@example.com");
            
            try
            {
                await smtpClient.SendMailAsync(mailMessage);
                TempData["Success"] = "Message sent successfully!";
                return RedirectToPage("./Contact");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                TempData["Error"] = "Failed to send message. Please try again.";
                return Page();
            }
        }
    }
}
