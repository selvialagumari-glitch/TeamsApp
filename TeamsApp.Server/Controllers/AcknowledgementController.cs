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

            acknowledgement.IsAcknowledged = true;
            acknowledgement.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { success = true, acknowledgement });
        }

        // ✅ New GET API to check acknowledgment status
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAcknowledgementStatus(string email)
        {
            var acknowledgement = await _context.Acknowledgements
                .FirstOrDefaultAsync(a => a.Email == email);

            if (acknowledgement == null)
            {
                return NotFound(new { message = "Record not found" });
            }

            return Ok(new
            {
                email = acknowledgement.Email,
                isAcknowledged = acknowledgement.IsAcknowledged
            });
        }
    }
}
