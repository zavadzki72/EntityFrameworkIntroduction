namespace Biblioteca.Domain.Dtos.Book
{
    public record CreateBook
    {
        public string? Title { get; set; }
        public int? PublicationYear { get; set; }
        public string? Category { get; set; }
        public List<string> Authors { get; set; } = [];
    }
}
