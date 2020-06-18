using ApiCatalogo.DTOs;
using AutoMapper;
using Catalogo_API.Models;
using Catalogo_API.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Catalogo_API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }
        
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorPreco()
        {
            var produtos =  _uof.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

        [HttpGet]
        //[ServiceFilter(typeof(ApiLoggingFilter))]
        public  ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
           var produtos =  _uof.ProdutoRepository.Get().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

       
        //[HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        [HttpGet("{id}", Name = "ObterProduto")]
        public   ActionResult<ProdutoDTO> Get(int id)
        {
            //throw new Exception("exception ao retornar produto pelo ia");           
            
            var produto =  _uof.ProdutoRepository.GetById(p => p.produtoId == id);

            if (produto == null)
            {
                return NotFound();
            }
            
            var produtosDto = _mapper.Map<ProdutoDTO>(produto);
            return produtosDto;
           
        }

        [HttpPost]
        public ActionResult Post([FromBody]ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _uof.ProdutoRepository.Add(produto);
            _uof.Commit();

            var produtosDto = _mapper.Map<ProdutoDTO>(produto);
            return new CreatedAtRouteResult("ObterProduto", new 
            { id = produto.produtoId }, produtoDto);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id,[FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);

            _uof.ProdutoRepository.Update(produto);
            _uof.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uof.ProdutoRepository.GetById(p => p.produtoId == id);
            if (produto == null)
            {
                return NotFound();
            }
            _uof.ProdutoRepository.Delete(produto);
            _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);
            return produtoDto;
        }

    }
}
