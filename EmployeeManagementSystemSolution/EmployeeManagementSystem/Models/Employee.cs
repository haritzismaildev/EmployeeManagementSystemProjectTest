using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        // Tanggal lahir – akan dienkripsi sebelum disimpan
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; }  // Contoh: "Male", "Female", dsb.

        public string Address { get; set; }

        public ICollection<JobPosition> JobPositions { get; set; }
    }
}
