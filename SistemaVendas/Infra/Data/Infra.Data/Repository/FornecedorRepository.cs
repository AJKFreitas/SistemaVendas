using MySql.Data.MySqlClient;
using SistemaVendas.Core.Domains.Fornecedores.Entities;
using SistemaVendas.Core.Domains.Fornecedores.Interfaces;
using System;
using System.Collections.Generic;

namespace SistemaVendas.Infra.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository

    {
         protected readonly VendasEFContext _context;
        private bool disposed = false;

        public FornecedorRepository(VendasEFContext context, bool disposed)
        {
            _context = context;
            this.disposed = disposed;
        }

        public void Delete(Guid EntityID)
        {
            try
            {
                var fornecedor = _context.Fornecedores.Find(EntityID);
                if (fornecedor != null)
                    _context.Remove(fornecedor);
            }
            catch (MySqlException e)
            {

                throw e;
            }
        }

        public  IEnumerable<Fornecedor> GetAll()
        {
            
            try
            {
                return  _context.Fornecedores;
            }
            catch (MySqlException e)
            {

                throw e;
            }
        }

        public  Fornecedor GetById(Guid EntityID)
        {
            try
            {
                return _context.Fornecedores.Find(EntityID);
            }
            catch (MySqlException e)
            {

                throw e;
            }
        }

        public void Insert(Fornecedor Entity)
        {
            try
            {
                Fornecedor fornecedor = new Fornecedor(Entity.Nome, Entity.Telefone, Entity.CNPJ, Entity.Email, Entity.Senha);
                _context.Fornecedores.Add(Entity);
                _context.SaveChangesAsync();
            }
            catch (MySqlException e)
            {

                throw e;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
            Dispose();
        }

        public void Update(Fornecedor Entity)
        {
            try
            {
                _context.Fornecedores.Update(Entity);
            }
            catch (MySqlException e)
            {
                Dispose();
                throw e;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }
}
