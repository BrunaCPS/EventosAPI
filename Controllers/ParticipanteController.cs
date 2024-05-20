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
    public class ParticipanteController : ControllerBase
    {
        private readonly IParticipanteService _participanteService;

        public ParticipanteController(IParticipanteService participanteService)
        {
            _participanteService = participanteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetParticipantes()
        {
            var participantes = await _participanteService.GetParticipantes();
            return Ok(participantes);
        }

        [HttpGet("{id}", Name = "ParticipanteById")]
        public async Task<IActionResult> GetParticipanteById(int id)
        {
            var participante = await _participanteService.GetParticipanteById(id);

            if (participante == null)
            {
                return NotFound();
            }
            return Ok(participante);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipante([FromBody] CreateParticipanteDto novoParticipante)
        {
            var responseDTO = await _participanteService.CreateParticipante(novoParticipante);

            if (responseDTO.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("ParticipanteById", new { id = responseDTO.Dado.Id }, responseDTO);
            }
            else
            {
                return BadRequest(responseDTO);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParticipante(int id, [FromBody] UpdateParticipanteDto atualizaParticipante)
        {

           var responseDTO = await _participanteService.UpdateParticipante(id, atualizaParticipante);

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
        public async Task<IActionResult> DeleteParticipante(int id)
        {

            var responseDTO = await _participanteService.DeleteParticipante(id);

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