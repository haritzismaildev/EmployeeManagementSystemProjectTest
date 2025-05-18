using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.DTOs;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobPositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobPositions
        [HttpGet]
        public async Task<IActionResult> GetJobPositions()
        {
            var jobPositions = await _context.JobPositions.ToListAsync();
            return Ok(jobPositions);
        }

        // GET: api/JobPositions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobPosition(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
                return NotFound();
            return Ok(jobPosition);
        }

        // POST: api/JobPositions
        [HttpPost]
        public async Task<IActionResult> CreateJobPosition(JobPositionDTO jobDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validasi: pastikan hanya satu posisi aktif per karyawan
            if (jobDto.IsActive)
            {
                var activeJob = await _context.JobPositions
                    .FirstOrDefaultAsync(j => j.EmployeeId == jobDto.EmployeeId && j.IsActive);
                if (activeJob != null)
                    return BadRequest("Karyawan sudah memiliki posisi aktif.");
            }

            var jobPosition = new JobPosition
            {
                EmployeeId = jobDto.EmployeeId,
                JobName = jobDto.JobName,
                StartDate = jobDto.StartDate,
                EndDate = jobDto.EndDate,
                Salary = jobDto.Salary,
                IsActive = jobDto.IsActive
            };

            _context.JobPositions.Add(jobPosition);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobPosition), new { id = jobPosition.Id }, jobPosition);
        }

        // PUT: api/JobPositions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosition(int id, JobPositionDTO jobDto)
        {
            if (id != jobDto.Id)
                return BadRequest("ID posisi pekerjaan tidak cocok.");

            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
                return NotFound();

            // Jika mengubah status ke aktif, pastikan tidak ada posisi aktif lain
            if (jobDto.IsActive && !jobPosition.IsActive)
            {
                var activeJob = await _context.JobPositions
                   .FirstOrDefaultAsync(j => j.EmployeeId == jobDto.EmployeeId && j.IsActive);
                if (activeJob != null && activeJob.Id != jobPosition.Id)
                    return BadRequest("Sudah ada posisi aktif untuk karyawan ini.");
            }

            jobPosition.JobName = jobDto.JobName;
            jobPosition.StartDate = jobDto.StartDate;
            jobPosition.EndDate = jobDto.EndDate;
            jobPosition.Salary = jobDto.Salary;
            jobPosition.IsActive = jobDto.IsActive;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/JobPositions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosition(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
                return NotFound();

            _context.JobPositions.Remove(jobPosition);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
