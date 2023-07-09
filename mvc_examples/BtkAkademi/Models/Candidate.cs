using System.ComponentModel.DataAnnotations;

namespace BtkAkademi.Models  // path
{
    public class Candidate
    {
        [Required(ErrorMessage = "E-mail is required.")]
        public String? Email { get; set; } = String.Empty; // ? Null bir değer olabilir.  

        [Required(ErrorMessage = "FirstName is required.")]
        public String? FirstName { get; set; } = String.Empty;

        [Required(ErrorMessage = "LastName is required.")]
        public String? LastName { get; set; } = String.Empty;
        public String? FullName => $"{FirstName} {LastName?.ToUpper()}"; // Soyisim değeri var ise büyüt yok ise bir şey yapma
        public int? Age { get; set; }
        public String? SelectedCourse { get; set; } = String.Empty;
        public DateTime ApplyAt { get; set; }

        public Candidate()
        {
            ApplyAt = DateTime.Now;
        }


    }
}