namespace Biblioteca.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Author(string name, List<Book> books)
        {
            Name = name;
            Books = books;
        }

        private Author() { }

        public string Name { get; private set; }
        public List<Book> Books { get; private set; }
    }
}
