# 🌿 Agri-Energy Connect

A Razor Pages web application for managing farmers and their products — built with ASP.NET Core, Entity Framework Core, and SQLite.

Employees can manage their assigned farmers and view their products. Farmers can log in to manage their own products. The platform includes role-based login, product filtering, and clean Bootstrap-styled dashboards.

---

## 🚀 Features

### 👥 Roles
- **Employee**:
  - View all their farmers
  - View and filter all products
  - Add new farmers
  - View detailed product info by farmer
- **Farmer**:
  - View and manage only their own products
  - Filter products by name, category, or production date

### 🧰 Functionality
- Role-based login system using ASP.NET Core Identity (cookie auth)
- Full CRUD for:
  - Employees
  - Farmers
  - Products
- Product search and date filtering
- Responsive dashboard built with Bootstrap
- SQLite-backed database with EF Core
- Clean Razor Page structure and services for separation of concerns

---

## 📂 Project Structure

```plaintext
/Pages
  Index.cshtml        <-- Login page
  Home.cshtml         <-- Dashboard (role-based display)
  /Shared             <-- _Layout and partial views
/Models
  Employee.cs
  Farmer.cs
  Product.cs
/Services
  EmployeeService.cs
  FarmerService.cs
  ProductService.cs
/Data
  AppDbContext.cs
Program.cs
````

---

## 🛠 Technologies Used

* ASP.NET Core Razor Pages
* Entity Framework Core (Code-First)
* SQLite
* Bootstrap 5
* LINQ + async/await
* Cookie Authentication

---

## 🧪 How to Run

1. **Extract the Zip file:**

After diwnloading the Zip file you should extract the file in a secure place you will remember

2. **Run the application:**

Open the folder in a Visual Studio Code and Run

---

## 🧪 Test Users

> These are seeded into the database on first run:

### 👨‍💼 Employee

* Email: `aiden@agri.com`
* Password: `EmployeePassword123`

### 👨‍🌾 Farmer

* Email: `joe@farm.com`
* Password: `secret`

---

## 🧠 Developer Notes

* Database is seeded automatically in `Program.cs` if no Employees exist.
* `AppDbContext` uses EF Core migrations. You can modify models and re-migrate using:

Add-Migration InitialCreate
Update-Database

* Role-based logic is handled via Claims (`ClaimTypes.Role`, `ClaimTypes.NameIdentifier`)
* `FarmerService` and `EmployeeService` abstract most business logic from pages

---

## ✨ Future Improvements

* Hash passwords before storing them (currently plain text for testing)
* Add pagination to dashboards
* Use modals for editing products and farmers inline
* Add unit tests for services and data access
* Add user registration and email verification

---

## 🔗 GitHub Link

https://github.com/ST10273397/Prog7311.git

## 📝 License

MIT License

---

> Built by Nicholas 🧠⚙️ — a developer with big dreams in the gaming and tech world 🌍
