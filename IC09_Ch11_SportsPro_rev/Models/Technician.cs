﻿using System.ComponentModel.DataAnnotations;

namespace IC09_Ch11_SportsPro_rev.Models
{
    public class Technician
    {
        public int TechnicianID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone.")]
        public string Phone { get; set; } = string.Empty;
    }
}
