﻿namespace Projeto.Business.Models
{
    public class User: Entity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Profile Profile { get; set; }
    }
}
