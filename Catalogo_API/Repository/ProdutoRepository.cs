using Catalogo_API.Context;
using Catalogo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) :base (contexto)
        {

        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }
    }
}
