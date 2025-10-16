using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }
    }
}
