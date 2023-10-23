using bookDemo.Models;

namespace bookDemo.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; }    
        static ApplicationContext()
        {
            //InMemory dataset
            Books = new List<Book>()
            {
                new Book(){Id=1, Title = "İnce Memed", Price = 75},
                new Book(){Id=2, Title = "İnce Memed 2", Price = 100},
                new Book(){Id=3, Title = "İnce Memed 3", Price = 125}
            };
        }
    }
}
