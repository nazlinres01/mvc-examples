namespace Basics.Models
{
    public class Employee
    {
        public int Id { get; set; }  //prop kısayol
        public String FirstName { get; set; } = String.Empty;
        public String LastName { get; set; } = String.Empty;

        public String FullName => $"{FirstName} {LastName.ToUpper()}";

        public int Age { get; set; }
    }
}