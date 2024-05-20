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
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<ResponseDto<Evento>> CreateEvento(CreateEventoDto evento)
        {
            var eventoExistente = await _eventoRepository.GetEventoByNome(evento.Descricao);

            if (eventoExistente != null)
            {
                return new ResponseDto<Evento>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Mensagem = "Já existe um evento cadastrado com esse nome..."
                };
            }

            var novoEvento = await _eventoRepository.CreateEvento(evento);

            return new ResponseDto<Evento>()
            {
                StatusCode = HttpStatusCode.Created,
                Dado = novoEvento,
                Mensagem = "Evento criado com sucesso!"
            };
        }


        public async Task<ResponseDto<Evento>> DeleteEvento(int id)
        {
            var eventoExistente = await _eventoRepository.GetEventoById(id);

            if (eventoExistente == null)
            {
                return new ResponseDto<Evento>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Evento não encontrado..."
                };
            }

            await _eventoRepository.DeleteEvento(id);

            return new ResponseDto<Evento>()
            {
                StatusCode = HttpStatusCode.OK,
                Mensagem = "Evento excluído com sucesso!"
            };
        }

        public async Task<Evento> GetEventoById(int id)
        {
            var endereco = await _eventoRepository.GetEventoById(id);
            return endereco;
        }

        public async Task<IEnumerable<Evento>> GetEventos()
        {
            var enderecos = await _eventoRepository.GetEventos();
            return enderecos;
        }

        public async Task<ResponseDto<Evento>> UpdateEvento(int id, UpdateEventoDto atualizaEvento)
        {
            var eventoExistente = await _eventoRepository.GetEventoById(id);

            if (eventoExistente == null)
            {
                return new ResponseDto<Evento>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Evento não encontrado..."
                };
            }

            await _eventoRepository.UpdateEvento(id, atualizaEvento);

            return new ResponseDto<Evento>()
            {
                StatusCode = HttpStatusCode.OK,
                Dado = eventoExistente,
                Mensagem = "Evento atualizado com sucesso!"
            };

        }
    }
}