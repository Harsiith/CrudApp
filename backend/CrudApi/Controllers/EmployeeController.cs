using CrudApi.Data;
using CrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return employee;
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = emp.Id }, emp);
        }

        // PUT: api/employee/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee emp)
        {
            if (id != emp.Id)
                return BadRequest();

            _context.Entry(emp).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/employee/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // =================== EXCEL DOWNLOAD ===================
        [HttpGet("download-excel")]
        public async Task<IActionResult> DownloadExcel()
        {
            var employees = await _context.Employees.ToListAsync();
            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Employees");

            // Header
            sheet.Cells[1, 1].Value = "ID";
            sheet.Cells[1, 2].Value = "Name";
            sheet.Cells[1, 3].Value = "Department";
            sheet.Cells[1, 4].Value = "Salary";

            int row = 2;
            foreach (var emp in employees)
            {
                sheet.Cells[row, 1].Value = emp.Id;
                sheet.Cells[row, 2].Value = emp.Name;
                sheet.Cells[row, 3].Value = emp.Department;
                sheet.Cells[row, 4].Value = emp.Salary;
                row++;
            }

            return File(
                package.GetAsByteArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Employees.xlsx"
            );
        }

        // =================== PDF DOWNLOAD ===================
        [HttpGet("download-pdf")]
        public async Task<IActionResult> DownloadPdf()
        {
            var employees = await _context.Employees.ToListAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);

                    page.Header()
                        .Text("Employee List")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Text("ID").Bold();
                        table.Cell().Text("Name").Bold();
                        table.Cell().Text("Department").Bold();
                        table.Cell().Text("Salary").Bold();

                        foreach (var emp in employees)
                        {
                            table.Cell().Text(emp.Id.ToString());
                            table.Cell().Text(emp.Name);
                            table.Cell().Text(emp.Department);
                            table.Cell().Text(emp.Salary.ToString());
                        }
                    });
                });
            });

            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", "Employees.pdf");
        }
    }
}
