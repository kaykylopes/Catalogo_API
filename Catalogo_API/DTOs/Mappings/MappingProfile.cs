using ApiCatalogo.DTOs;
using AutoMapper;
using Catalogo_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.DTOs.Mappings
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
