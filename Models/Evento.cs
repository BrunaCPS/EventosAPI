using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI_2.Models
{
    public class Evento
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataEvento { get; set; }

        //relacionamento com Entidade Endereco
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }

        //ICOLLECTION como lista para armazenar as incricoes
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}