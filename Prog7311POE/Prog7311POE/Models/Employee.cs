using System.Net.Http.Headers;

namespace Prog7311POE.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Farmer> Farmers { get; set; } = new();

        public Employee()
        { }

        public Employee(int id, string fullName, string email, string password, List<Farmer> farmers = null)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Password = password;
            Farmers = farmers ?? new List<Farmer>();
        }
    }

}
