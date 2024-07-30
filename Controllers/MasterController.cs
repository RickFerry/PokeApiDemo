using Microsoft.AspNetCore.Mvc;
using PokeApiDemo.Data;
using PokeApiDemo.Models;

namespace PokeApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> CreateMaster([FromBody] Master master)
        {
            _context.Masters.Add(master);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateMaster), new { id = master.Id }, master);
        }
    }
}
