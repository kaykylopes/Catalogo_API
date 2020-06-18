using Catalogo_API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public AppDbContext _uof;
        private ProdutoRepository _produtoRepo;
        private CategoriaRepository _categoriaRepo;

        public UnitOfWork(AppDbContext contexto)
        {
            _uof = contexto;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_uof);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_uof);
            }
        }

        public void Commit()
        {
            _uof.SaveChanges();
        }

        public void Dispose()
        {
            _uof.Dispose();
        }
    }
}
