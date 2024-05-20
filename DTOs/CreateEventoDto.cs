using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI_2.DTOs
{
    public class CreateEventoDto
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataEvento { get; set; }
        [Required]
        public int EnderecoId { get; set; }
    }
}