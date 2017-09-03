using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sciensa.Web.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nome { get; set; }
        public int CPF { get; set; }
        public string Endereco { get; set; }
    }
}
