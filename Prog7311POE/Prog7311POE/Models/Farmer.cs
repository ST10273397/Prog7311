using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;

namespace Prog7311POE.Models
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public List<Product> Products { get; set; } = new();
        
        public int EmployeeId { get; set; }
        
        public Employee Employee { get; set; }
        
        public Farmer() { }

        public Farmer(int id, string fullName, string email, string password, int employeeId, List<Product> products = null)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Password = password;
            EmployeeId = employeeId;
            Products = products ?? new List<Product>();
            
        }
    }
}
