using System.ComponentModel.DataAnnotations;

namespace Stoica_Darius_Lab2.Models
{
    public class Author
    {
        public int ID { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Navigation property pentru relația cu cărțile
        public ICollection<Book>? Books { get; set; }
    }
}