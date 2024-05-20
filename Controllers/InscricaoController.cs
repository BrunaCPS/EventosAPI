using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventosAPI_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly IInscricaoService _inscricaoService;

        public InscricaoController(IInscricaoService inscricaoService)
        {
            _inscricaoService = inscricaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInscricoes()
        {
            var inscricoes = await _inscricaoService.GetInscricoes();
            return Ok(inscricoes);
        }

        [HttpGet("{id}", Name = "InscricaoById")]
        public async Task<IActionResult> GetInscricaoById(int id)
        {
            var inscricao = await _inscricaoService.GetInscricaoById(id);

            if (inscricao == null)
            {
                return NotFound();
            }
            return Ok(inscricao);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInscricao([FromBody] CreateInscricaoDto novaInscricao)
        {
           var responseDTO = await _inscricaoService.CreateInscricao(novaInscricao);

            if (responseDTO.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("InscricaoById", new { id = responseDTO.Dado.Id }, responseDTO);
            }
            else
            {
                return BadRequest(responseDTO);
            }
        }

    }
}