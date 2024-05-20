using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IEventoService
    {
        public Task<IEnumerable<Evento>> GetEventos();
        public Task<Evento> GetEventoById(int id);
        public Task<ResponseDto<Evento>> CreateEvento(CreateEventoDto evento);
        public Task<ResponseDto<Evento>> UpdateEvento(int id, UpdateEventoDto atualizaEvento);
        public Task<ResponseDto<Evento>> DeleteEvento(int id);
    }
}