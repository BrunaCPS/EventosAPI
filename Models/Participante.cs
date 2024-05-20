using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventosAPI_2.Models
{
    public class Participante
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Usuário pode conter apenas letras ou dígitos...")]
        [Required(ErrorMessage = "É obrigatório o preenchimento do nome do usuário...")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "É obrigatório o preenchimento do e-mail do usuário...")]
        public string Email { get; set; }

    }
}