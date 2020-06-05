#nullable enable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using _20GRPED.MVC2.Domain.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _20GRPED.MVC2.Mvc.ViewModels
{
    public class LivroAutorAggregateViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome do Livro", Prompt = "Entre com o nome do livro", Description = "Nome do Livro neste campo")]
        public string NomeLivro { get; set; }

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

        public int? AutorEntityId { get; set; }
        public AutorEntity? Autor { get; set; }

        public List<SelectListItem>? Autores { get; }

        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Nome do Autor", Prompt = "Entre com o nome do autor", Description = "Nome do Autor neste campo")]
        public string? NomeAutor { get; set; }

        [StringLength(20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        [Display(Name = "Último nome do Autor", Prompt = "Entre com o último nome do autor", Description = "Último nome do Autor neste campo")]
        public string? UltimoNomeAutor { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Nascimento { get; set; }

        public LivroAutorAggregateViewModel() { }

        public LivroAutorAggregateViewModel(IEnumerable<AutorEntity> autores)
        {
            Autores = ToAutorSelectListItem(autores);
        }

        private static List<SelectListItem> ToAutorSelectListItem(IEnumerable<AutorEntity> autores)
        {
            return autores.Select(x => new SelectListItem
                { Text = $"{x.Nome} {x.UltimoNome}", Value = x.Id.ToString() }).ToList();
        }

        public LivroAutorAggregateEntity ToAggregateEntity()
        {
            var aggregateEntity = new LivroAutorAggregateEntity
            {
                LivroEntity = new LivroEntity
                {
                    Nome = NomeLivro,
                    Isbn = Isbn,
                    Lancamento = Lancamento,
                    Paginas = Paginas,
                    AutorEntityId = AutorEntityId ?? 0
                }
            };

            if (!Nascimento.HasValue ||
                string.IsNullOrWhiteSpace(NomeAutor) ||
                string.IsNullOrWhiteSpace(UltimoNomeAutor))
                return aggregateEntity;

            aggregateEntity.AutorEntity = new AutorEntity
            {
                Nome = NomeAutor, UltimoNome = UltimoNomeAutor, Nascimento = Nascimento.Value, Id = AutorEntityId ?? 0
            };

            return aggregateEntity;
        }
    }
}
