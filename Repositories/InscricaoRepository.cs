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
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly EventoDbContext _eventoDbContext;
        public InscricaoRepository(EventoDbContext eventoDbContext)
        {
            _eventoDbContext = eventoDbContext;
        }

        public async Task<Inscricao> CreateInscricao(CreateInscricaoDto inscricao)
        {
            var query = "INSERT INTO Inscricoes (ParticipanteId, EventoId) VALUES (@ParticipanteId, @EventoId)";

            var parametros = new DynamicParameters();
            parametros.Add("ParticipanteId", inscricao.ParticipanteId, System.Data.DbType.Int64);
            parametros.Add("EventoId", inscricao.EventoId, System.Data.DbType.Int64);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var id = await conn.QuerySingleOrDefaultAsync<int>(query, parametros);

                var createInscricao = new Inscricao
                {
                    Id = id,
                    ParticipanteId = inscricao.ParticipanteId,
                    EventoId = inscricao.EventoId,
                };

                return createInscricao;
            }
        }

        public async Task DeleteInscricao(int id)
        {
            var query = "DELETE FROM Inscricoes WHERE Id = @id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Inscricao> GetInscricaoById(int id)
        {
           var query = "SELECT * FROM Inscricoes WHERE Id = @Id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var inscricao = await conn.QuerySingleOrDefaultAsync<Inscricao>(query, new { id });
                return inscricao!;
            }
        }

        public async Task<IEnumerable<Inscricao>> GetInscricoes()
        {
            var query = "SELECT * FROM Inscricoes";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var inscricoes = await conn.QueryAsync<Inscricao>(query);
                return inscricoes.ToList();
            }
        }

        public async Task<Inscricao> GetParticipanteEventoById(int idParticipante, int idEvento)
        {
            var query = @$"SELECT * FROM Inscricoes WHERE ParticipanteId = {idParticipante} AND EventoId = {idEvento}";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var inscricao = await conn.QueryFirstOrDefaultAsync<Inscricao>(query);
                return inscricao;
            }
        }
    }
}