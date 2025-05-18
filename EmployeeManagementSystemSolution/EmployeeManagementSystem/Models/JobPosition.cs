using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class JobPosition
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        [Required]
        public string JobName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        // Field opsional/nullable
        public DateTime? EndDate { get; set; }

        [Required]
        public decimal Salary { get; set; }

        // Status: true = aktif, false = non-aktif
        public bool IsActive { get; set; }
    }
}
