﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogo_API.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Required]
        public int produtoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(80, ErrorMessage = "O nome deve ter no máximo {1} e no mínimo {2} caracteres",
           MinimumLength = 5)]
        public string Nome { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo {1} caracteres")]
        public string     Descricao { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(8,2)")]
        [Range(1,100000, ErrorMessage = " O preço deve ser entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string ImagemUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }
    }
}
