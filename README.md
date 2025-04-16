# Biblioteca

## Guia do Entity Framework Core

Este projeto utiliza o Entity Framework Core como ORM (Object-Relational Mapping) para gerenciar a persistência de dados. Este guia explica os principais conceitos do Entity Framework para quem não está familiarizado com a tecnologia.

### O que é Entity Framework?

Entity Framework Core é um ORM (Object-Relational Mapping) desenvolvido pela Microsoft que permite aos desenvolvedores trabalhar com um banco de dados usando objetos .NET. Ele elimina a necessidade de escrever a maior parte do código de acesso a dados que normalmente seria necessário.

### Principais Conceitos

#### 1. DbContext

O `DbContext` é a principal classe do Entity Framework que coordena a funcionalidade de entidades para um modelo de dados específico.

No nosso projeto, temos o `ApplicationContext`:

```csharp
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
```

O `DbContext` é responsável por:
- Gerenciar conexões com o banco de dados
- Rastrear mudanças nas entidades
- Persistir dados no banco de dados
- Consultar dados do banco de dados

#### 2. Entidades

As entidades são classes que representam tabelas no banco de dados. No nosso projeto, temos:

- `Book` (Livro)
- `Author` (Autor)
- `Loan` (Empréstimo)

Exemplo da entidade `Book`:

```csharp
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
```

Todas as entidades herdam de `BaseEntity`, que contém propriedades comuns:

```csharp
public class BaseEntity
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; private set; }
}
```

#### 3. Configuração de Entidades (Fluent API)

O Entity Framework permite configurar o mapeamento entre entidades e tabelas usando a Fluent API. No nosso projeto, usamos classes de mapeamento:

```csharp
public class BookMap : BaseMap<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x => x.Title)
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.PublicationYear)
            .IsRequired();

        builder.Property(x => x.Category)
            .IsRequired();

        builder.HasIndex(x => new { x.Title, x.PublicationYear }).IsUnique();
        builder.HasIndex(x => x.Title);

        base.Configure(builder);
    }
}
```

A Fluent API permite:
- Definir chaves primárias
- Configurar propriedades (tamanho máximo, obrigatoriedade, etc.)
- Definir relacionamentos entre entidades
- Criar índices
- Configurar convenções de nomenclatura

#### 4. Migrações

As migrações são uma forma de evoluir o esquema do banco de dados de maneira controlada. O Entity Framework gera migrações com base nas mudanças no modelo de dados.

Exemplo de migração:

```csharp
public partial class FirstMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Books",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                // outras colunas...
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Books", x => x.Id);
            });
        
        // outras tabelas e relacionamentos...
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // lógica para reverter a migração...
    }
}
```

#### 5. Operações CRUD

O Entity Framework facilita as operações CRUD (Create, Read, Update, Delete):

**Create (Criar)**:
```csharp
var book = new Book(createBook.Title!, createBook.PublicationYear!.Value, category, authors!);
var result = await _applicationContext.AddAsync(book);
await _applicationContext.SaveChangesAsync();
```

**Read (Ler)**:
```csharp
var books = _applicationContext.Books
    .Include(x => x.Authors)
    .Select(x => new BookResponse
    {
        Id = x.Id,
        Title = x.Title,
        // outras propriedades...
    }).ToListAsync();
```

**Update (Atualizar)**:
```csharp
book.Update(updateBook.Title, updateBook.PublicationYear!.Value, category, authors);
_applicationContext.Update(book);
await _applicationContext.SaveChangesAsync();
```

**Delete (Excluir)**:
```csharp
_applicationContext.Remove(book);
await _applicationContext.SaveChangesAsync();
```

#### 6. Consultas com LINQ

O Entity Framework permite usar LINQ (Language Integrated Query) para consultar dados:

```csharp
var book = _applicationContext.Books
    .Include(x => x.Authors)
    .Where(x => x.Id == id)
    .Select(x => new BookResponse
    {
        Id = x.Id,
        Title = x.Title,
        // outras propriedades...
    })
    .FirstOrDefaultAsync();
```

Recursos de consulta:
- Filtros (Where)
- Ordenação (OrderBy)
- Agrupamento (GroupBy)
- Junções (Join)
- Carregamento de entidades relacionadas (Include)
- Projeções (Select)

#### 7. Injeção de Dependência

O Entity Framework se integra facilmente com o sistema de injeção de dependência do ASP.NET Core:

```csharp
services.AddDbContext<ApplicationContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});
```

Isso permite injetar o `ApplicationContext` em serviços:

```csharp
public class BookService : IBookService
{
    private readonly ApplicationContext _applicationContext;

    public BookService(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    // métodos do serviço...
}
```

### Vantagens do Entity Framework

1. **Produtividade**: Reduz a quantidade de código necessário para acessar dados
2. **Manutenção**: Centraliza a lógica de acesso a dados
3. **Independência de banco de dados**: Facilita a troca de provedores de banco de dados
4. **Consultas tipadas**: Detecta erros em tempo de compilação
5. **Gerenciamento de mudanças**: Facilita a evolução do esquema do banco de dados

### Boas Práticas

1. **Separação de Responsabilidades**: Use camadas de serviço para encapsular a lógica de negócios
2. **Entidades Ricas**: Implemente comportamentos nas entidades para garantir a consistência dos dados
3. **Consultas Otimizadas**: Use Include apenas quando necessário e projete resultados para DTOs
4. **Transações**: Use transações para operações que envolvem múltiplas entidades
5. **Migrações**: Mantenha as migrações versionadas no controle de código-fonte

### Apresentação

[Google Docs](https://docs.google.com/presentation/d/12y2c3lwxaGc3s3AY1Ox78-DLGJHdYUpYskzou9wPyyY/edit?usp=sharing)
