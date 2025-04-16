namespace Biblioteca.Domain.Dtos.Library
{
    public record LoanBook
    {
        public string? Username { get; set; }
        public string? BookTitle { get; private set; }
        public int? QuantityDaysToReturn { get; set; }
        public decimal? LoanValue { get; set; }
    }
}
