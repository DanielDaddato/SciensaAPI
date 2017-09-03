using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sciensa.API.Models
{
    public class Conta
    {
        public int ContaID { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public int TipoConta { get; set; }
        public int ClienteID { get; set; }
        public double Saldo { get; set; }
    }
}
