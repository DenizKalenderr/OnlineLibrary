using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        //Kullanıcı tablosunda neler olmalı

        [Required]
        public int StudentNo { get; set; }

        public string? Address { get; set; }

        public string? Faculty { get; set; }

        public string? Department { get; set; }
    }
}
