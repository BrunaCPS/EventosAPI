using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using EventosAPI_2.Models;
using EventosAPI_2.Repositories;

namespace EventosAPI_2.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;

        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }


        public async Task<ResponseDto<Endereco>> CreateEndereco(CreateEnderecoDto endereco)
        {
            var responseDTO = new ResponseDto<Endereco>();
            var enderecoExistente = await _enderecoRepository.GetEnderecoByLogradouroENumero(endereco.Logradouro, endereco.Numero);

            if (enderecoExistente != null)
            {
                responseDTO.StatusCode = HttpStatusCode.BadRequest;
                responseDTO.Mensagem = "Já existe um endereço cadastrado com essas informações...";
                return responseDTO;
            }

            var novoEndereco = await _enderecoRepository.CreateEndereco(endereco);

            responseDTO.StatusCode = HttpStatusCode.Created;
            responseDTO.Dado = novoEndereco;
            responseDTO.Mensagem = "Endereço criado com sucesso!";

            return responseDTO;
        }
        public async Task<ResponseDto<Endereco>> DeleteEndereco(int id)
        {
            var enderecoExistente = await _enderecoRepository.GetEnderecoById(id);

            if (enderecoExistente == null)
            {
                return new ResponseDto<Endereco>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Endereço não encontrado..."
                };
            }

            await _enderecoRepository.DeleteEndereco(id);

            return new ResponseDto<Endereco>()
            {
                StatusCode = HttpStatusCode.OK,
                Mensagem = "Endereço excluído com sucesso!"
            };
        }

        public async Task<Endereco> GetEnderecoById(int id)
        {
            var endereco = await _enderecoRepository.GetEnderecoById(id);
            return endereco;
        }

        public async Task<IEnumerable<Endereco>> GetEnderecos()
        {
            var enderecos = await _enderecoRepository.GetEnderecos();
            return enderecos;
        }

        public async Task<ResponseDto<Endereco>> UpdateEndereco(int id, UpdateEnderecoDto atualizaEndereco)
        {
            var enderecoExistente = await _enderecoRepository.GetEnderecoById(id);

            if (enderecoExistente == null)
            {
                return new ResponseDto<Endereco>()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Mensagem = "Endereço não encontrado..."
                };
            }

            await _enderecoRepository.UpdateEndereco(id, atualizaEndereco);

            return new ResponseDto<Endereco>()
            {
                StatusCode = HttpStatusCode.OK,
                Dado = enderecoExistente,
                Mensagem = "Endereço atualizado com sucesso!"
            };

        }
    }
}