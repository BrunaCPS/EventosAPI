using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IEnderecoService
    {
        public Task<IEnumerable<Endereco>> GetEnderecos();
        public Task<Endereco> GetEnderecoById(int id);
        public Task<ResponseDto<Endereco>> CreateEndereco(CreateEnderecoDto endereco);
        public Task<ResponseDto<Endereco>> UpdateEndereco(int id, UpdateEnderecoDto atualizaEndereco);
        public Task<ResponseDto<Endereco>> DeleteEndereco(int id);
    }
}