using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.Domain.Model.Entities 
{ 
    //[Table("livro_entity")]
    public class LivroEntity : BaseEntity
    {
        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [Remote(
            action: "CheckIsbn", 
            controller:"Livro",
            AdditionalFields = nameof(Id))]
        public string Isbn { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        [Range(10, 3000, ErrorMessage = "Livro deve ter entre {2} e {1} páginas.")]
        public int Paginas { get; set; }

        public int AutorEntityId { get; set; }
        public AutorEntity Autor { get; set; }
    }
}
