using Stoica_Darius_Lab2.Models;

namespace Stoica_Darius_Lab2.Models.ViewModels
{
    public class BookIndexData
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}