using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationProject.Models
{
    public class BookType
    {
        [Key] // Primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap Türü Adı boş bırakılamaz!")] // not null
        [MaxLength(25)]
        [DisplayName("Kitap Türü Adı")]
        public string Name { get; set; }

    }
}
