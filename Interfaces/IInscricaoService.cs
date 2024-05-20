using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Models;

namespace EventosAPI_2.Interfaces
{
    public interface IInscricaoService
    {
        public Task<IEnumerable<Inscricao>> GetInscricoes();
        public Task<Inscricao> GetInscricaoById(int id);
        public Task<ResponseDto<Inscricao>> CreateInscricao(CreateInscricaoDto inscricao);

    }
}