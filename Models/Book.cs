namespace WebApplication_bookstoreApi.Models
{
    public class Book
    {
        private static int _counter = 1;

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Pages { get; set; }
        public float Price { get; set; }
        public  string CoverImg { get; set; }
        public string Description { get; set; }
        public bool IsInStock { get; set; }

        public Book()
        {
            Id = _counter++;
        }

    }
}
