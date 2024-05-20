using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using EventosAPI_2.Models;

namespace EventosAPI_2.Services
{
    public class ParticipanteService : IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;

        public ParticipanteService(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<ResponseDto<Participante>> CreateParticipante(CreateParticipanteDto participante)
        {
            var participanteExistente = await _participanteRepository.GetParticipanteByEmail(participante.Email);

            if (participanteExistente != null)
            {
                return new ResponseDto<Participante>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensagem = "Já existe um participante cadastrado com esse e-mail..."
                };
            }

            var novoParticipante = await _participanteRepository.CreateParticipante(participante);

            return new ResponseDto<Participante>()
            {
                StatusCode = HttpStatusCode.Created,
                Dado = novoParticipante,
                Mensagem = "Participante criado com sucesso!"
            };
        }

        public async Task<ResponseDto<Participante>> DeleteParticipante(int id)
        {
            var enderecoExistente = await _participanteRepository.GetParticipanteById(id);

            if (enderecoExistente == null)
            {
                return new ResponseDto<Participante>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Participante não encontrado..."
                };
            }

            await _participanteRepository.DeleteParticipante(id);

            return new ResponseDto<Participante>()
            {
                StatusCode = HttpStatusCode.OK,
                Mensagem = "Participante excluído com sucesso!"
            };
        }


        public async Task<Participante> GetParticipanteById(int id)
        {
            var participante = await _participanteRepository.GetParticipanteById(id);
            return participante;
        }

        public async Task<IEnumerable<Participante>> GetParticipantes()
        {
            var participantes = await _participanteRepository.GetParticipantes();
            return participantes;
        }

        public async Task<ResponseDto<Participante>> UpdateParticipante(int id, UpdateParticipanteDto atualizaParticipante)
        {
            var participanteExistente = await _participanteRepository.GetParticipanteById(id);

            if (participanteExistente == null)
            {
                return new ResponseDto<Participante>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Participante não encontrado..."
                };
            }

            await _participanteRepository.UpdateParticipante(id, atualizaParticipante);

            return new ResponseDto<Participante>()
            {
                StatusCode = HttpStatusCode.OK,
                Dado = participanteExistente,
                Mensagem = "Participante atualizado com sucesso!"
            };

        }
    }
}