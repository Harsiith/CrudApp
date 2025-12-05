using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using CrudApi.Data;
using System.Linq;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExportController(AppDbContext context)
        {
            _context = context;
        }

        // ---------------------- EXPORT EXCEL ----------------------
        [HttpGet("excel")]
        public IActionResult ExportExcel()
        {

            var employees = _context.Employees.ToList();

            using var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Employees");

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

        // ---------------------- EXPORT PDF ----------------------
        [HttpGet("pdf")]
        public IActionResult ExportPdf()
        {
            var employees = _context.Employees.ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Employee Report")
                        .FontSize(22)
                        .SemiBold()
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

                        table.Cell().Text("ID").SemiBold();
                        table.Cell().Text("Name").SemiBold();
                        table.Cell().Text("Dept").SemiBold();
                        table.Cell().Text("Salary").SemiBold();

                        foreach (var emp in employees)
                        {
                            table.Cell().Text(emp.Id.ToString());
                            table.Cell().Text(emp.Name);
                            table.Cell().Text(emp.Department);
                            table.Cell().Text(emp.Salary.ToString());
                        }
                    });

                    page.Footer().AlignCenter().Text($"Generated on {DateTime.Now:yyyy-MM-dd HH:mm}");
                });
            });

            var pdf = document.GeneratePdf();

            return File(pdf, "application/pdf", "Employees.pdf");
        }
    }
}
