using Projeto.Business.Enums;

namespace Projeto.Business.Models
{
    public class Profile : Entity
    {
        public EProfile Type { get; set; }

        public string Avatar { get; set; }

        public string Documento { get; set; }

        public string Endereco { get; set; }
    }
}
