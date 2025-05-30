﻿using Biblioteca.Domain.Enumerators;

namespace Biblioteca.Domain.Entities
{
    public class Book : BaseEntity
    {
        public Book(string title, int publicationYear, Category category, List<Author> authors)
        {
            Title = title;
            PublicationYear = publicationYear;
            Category = category;
            Authors = authors;
        }

        private Book() { }
        public string Title { get; private set; }
        public int PublicationYear { get; private set; }
        public Category Category { get; private set; }
        public List<Author> Authors { get; private set; }
        public List<Loan> Loans { get; private set; } = [];

        public void Update(string title, int publicationYear, Category category, List<Author> authors)
        {
            Title = title;
            PublicationYear = publicationYear;
            Category = category;
            Authors = authors;
        }
    }
}
