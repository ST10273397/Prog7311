using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Prog7311POE.Services;
using System.Security.Claims;

namespace Prog7311POE.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly FarmerService _farmerService;

        public IndexModel(EmployeeService employeeService, FarmerService farmerService)
        {
            _employeeService = employeeService;
            _farmerService = farmerService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //Check for Employee first
            var employee = (await _employeeService.GetAllAsync()).FirstOrDefault(e => e.Email == Email && e.Password == Password);

            if (employee != null)
            {
                await SignInUser(employee.Id.ToString(), "Employee");
                return RedirectToPage("/Home");
            }

            //Then check for Farmer
            var farmer = (await _farmerService.GetAllAsync()).FirstOrDefault(f => f.Email == Email && f.Password == Password);

            if (farmer != null)
            {
                await SignInUser(farmer.Id.ToString(), "Farmer");
                return RedirectToPage("/Home");
            }

            ErrorMessage = "Invalid email or password";
            return Page();
        }

        private async Task SignInUser(string userId, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, role)
            };

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("CookieAuth", principal);
        }

    }
}
