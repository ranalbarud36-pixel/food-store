using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodOrderApp.Data; // החיבור לדאטה-בייס
using FoodOrderApp.Models;
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context; // משתנה לחיבור למסד הנתונים

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "נא להזין אימייל")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "נא להזין סיסמה")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // --- הלוגיקה החדשה: בדיקה מול הדאטה-בייס ---

            // מחפשים משתמש שיש לו גם את האימייל הזה וגם את הסיסמה הזאת
            var user = _context.Users.FirstOrDefault(u => u.Email == Input.Email && u.Password == Input.Password);

            if (user == null)
            {
                // אם לא מצאנו משתמש כזה - נציג שגיאה
                // שינינו את string.Empty ל-"Input.Email" כדי שהשגיאה תופיע מתחת לשדה האימייל
                ModelState.AddModelError("Input.Email", "האימייל או הסיסמה שגויים");
                return Page();
            }

            // אם הגענו לפה - המשתמש נמצא והסיסמה נכונה!
            // כאן בעתיד נוסיף את "שמירת החיבור" (Session/Cookies)

            // מעבירים את המשתמש לדף הבית
            return RedirectToPage("/Index");
        }
    }
}