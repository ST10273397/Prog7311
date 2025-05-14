using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Prog7311POE.Services;
using System.Security.Claims;
using Prog7311POE.Models;

namespace Prog7311POE.Pages
{
    public class HomeModel : PageModel
    {
        private readonly EmployeeService _employeeService;
        private readonly FarmerService _farmerService;
        private readonly ProductService _productService;

        public HomeModel(EmployeeService employeeService, FarmerService farmerService, ProductService productService)
        {
            _employeeService = employeeService;
            _farmerService = farmerService;
            _productService = productService;
        }

        public List<Farmer> Farmers { get; set; } = new();
        public List<Product> Products { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string? searchTerm, DateTime? startDate, DateTime? endDate)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            if (role == "Employee")
            {
                var showByFarmerName = false;
                var showByProduct = false;

                // First try to search farmers by name
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var matchedFarmers = await _farmerService.SearchByNameAsync(searchTerm);
                    Farmers = matchedFarmers.Where(f => f.EmployeeId == userId).ToList();

                    if (Farmers.Any())
                    {
                        // Load all products for these farmers
                        for (int i = 0; i < Farmers.Count; i++)
                        {
                            var fullFarmer = await _farmerService.GetByIdWithProductsAsync(Farmers[i].Id);
                            if (fullFarmer != null)
                                Farmers[i].Products = fullFarmer.Products;
                        }

                        showByFarmerName = true;
                    }
                }

                // If no farmers matched OR search is for products/dates, try product search
                if (!showByFarmerName)
                {
                    var employee = await _employeeService.GetByIdWithFarmersAndProductsAsync(userId);
                    if (employee != null)
                        Farmers = employee.Farmers;

                    var filteredFarmers = new List<Farmer>();

                    foreach (var farmer in Farmers)
                    {
                        var matchingProducts = farmer.Products
                            .Where(p =>
                                (string.IsNullOrWhiteSpace(searchTerm) ||
                                 p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                 p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) &&
                                (!startDate.HasValue || !endDate.HasValue ||
                                 (p.productionDate >= startDate && p.productionDate <= endDate)))
                            .ToList();

                        if (matchingProducts.Any())
                        {
                            farmer.Products = matchingProducts;
                            filteredFarmers.Add(farmer);
                        }
                    }

                    Farmers = filteredFarmers;
                }

            }


            else if (role == "Farmer")
            {
                var allProducts = await _productService.GetByFarmerIdAsync(userId);

                //Apply filtering if any
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    allProducts = allProducts.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                         p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (startDate.HasValue && endDate.HasValue)
                {
                    allProducts = allProducts.Where(p => p.productionDate >= startDate && p.productionDate <= endDate).ToList();
                }

                Products = allProducts;
            }

            return Page();
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Category { get; set; }

        [BindProperty]
        public DateTime ProductionDate { get; set; }

        public async Task<IActionResult> OnPostCreateProductAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var newProduct = new Product
            {
                Name = Name,
                Category = Category,
                productionDate = ProductionDate
            };

            if (role == "Farmer")
            {
                newProduct.FarmerId = userId;
            }

            await _productService.AddAsync(newProduct);
            return RedirectToPage(); // Refresh
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAddFarmerAsync()
        {
            var employeeId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var newFarmer = new Farmer
            {
                FullName = FullName,
                Email = Email,
                Password = Password,
                EmployeeId = employeeId
            };

            await _farmerService.AddAsync(newFarmer);
            return RedirectToPage();
        }


    }
}
