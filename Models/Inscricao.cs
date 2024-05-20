using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI_2.Models
{
    public class Inscricao
    {
        [Key]
        [Required]
        public int Id { get; set; }

        //relacao com usuario
        [Required]
        public int? ParticipanteId { get; set; }
        public virtual Participante Participante { get; set; }

        //relacao com evento
        [Required]
        public int? EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}