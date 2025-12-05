using CrudApi.Data;
using CrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
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

        // ----------------------- GET ALL -----------------------
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
    
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        // ----------------------- GET BY ID -----------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();

            return Ok(emp);
        }

        // ----------------------- CREATE -----------------------
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return Ok(emp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return Ok(emp);
        }


        // ----------------------- UPDATE -----------------------
        [HttpPut("{id}")]
public async Task<IActionResult> Update(int id, Employee emp)
{
    var existing = await _context.Employees.FindAsync(id);
    if (existing == null)
        return NotFound();

    existing.Name = emp.Name;
    existing.Department = emp.Department;
    existing.Age = emp.Age;

    await _context.SaveChangesAsync();
    return Ok(existing);
}


        

        // ----------------------- DELETE -----------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // ----------------------- EXPORT PDF -----------------------
        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportPdf()
        {
            var employees = await _context.Employees.ToListAsync();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header().Text("Employee Report").FontSize(22).SemiBold();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn();
                            cols.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Name").Bold();
                            header.Cell().Text("Department").Bold();
                        });

                        foreach (var e in employees)
                        {
                            table.Cell().Text(e.Name);
                            table.Cell().Text(e.Department);
                        }
                    });

                    page.Footer().AlignCenter().Text($"Generated: {DateTime.Now}");
                });
            });

            byte[] pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", "employees.pdf");
        }
    }
}
