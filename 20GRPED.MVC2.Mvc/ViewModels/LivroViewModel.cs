using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _20GRPED.MVC2.Mvc.ViewModels
{
    public class LivroViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 10)]
        [Remote(
            action: "CheckIsbn",
            controller: "Livro",
            AdditionalFields = nameof(Id))]
        public string Isbn { get; set; }

        [DataType(DataType.Date)]
        public DateTime Lancamento { get; set; }

        [Range(10, 3000, ErrorMessage = "Livro deve ter entre {2} e {1} páginas.")]
        public int Paginas { get; set; }

        public int AutorEntityId { get; set; }
        public AutorEntity Autor { get; set; }

        public List<SelectListItem> Autores { get; }

        public LivroViewModel() { }

        public LivroViewModel(LivroEntity livroModel)
        {
            Nome = livroModel.Nome;
            Isbn = livroModel.Isbn;
            Lancamento = livroModel.Lancamento;
            Paginas = livroModel.Paginas;
            AutorEntityId = livroModel.AutorEntityId;
            Autor = livroModel.Autor;
        }

        public LivroViewModel(LivroEntity livroModel, IEnumerable<AutorEntity> autores) : this(livroModel)
        {
            Autores = ToAutorSelectListItem(autores);
        }

        public LivroViewModel(IEnumerable<AutorEntity> autores)
        {
            Autores = ToAutorSelectListItem(autores);
        }

        private static List<SelectListItem> ToAutorSelectListItem(IEnumerable<AutorEntity> autores)
        {
            return autores.Select(x => new SelectListItem
                { Text = $"{x.Nome} {x.UltimoNome}", Value = x.Id.ToString() }).ToList();
        }
    }
}
