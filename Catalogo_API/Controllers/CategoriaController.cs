using ApiCatalogo.DTOs;
using AutoMapper;
using Catalogo_API.Models;
using Catalogo_API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Catalogo_API.Controllers
{
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        //private readonly IConfiguration _iconfiguration;
        //private readonly ILogger _Logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor do controlador CategoriasController
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="mapper"></param>
        //public CategoriaController(IUnitOfWork uof, IConfiguration iconfiguration,
        //    ILogger<CategoriaController> logger, IMapper mapper)
        //{
        //    _uof = uof;
        //    _iconfiguration = iconfiguration;
        //    _Logger = logger;
        //    _mapper = mapper;
        //}

        public CategoriaController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("teste")]
        public string GetTeste()
        {
            return $"CategoriaController - {DateTime.Now.ToLongDateString().ToString()}";
        }

        //[HttpGet("autor")]
        //public string GetAuthor()
        //{
        //    var autor = _iconfiguration["autor"];
        //    var conexao = _iconfiguration["ConnectionStrings:DefaultConnection"];
        //    return $"Autor : { autor} conexao: {conexao}";
        //}
        /// <summary>
        /// Retorna uma coleção de objetos Categoria
        /// </summary>
        /// <returns>Lista de Categorias</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _uof.CategoriaRepository.Get().ToList();
                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
               // throw new Exception();
                return categoriasDto;
            }          
            
             catch (Exception)
            {
                return  BadRequest();
            }
        }
        /// <summary>
        /// Obtém os produtos relacionados para cada categoria
        /// </summary>
        /// <returns>Objetos Categoria e respectivo Objetos Produtos</returns>
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriaProdutos()
        {
           // _Logger.LogInformation("===================GET / API/Categorias/produtos==============");

            var categorias =  _uof.CategoriaRepository.GetCategoriasProdutos().ToList();
            var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDto;
        }
        /// <summary>
        /// Obtem uma Categoria pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objetos Categoria</returns>
        [HttpGet("{id}", Name = "ObterCategoria")]
        [ProducesResponseType(typeof(ProdutoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoriaDTO> Get (int id)
        {
           // _Logger.LogInformation("===================GET / API/Categorias==============");
            var categoria = _uof.CategoriaRepository.GetById(ca => ca.CategoriaId == id);

            if(categoria == null)
            {
               return NotFound();
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDto;
        }
        /// <summary>
        /// Inclui uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST api/categorias
        ///     {
        ///        "categoriaId": 1,
        ///        "nome": "categoria1",
        ///        "imagemUrl": "http://teste.net/1.jpg"
        ///     }
        /// </remarks>
        /// <param name="categoriaDto">objeto Categoria</param>
        /// <returns>O objeto Categoria incluida</returns>
        /// <remarks>Retorna um objeto Categoria incluído</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Add(categoria);
            _uof.Commit();

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

            return  new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoriaDTO);
        }


        /// <summary>
        /// Altera uma Categoria 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoriaDto"></param>
        /// <returns>retorna 400 ou 200</returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            return Ok();
        }

        /// <summary>
        /// Deleta uma Categoria
        /// </summary>
        /// <param name="id">codigo da categoria (int) </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound();
            }
            _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return categoriaDto;
        }
            
    }
}