using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamsApp.Shared;
using TeamsApp.Server.Data;

namespace TeamsApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcknowledgementController : ControllerBase
    {
        private readonly MyDbContext _context;

        public AcknowledgementController(MyDbContext context)
        {
            _context = context;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> UpdateAcknowledgement([FromBody] AcknowledgementDto dto)
        {
            var acknowledgement = await _context.Acknowledgements
                .FirstOrDefaultAsync(a => a.Email == dto.Email);

            if (acknowledgement == null)
            {
                return NotFound(new { message = "Record not found" });
            }

            acknowledgement.Is_Acknowledged = true;
            acknowledgement.Updated_At = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, acknowledgement });
        }
    }
}
