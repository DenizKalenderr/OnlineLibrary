using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationProject.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookName { get; set; }

        public string Explain { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(10, 5000)] // Fiyat aralığı
        public double Price { get; set; }

        [ValidateNever]
        public int BookTypeId { get; set; }
        [ForeignKey("BookTypeId")] // Hangi alanı kullanacağımızı yazdık.

        [ValidateNever]
        public BookType BookType { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }


    }
}
