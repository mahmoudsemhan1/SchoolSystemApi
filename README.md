# 🎓 School System API

This is a RESTful Web API developed using **ASP.NET Core 8** for managing a simple school system. The API includes core features like student registration, subject management, and grade tracking. It follows clean architecture principles, separating responsibilities across different layers (API, Application, Domain, and Infrastructure).

---

## 📦 Technologies Used

- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **FluentValidation**
- **MediatR**
- **Swagger (Swashbuckle)** for API documentation
- **Repository & Service Pattern**

---

## 🧠 Features

- 🧑‍🎓 Manage students (Add, Update, Delete, View)
- 📚 Manage subjects and assign them to students
- 📝 Record and view grades
- ⚙️ Clean separation of concerns with layered architecture
- 📄 Swagger UI for interactive API testing

---

## 🗂️ Project Structure

SchoolSystemApi/
├── SchoolSystem.Api --> Entry point / Controllers / Swagger config
├── SchoolSystem.Application --> DTOs, Interfaces, Business Logic
├── SchoolSystem.Domain --> Core Models and Enums
├── SchoolSystem.Infrastructure--> DB context, Repository Implementation

