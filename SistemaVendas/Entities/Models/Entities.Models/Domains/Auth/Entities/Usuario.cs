﻿using SistemaVendas.Core.Domains.Auth.Interfaces;
using SistemaVendas.Core.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaVendas.Core.Domains.Auth.Entities
{


    public class Usuario : TEntity , IUsuario
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }

        public Usuario(string nome, string email, string senha, string role) : this(nome, email, senha)
        {
            Role = role;
        }

        public Usuario(string nome, string email, string senha, string role, bool isAdmin) : this(nome, email, senha, role)
        {
            IsAdmin = isAdmin;
        }

        public Usuario()
        {
        }

        public Usuario(Guid id, string nome, string email, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public Usuario(string nome, string email, string senha)
        {
            Id = new Guid();
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}


