using System;
using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace _20GRPED.MVC2.A02.Domain.Model.Entities 
{ 
    //[Table("livro_entity")]
    public class LivroEntity
    {
        //[Column("LivroPk")]
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [Remote( //Reforçando que o Remote não deveria estar aqui com a referencia do Microsoft.AspNetCore.Mvc
            //deveria estar apenas na ViewModel
            action: "CheckIsbn", 
            controller:"Livro",
            AdditionalFields = nameof(Id))]
        public string Isbn { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        [Range(10, 3000, ErrorMessage = "Livro deve ter entre {2} e {1} páginas.")]
        public int Paginas { get; set; }
    }
}
