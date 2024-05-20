using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IEventoRepository
    {
        public Task<IEnumerable<Evento>> GetEventos();
        public Task<Evento> GetEventoById(int id);
        public Task<Evento> CreateEvento(CreateEventoDto evento);
        public Task UpdateEvento(int id, UpdateEventoDto atualizaEvento);
        public Task DeleteEvento(int id);
        public Task<Evento> GetEventoByNome(string descricao);

    }
}