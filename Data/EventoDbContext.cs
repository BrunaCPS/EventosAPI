using System.Data;
using EventosAPI_2.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI.Data;

public class EventoDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    public EventoDbContext(DbContextOptions<EventoDbContext> opts, IConfiguration configuration) : base(opts)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("EventoDbConnection");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Participante> Participantes { get; set; }
    public DbSet<Inscricao> Inscricoes { get; set; }
}