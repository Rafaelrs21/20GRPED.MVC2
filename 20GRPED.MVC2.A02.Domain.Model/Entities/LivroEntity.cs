using System;
using System.ComponentModel.DataAnnotations;

namespace _20GRPED.MVC2.A02.Domain.Model.Entities 
{ 
    public class LivroEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        public string Isbn { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        [Range(10, 3000, ErrorMessage = "Livro deve ter entre {2} e {1} páginas.")]
        public int Paginas { get; set; }
    }
}
