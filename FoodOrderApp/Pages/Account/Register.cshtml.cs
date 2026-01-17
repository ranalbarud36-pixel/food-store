using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodOrderApp.Data;   // הייבוא של ה-Context
using FoodOrderApp.Models; // הייבוא של ה-User
using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context; // המשתנה שמחזיק את החיבור לדאטה-בייס

        // בנאי (Constructor) שמקבל את החיבור
        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "חובה להזין שם מלא")]
            [Display(Name = "שם מלא")]
            public string FullName { get; set; }

            [Required(ErrorMessage = "חובה להזין אימייל")]
            [EmailAddress(ErrorMessage = "כתובת אימייל לא תקינה")]
            public string Email { get; set; }

            [Required(ErrorMessage = "חובה לבחור סיסמה")]
            [DataType(DataType.Password)]
            [MinLength(6, ErrorMessage = "הסיסמה חייבת להכיל לפחות 6 תווים")]
            public string Password { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. בדיקה אם האימייל כבר קיים במערכת
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == Input.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Input.Email", "כתובת האימייל הזו כבר רשומה במערכת");
                return Page();
            }

            // 2. יצירת משתמש חדש
            var user = new User
            {
                FullName = Input.FullName,
                Email = Input.Email,
                Password = Input.Password // הערה: בפרויקט אמיתי מצפינים סיסמה, כרגע זה לימודי
            };

            // 3. שמירה לדאטה-בייס
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // הפקודה ששומרת בפועל!

            // 4. מעבר לדף הכניסה
            return RedirectToPage("/Account/Login");
        }
    }
}