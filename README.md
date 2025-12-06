🚀 CRUD Application (Angular + .NET 8 + SQL Server)

This project is a full-stack CRUD (Create, Read, Update, Delete) application built using:

Frontend: Angular 17

Backend: ASP.NET Core 8 Web API

Database: SQL Server (EF Core)

Extras: PDF & Excel Export, Validation, Responsive UI

📌 Features
✅ Frontend (Angular)

Add, Edit, Delete Employees

Form validation (min length, required, numbers)

Stylish UI with centered design

Export employee list to:

PDF

Excel

API integration using Angular services

✅ Backend (ASP.NET Core API)

REST API for CRUD operations

Separate controllers for:

EmployeeController → CRUD

ExportController → PDF / Excel export

Uses Entity Framework Core

Generates professional PDF using QuestPDF

Generates Excel using EPPlus

📁 Project Structure
CrudApp/
│
├── backend/
│   ├── CrudApi/
│   │   ├── Controllers/
│   │   │   ├── EmployeeController.cs
│   │   │   ├── ExportController.cs
│   │   ├── Data/AppDbContext.cs
│   │   ├── Models/Employee.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│
└── frontend/
    ├── mypp-frontend/
    │   ├── src/app/employee/
    │   │   ├── employee.component.ts
    │   │   ├── employee.component.html
    │   │   ├── employee.component.css
    │   ├── services/employee.service.ts
    │   └── ...

⚙️ Backend Setup
▶️ 1. Install Dependencies

Run inside the backend folder:

dotnet restore

▶️ 2. Update Connection String in appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CrudDb;Trusted_Connection=True;"
}

▶️ 3. Run Migrations
dotnet ef database update

▶️ 4. Start API
dotnet run


The API runs by default at:

http://localhost:5280

🌐 Frontend Setup (Angular)

Inside the Angular project:

▶️ 1. Install Dependencies
npm install

▶️ 2. Install File-Saver, XLSX, and Types
npm install file-saver xlsx
npm install --save-dev @types/file-saver

▶️ 3. Start Angular App
ng serve --open


Default URL:

http://localhost:4200

📤 Export Features
📄 PDF Export

Backend generates a table-styled PDF using QuestPDF.

GET http://localhost:5280/api/export/pdf

📊 Excel Export

Backend generates .xlsx using EPPlus.

GET http://localhost:5280/api/export/excel

🤝 Contributing

Pull requests are welcome!
For major changes, please open an issue first to discuss what you’d like to change.
