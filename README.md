# EcommerceAPI 

[Demo Link Here](https://drive.google.com/file/d/1YntOacHUGAcfvfa5-zsTR-RPXrs_h-mI/view?usp=sharing)
---

# 🏢 Ecommerce API System

🔷 **ASP.NET Core Web API – N-Tier Architecture (.NET 9)**  
🔷 **Repository Pattern + Unit Of Work + JWT Authentication**

---

# 📌 Project Overview

**Ecommerce API** is a complete backend e-commerce system built using professional clean architecture principles and modern ASP.NET Core practices.

The system allows users to:

* Browse products and categories
* Register and login securely
* Manage shopping cart
* Place orders
* Upload images
* Filter and paginate products

The project demonstrates enterprise-level backend architecture using:

* ✅ N-Tier Architecture
* ✅ Repository Pattern
* ✅ Generic Repository
* ✅ Unit of Work Pattern
* ✅ JWT Authentication
* ✅ Microsoft Identity
* ✅ Policy-Based Authorization
* ✅ DTO Pattern
* ✅ Fluent Validation
* ✅ Entity Framework Core
* ✅ Async Programming
* ✅ AutoMapper
* ✅ Image Upload
* ✅ Product Filtering
* ✅ Pagination
* ✅ Logging with Serilog
* ✅ API Versioning
* ✅ CORS
* ✅ Data Seeding
* ✅ Audit Logging

---

# 🏗 Architecture Overview

```bash
Ecommerce.API
│
├── Ecommerce.APIs      → Presentation Layer
├── Ecommerce.BLL       → Business Logic Layer
├── Ecommerce.DAL       → Data Access Layer
└── Ecommerce.Common    → Shared Models & Utilities
```

---

# 🔁 Application Flow

```bash
Client Request
      ↓
API Controller
      ↓
Manager (BLL)
      ↓
Unit Of Work
      ↓
Repository
      ↓
DbContext (EF Core)
      ↓
SQL Server
```

---

# 🧩 Layers Explained

# 1️⃣ Presentation Layer – Ecommerce.APIs

## Contains

* API Controllers
* Program.cs
* Middleware Configuration

## Responsibilities

* Handle HTTP Requests
* Return JSON Responses
* JWT Authentication
* Authorization
* API Versioning
* Logging
* CORS Configuration 

---

# 2️⃣ Business Logic Layer – Ecommerce.BLL

## Contains

* Managers
* DTOs
* Validators
* AutoMapper Profiles
* Business Rules

## Responsibilities

* Validation
* Data Transformation
* Business Logic
* Calling UnitOfWork
* Handling Responses

## Managers

* ProductManager
* CategoryManager
* CartManager
* OrderManager
* AuthManager
* ImageManager

---

# 3️⃣ Data Access Layer – Ecommerce.DAL

## Contains

* DbContext
* Repositories
* Unit Of Work
* Entity Configurations
* Seed Data

## Repositories

### Generic

* IGenericRepository
* GenericRepository

### Custom

* IProductRepository
* ICategoryRepository
* ICartRepository
* IOrderRepository

---

# 4️⃣ Common Layer – Ecommerce.Common

## Contains

* GeneralResult Wrapper
* Pagination Models
* Error Models
* Shared Utilities

---

# 🛠 Design Patterns Used

* ✅ Repository Pattern
* ✅ Generic Repository
* ✅ Unit Of Work
* ✅ DTO Pattern
* ✅ Dependency Injection
* ✅ Dependency Inversion Principle
* ✅ Fluent Validation
* ✅ AutoMapper
* ✅ Async Programming

---

# 🔐 Authentication & Authorization

Implemented using:

* JWT Authentication
* Microsoft Identity
* Policy-Based Authorization

## Features

* User Registration
* User Login
* Secure Endpoints
* Extract UserId from JWT Claims

---

# 🗄 Database

## Provider

SQL Server

## ORM

Entity Framework Core

---

# 📦 Main Entities

## 👤 User

Handled using:

```csharp
ApplicationUser : IdentityUser
```

---

## 📂 Category

One Category → Many Products

---

## 📦 Product

Many Products → One Category

---

## 🛒 Cart

One User → One Cart

---

## 🛒 CartItem

Many CartItems → One Cart

Many CartItems → One Product

---

## 📦 Order

One User → Many Orders

---

## 📦 OrderItem

Many OrderItems → One Order

Many OrderItems → One Product

---

# 🔎 Audit Logging

Implemented inside `AppDbContext`
 

---

# 🌱 Data Seeding

Default data is seeded automatically:

* Categories
* Products

---

# 📌 Main Features

# 👤 Authentication

* Register
* Login
* JWT Token Generation

---

# 📂 Categories

* Get All Categories
* Get Category By Id
* Create Category
* Update Category
* Delete Category
* Upload Category Image

---

# 📦 Products

* Get Products
* Product Details
* Create Product
* Update Product
* Delete Product
* Upload Product Image
* Filtering
* Search
* Pagination

---

# 🛒 Cart

* Add To Cart
* Update Quantity
* Remove From Cart
* Get User Cart

---

# 📦 Orders

* Place Order
* Orders History
* Get Order Details

---

# 📁 Image Upload

* Upload Images
* Save Images Locally
* Return Image URL

---

# 📖 API Endpoints

# 🔐 Auth

```http
POST /api/auth/register
POST /api/auth/login
```

---

# 📂 Categories

```http
GET    /api/categories
GET    /api/categories/{id}
POST   /api/categories
PUT    /api/categories/{id}
DELETE /api/categories/{id}
```

---

# 📦 Products

```http
GET    /api/products
GET    /api/products/{id}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
```

---

# 🛒 Cart

```http
GET    /api/cart
POST   /api/cart
PUT    /api/cart
DELETE /api/cart/{productId}
```

---

# 📦 Orders

```http
GET    /api/orders
GET    /api/orders/{id}
POST   /api/orders
```

---

# 🚀 How To Run

# 1️⃣ Configure Connection String

Inside `appsettings.json`

```json
"ConnectionStrings": {
  "Ecommerce": "Server=.;Database=EcommerceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

# 2️⃣ Apply Migrations

```bash
Add-Migration InitDb
Update-Database
```

---

# 3️⃣ Run Project

```bash
dotnet run
```

---

# 🧪 Testing

API tested using:

* Postman

Includes:

* Authentication Testing
* CRUD Operations
* Cart Flow
* Order Flow

---

# 🧠 Architectural Principles Applied

* Separation of Concerns (SoC)
* Single Responsibility Principle (SRP)
* Dependency Inversion Principle (DIP)
* DRY Principle
* Clean Architecture
* Loose Coupling

---

# 📦 Technologies Used

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* Microsoft Identity
* JWT
* FluentValidation
* AutoMapper
* Serilog
* Swagger / OpenAPI

---

# 🏆 Project Outcome

This project demonstrates:

* Enterprise-level backend architecture
* Professional API development
* Clean code organization
* Real-world e-commerce business flow
* Secure authentication system
* Advanced backend patterns

---

# 👨‍💻 Author

## Shahd Osman

Software Engineer

 
