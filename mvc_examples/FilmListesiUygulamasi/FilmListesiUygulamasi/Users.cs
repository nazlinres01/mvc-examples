using System.ComponentModel.DataAnnotations;

namespace FilmListesiUygulamasi.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
