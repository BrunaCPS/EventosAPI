using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using EventosAPI.Data;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using EventosAPI_2.Models;
using Microsoft.EntityFrameworkCore;

namespace EventosAPI_2.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly EventoDbContext _eventoDbContext;
        public EventoRepository(EventoDbContext eventoDbContext)
        {
            _eventoDbContext = eventoDbContext;
        }

        public async Task<Evento> CreateEvento(CreateEventoDto evento)
        {
            var query = "INSERT INTO Eventos (Descricao, DataEvento, EnderecoId) VALUES (@Descricao, @DataEvento, @EnderecoId)";

            var parametros = new DynamicParameters();
            parametros.Add("Descricao", evento.Descricao, System.Data.DbType.String);
            parametros.Add("DataEvento", evento.DataEvento, System.Data.DbType.DateTime);
            parametros.Add("EnderecoId", evento.EnderecoId, System.Data.DbType.Int64);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var id = await conn.QueryFirstOrDefaultAsync<int>(query, parametros);

                var createEvento = new Evento
                {
                    Id = id,
                    Descricao = evento.Descricao,
                    DataEvento = evento.DataEvento,
                    EnderecoId = evento.EnderecoId
                };

                return createEvento;
            }

        }

        public async Task DeleteEvento(int id)
        {
            var query = "DELETE FROM Eventos WHERE Id = @id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<Evento> GetEventoById(int id)
        {
            var query = "SELECT * FROM Eventos WHERE Id = @Id";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var evento = await conn.QuerySingleOrDefaultAsync<Evento>(query, new { id });
                return evento;
            }

        }

        public async Task<IEnumerable<Evento>> GetEventos()
        {
            var query = "SELECT * FROM Eventos";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var eventos = await conn.QueryAsync<Evento>(query);
                return eventos.ToList();
            }

        }

        public async Task<Evento> GetEventoByNome(string descricao)
        {
            var query = @$"SELECT * FROM Eventos WHERE Descricao = '{descricao}'";

            using (var conn = _eventoDbContext.CreateConnection())
            {
                var evento = await conn.QueryFirstOrDefaultAsync<Evento>(query);
                return evento;
            }
        }


        public async Task UpdateEvento(int id, UpdateEventoDto atualizaEvento)
        {
            var query = "UPDATE Eventos SET Descricao = @Descricao, DataEvento = @DataEvento, EnderecoId = @EnderecoId WHERE Id = @Id";

            var parametros = new DynamicParameters();
            parametros.Add("Id", id, System.Data.DbType.Int32);
            parametros.Add("Descricao", atualizaEvento.Descricao, System.Data.DbType.String);
            parametros.Add("DataEvento", atualizaEvento.DataEvento, System.Data.DbType.DateTime);
            parametros.Add("EnderecoId", atualizaEvento.EnderecoId, System.Data.DbType.Int32);

            using (var conn = _eventoDbContext.CreateConnection())
            {
                await conn.ExecuteAsync(query, parametros);
            }

        }

    }
}