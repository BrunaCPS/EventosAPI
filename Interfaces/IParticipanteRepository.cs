using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IParticipanteRepository
    {
        public Task<IEnumerable<Participante>> GetParticipantes();
        public Task<Participante> GetParticipanteById(int id);
        public Task<Participante> CreateParticipante(CreateParticipanteDto participante);
        public Task UpdateParticipante(int id, UpdateParticipanteDto atualizaParticipante);
        public Task DeleteParticipante(int id);
        public Task<Participante> GetParticipanteByEmail(string email);
    }
}