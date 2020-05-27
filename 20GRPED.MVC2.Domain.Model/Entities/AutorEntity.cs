using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _20GRPED.MVC2.Domain.Model.Entities
{
    public class AutorEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string UltimoNome { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        public List<LivroEntity> Livro { get; set; }
    }
}
