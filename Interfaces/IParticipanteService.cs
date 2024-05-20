using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IParticipanteService
    {
        public Task<IEnumerable<Participante>> GetParticipantes();
        public Task<Participante> GetParticipanteById(int id);
        public Task<ResponseDto<Participante>> CreateParticipante(CreateParticipanteDto participante);
        public Task<ResponseDto<Participante>> UpdateParticipante(int id, UpdateParticipanteDto atualizaParticipante);
        public Task<ResponseDto<Participante>>DeleteParticipante(int id);
    }
}