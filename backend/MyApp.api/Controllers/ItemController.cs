using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.api.Data;
using MyApp.api.Models;

namespace MyApp.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _context.Items.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, Item updated)
        {
            if (id != updated.Id) return BadRequest();

            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null) return NotFound();

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
