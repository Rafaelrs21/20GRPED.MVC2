using System;

namespace _20GRPED.MVC2.A02.Domain.Model.Entities 
{ 
    public class LivroEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Isbn { get; set; }
        public DateTime Lancamento { get; set; }
        public string NewPropertyTest { get; set; }
    }
}
