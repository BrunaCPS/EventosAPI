using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IEnderecoRepository
    {
        public Task<IEnumerable<Endereco>> GetEnderecos();
        public Task<Endereco> GetEnderecoById(int id);
        public Task<Endereco> GetEnderecoByLogradouroENumero(string logradouro, int numero);
        public Task<Endereco> CreateEndereco(CreateEnderecoDto endereco);
        public Task UpdateEndereco(int id, UpdateEnderecoDto atualizaEndereco);
        public Task DeleteEndereco(int id);
    }
}