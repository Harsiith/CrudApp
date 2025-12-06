using CrudApi.Data;
using CrudApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        // ----------------------- CREATE -----------------------
        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        // ----------------------- UPDATE -----------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee updatedEmployee)
        {
            var existing = await _context.Employees.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Name = updatedEmployee.Name;
            existing.Department = updatedEmployee.Department;
            existing.Salary = updatedEmployee.Salary;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // ----------------------- DELETE -----------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
