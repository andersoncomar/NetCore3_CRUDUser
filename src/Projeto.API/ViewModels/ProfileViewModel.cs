using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace Projeto.API.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }

        public int Type { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(5, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Endereco { get; set; }
    }
}
