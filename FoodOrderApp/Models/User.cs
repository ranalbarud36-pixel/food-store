using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } // מזהה ייחודי למשתמש

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // בפרויקט אמיתי מצפינים את זה, כרגע נשמור רגיל

        public bool IsAdmin { get; set; } = false; // האם הוא מנהל? ברירת מחדל: לא
    }
}