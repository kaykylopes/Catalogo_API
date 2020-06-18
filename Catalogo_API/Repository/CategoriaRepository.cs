using Catalogo_API.Context;
using Catalogo_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository( AppDbContext context):base(context)
        { }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(c => c.Produtos);
        }
    }
}
