namespace Biblioteca.Domain.Dtos.Book
{
    public record BookResponse
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required int PublicationYear { get; set; }
        public required string Category { get; set; }
        public required List<string> Authors { get; set; }
    }
}
