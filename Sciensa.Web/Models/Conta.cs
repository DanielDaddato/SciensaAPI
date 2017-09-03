using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sciensa.Web.Models
{
    public class Conta
    {
        public int ContaID { get; set; }
        public int Agencia { get; set; }
        public int NumeroConta { get; set; }
        public int TipoConta { get; set; }
        public int ClienteID { get; set; }
        public double Saldo { get; set; }
    }
}
