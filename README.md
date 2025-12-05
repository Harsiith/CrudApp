This repository contains a complete full-stack CRUD application built using:

Frontend: Angular

Backend: ASP.NET Core Web API (.NET 8)

Database: PostgreSQL

Extras: EPPlus 8 (Excel export), QuestPDF (PDF export)

📁 Project Structure
/
├── backend/
│   ├── CrudApi/
│   │   ├── Controllers/
│   │   │   ├── EmployeeController.cs
│   │   │   └── ExportController.cs
│   │   ├── Models/
│   │   │   └── Employee.cs
│   │   ├── Data/
│   │   │   └── AppDbContext.cs
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   └── ... (other backend files)
│
└── frontend/
    └── Angular App (components, services, UI)

⚙️ Backend Setup (ASP.NET Core + PostgreSQL)
🔧 1. Restore Dependencies
cd backend/CrudApi
dotnet restore

🗄️ 2. PostgreSQL Configuration

Update appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=cruddb;Username=postgres;Password=yourpassword"
}

🧱 3. Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update

▶️ 4. Run Backend
dotnet run


API will run at:

https://localhost:7092
http://localhost:5092

📄 Available Backend Endpoints
CRUD (Employee)
Method	Endpoint	Description
GET	/api/employee	Get all employees
GET	/api/employee/{id}	Get employee by ID
POST	/api/employee	Add new employee
PUT	/api/employee/{id}	Update employee
DELETE	/api/employee/{id}	Delete employee
Export
Endpoint	Type	Description
/api/export/excel	GET	Download Excel file
/api/export/pdf	GET	Download PDF report
🖥️ Frontend Setup (Angular)
▶️ 1. Install Dependencies
cd frontend
npm install

▶️ 2. Run Angular App
ng serve -o


Runs on:

http://localhost:4200/

🔄 API Integration

Update API base URL inside Angular service:

src/app/services/employee.service.ts

apiUrl = 'http://localhost:5092/api';

🧰 Tech Stack Used
Backend

ASP.NET Core Web API (.NET 8)

Entity Framework Core

PostgreSQL

EPPlus 8 (Excel Export)

QuestPDF (PDF Reports)

Swagger (API Docs)

Frontend

Angular

Bootstrap / CSS styling

HttpClient for API calls

📦 Features
✔ CRUD Operations (Create, Read, Update, Delete)
✔ Form Validation
✔ Fetch Single Employee
✔ Fetch All Employees
✔ Export Employee List as Excel (.xlsx)
✔ Export Employee List as PDF
✔ PostgreSQL Integration
✔ Clean API + Angular UI
📝 How to Upload to GitHub
git init
git add .
git commit -m "Initial Commit - Full stack CRUD App"
git branch -M main
git remote add origin <your-repo-url>
git push -u origin main

🤝 Contributions

Pull requests are welcome!

📜 License

This project is completely free to use and modify.
