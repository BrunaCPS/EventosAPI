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
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;

        public EnderecoController(IEnderecoService enderecoService)
        {
            _enderecoService = enderecoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEnderecos()
        {
            var enderecos = await _enderecoService.GetEnderecos();
            return Ok(enderecos);
        }

        [HttpGet("{id}", Name = "EnderecoById")]
        public async Task<IActionResult> GetEnderecoById(int id)
        {
            var endereco = await _enderecoService.GetEnderecoById(id);

            if (endereco == null)
            {
                return NotFound();
            }
            return Ok(endereco);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEndereco([FromBody] CreateEnderecoDto novoEndereco)
        {
            var responseDTO = await _enderecoService.CreateEndereco(novoEndereco);

            if (responseDTO.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("EnderecoById", new { id = responseDTO.Dado.Id }, responseDTO);
            }
            else
            {
                return BadRequest(responseDTO);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEndereco(int id, [FromBody] UpdateEnderecoDto atualizaEndereco)
        {
            var responseDTO = await _enderecoService.UpdateEndereco(id, atualizaEndereco);

            if (responseDTO.StatusCode == HttpStatusCode.OK)
            {
                return Ok(responseDTO);
            }
            else
            {
                return NotFound(responseDTO);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {

            var responseDTO = await _enderecoService.DeleteEndereco(id);

            if (responseDTO.StatusCode == HttpStatusCode.OK)
            {
                return Ok(responseDTO);
            }
            else
            {
                return NotFound(responseDTO);
            }
        }

    }
}