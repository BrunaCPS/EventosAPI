using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI_2.DTOs
{
    public class CreateInscricaoDto
    {
        public int ParticipanteId { get; set; }

        public int EventoId { get; set; }
    }
}