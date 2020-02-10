using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models.Usuario
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public Usuario()
        {
        }

        public Usuario(Guid id, string nome, string login, string senha)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public Usuario(string nome, string login, string senha)
        {
            Id = new Guid();
            Nome = nome;
            Login = login;
            Senha = senha;
        }
    }
}
