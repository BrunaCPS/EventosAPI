using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventosAPI.Data;
using EventosAPI_2.DTOs;
using EventosAPI_2.Interfaces;
using EventosAPI_2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventosAPI_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventoController : ControllerBase
    {
        //private readonly IEventoRepository _eventoRepository;
        private readonly IEventoService _eventoService;

        public EventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventos()
        {
            var eventos = await _eventoService.GetEventos();
            return Ok(eventos);
        }

        [HttpGet("{id}", Name = "EventoById")]
        public async Task<IActionResult> GetEventoById(int id)
        {
            var evento = await _eventoService.GetEventoById(id);

            if (evento == null)
            {
                return NotFound();
            }
            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvento([FromBody] CreateEventoDto novoEvento)
        {
            var responseDTO = await _eventoService.CreateEvento(novoEvento);

            if (responseDTO.StatusCode == HttpStatusCode.Created)
            {
                return CreatedAtRoute("EventoById", new { id = responseDTO.Dado.Id }, responseDTO);
            }
            else
            {
                return BadRequest(responseDTO);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvento(int id, [FromBody] UpdateEventoDto atualizaEvento)
        {

           var responseDTO = await _eventoService.UpdateEvento(id, atualizaEvento);

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
        public async Task<IActionResult> DeleteEvento(int id)
        {

            var responseDTO = await _eventoService.DeleteEvento(id);

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