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
    public class InscricaoService : IInscricaoService
    {
        private readonly IInscricaoRepository _inscricaoRepository;

        public InscricaoService(IInscricaoRepository inscricaoRepository)
        {
            _inscricaoRepository = inscricaoRepository;
        }

        public async Task<ResponseDto<Inscricao>> CreateInscricao(CreateInscricaoDto inscricao)
        {
            var inscricaoExistente = await _inscricaoRepository.GetParticipanteEventoById(inscricao.EventoId, inscricao.ParticipanteId);

            if (inscricaoExistente != null)
            {
                return new ResponseDto<Inscricao>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensagem = "Esse usuário já está cadastrado nesse evento..."
                };
            }

            var novaInscricao = await _inscricaoRepository.CreateInscricao(inscricao);

            return new ResponseDto<Inscricao>()
            {
                StatusCode = HttpStatusCode.Created,
                Dado = novaInscricao,
                Mensagem = "Inscrição feita com sucesso!"
            };
        }

        public async Task<Inscricao> GetInscricaoById(int id)
        {
            var inscricao = await _inscricaoRepository.GetInscricaoById(id);
            return inscricao;
        }

        public async Task<IEnumerable<Inscricao>> GetInscricoes()
        {
            var inscricoes = await _inscricaoRepository.GetInscricoes();
            return inscricoes;
        }
    }
}