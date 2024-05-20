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
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly EventoDbContext _eventoDbContext;
        public ParticipanteRepository(EventoDbContext eventoDbContext)
        {
            _eventoDbContext = eventoDbContext;
        }
        public async Task<Participante> CreateParticipante(CreateParticipanteDto participante)
        {
            var query = "INSERT INTO Participantes (UserName, Email) VALUES (@UserName, @Email)";

            var parametros = new DynamicParameters();
            parametros.Add("UserName", participante.UserName, System.Data.DbType.String);
            parametros.Add("Email", participante.Email, System.Data.DbType.String);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var id = await conn.QuerySingleOrDefaultAsync<int>(query, parametros);

                var createParticipante = new Participante
                {
                    Id = id,
                    UserName = participante.UserName,
                    Email = participante.Email,
                };

                return createParticipante;
            }
        }

        public async Task DeleteParticipante(int id)
        {
            var query = "DELETE FROM Participantes WHERE Id = @id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Participante> GetParticipanteById(int id)
        {
            var query = "SELECT * FROM Participantes WHERE Id = @Id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var participante = await conn.QuerySingleOrDefaultAsync<Participante>(query, new { id });
                return participante!;
            }
        }

        public async Task<IEnumerable<Participante>> GetParticipantes()
        {
            var query = "SELECT * FROM Participantes";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var participantes = await conn.QueryAsync<Participante>(query);
                return participantes.ToList();
            }
        }

        public async Task UpdateParticipante(int id, UpdateParticipanteDto atualizaParticipante)
        {
            var query = "UPDATE Participantes SET UserName = @UserName, Email = @Email WHERE Id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Id", id, System.Data.DbType.Int32);
            parametros.Add("UserName", atualizaParticipante.UserName, System.Data.DbType.String);
            parametros.Add("Email", atualizaParticipante.Email, System.Data.DbType.String);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, parametros);
            }
        }

        public async Task<Participante> GetParticipanteByEmail(string email)
        {
            var query = @$"SELECT * FROM Participantes WHERE Email = '{email}'";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var participante = await conn.QueryFirstOrDefaultAsync<Participante>(query);
                return participante;
            }
        }

    }
}