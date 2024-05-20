using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EventosAPI.Data;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using EventosAPI_2.Models;

namespace EventosAPI_2.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly EventoDbContext _eventoDbContext;
        public EnderecoRepository(EventoDbContext eventoDbContext)
        {
            _eventoDbContext = eventoDbContext;
        }

        public async Task<Endereco> CreateEndereco(CreateEnderecoDto endereco)
        {
            var query = "INSERT INTO Enderecos (Logradouro, Numero) VALUES (@Logradouro, @Numero)";

            var parametros = new DynamicParameters();
            parametros.Add("Logradouro", endereco.Logradouro, System.Data.DbType.String);
            parametros.Add("Numero", endereco.Numero, System.Data.DbType.Int32);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var id = await conn.QuerySingleOrDefaultAsync<int>(query, parametros);

                var createEndereco = new Endereco
                {
                    Id = id,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                };

                return createEndereco;
            }
        }

        public async Task DeleteEndereco(int id)
        {
            var query = "DELETE FROM Enderecos WHERE Id = @id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { id });
            }
        }


        public async Task<Endereco> GetEnderecoById(int id)
        {
            var query = "SELECT * FROM Enderecos WHERE Id = @Id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var endereco = await conn.QuerySingleOrDefaultAsync<Endereco>(query, new { id });
                return endereco!;
            }
        }

        public async Task<Endereco> GetEnderecoByLogradouroENumero(string logradouro, int numero)
        {
            var query = @$"SELECT Numero FROM Enderecos WHERE Logradouro = '{logradouro}' AND Numero = {numero}";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var endereco = await conn.QueryFirstOrDefaultAsync<Endereco>(query);
                return endereco;
            }
        }

        public async Task<IEnumerable<Endereco>> GetEnderecos()
        {
            var query = "SELECT * FROM Enderecos";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var enderecos = await conn.QueryAsync<Endereco>(query);
                return enderecos.ToList();
            }
        }



        public async Task UpdateEndereco(int id, UpdateEnderecoDto atualizaEndereco)
        {
            var query = "UPDATE Enderecos SET Logradouro = @Logradouro, Numero = @Numero WHERE Id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Id", id, System.Data.DbType.Int32);
            parametros.Add("Logradouro", atualizaEndereco.Logradouro, System.Data.DbType.String);
            parametros.Add("Numero", atualizaEndereco.Numero, System.Data.DbType.Int32);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, parametros);
            }
        }
    }
}