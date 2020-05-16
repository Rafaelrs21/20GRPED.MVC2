using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20GRPED.MVC2.Domain.Model.Options
{
    public class BibliotecaHttpOptions
    {
        public Uri ApiBaseUrl { get; set; }
        public string LivroPath { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
    }
}
