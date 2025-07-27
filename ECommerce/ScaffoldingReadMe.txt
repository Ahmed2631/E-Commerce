E-Commerce Web Application README
ECommerce is an ASP.NET Core MVC web application that demonstrates a layered architecture for building scalable online stores.
 
features:
Product listing and categorization
Shopping cart and checkout workflow
User management via ASP.NET Core Identity
Modular design separating domain, business logic, and helpers

Project Overview :
An e-commerce platform built with ASP.NET Core MVC that provides:
User authentication & role management
Product catalog with categories
Shopping cart functionality
Wishlist feature
Order processing with Stripe payment integration
Admin management areas.


Technology Stack:
Framework: ASP.NET Core MVC
Database: SQL Server (Entity Framework Core)
Authentication: ASP.NET Core Identity
Payment Processing: Stripe API
Caching: Distributed Memory Cache
Email: Custom IEmailSender implementation
Frontend: Razor Pages with runtime compilation

Key Features:

User Management:
Role-based authentication (IdentityUser + IdentityRole)
Account locking policies
Email confirmation support

Product System:
Category management
Product listings
Shopping cart
Wishlists

Order Processing:
Order headers/details tracking
Stripe payment integration

Admin Areas:
Category/product/Users/Orders management

Prerequisites
.NET 6 SDK
SQL Server
Stripe account (for payment processing)
SMTP email service (for email functionality)


Architecture
Solution Structure
The solution (ECommerce.sln) contains four projects:

ECommerce
ASP.NET Core MVC web project
UI pages, controllers, views, and static assets

ECommerceDomains
Domain entities and data annotations
EF Core DbContext and entity configurations

BusinessLayer
Application services, business rules, and transaction management
Interfaces and implementations for product, category, cart, and order workflows

ECommerce.Helper
Reusable utilities, extension methods, and common helper classes

Layer Responsibilities
Presentation (ECommerce): Handles HTTP requests, view rendering, and client interactions.
Business (BusinessLayer): Implements use cases, validation, and orchestrates data operations.
Domain (ECommerceDomains): Defines core entities, DB mappings, and repository interfaces.
Helper (ECommerce.Helper): Provides cross-cutting concerns like logging, mapping, and common extensions.

App Link : https://e-commerce-cv.runasp.net/User
To Access as Admin:
E-Mail : Ahmed254010@ECommerce.com
Password : Ah2540$$


