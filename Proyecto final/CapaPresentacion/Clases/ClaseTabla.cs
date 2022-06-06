using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaPresentacion.Clases
{
    public class ClaseTabla
    {
        
        private string Parametro;
        private string Valor;

        public ClaseTabla()
        {

        }

        public ClaseTabla(string parametro, string valor)
        {
            this.Parametro = parametro;
            this.Valor = valor;
        }

        
        public string PARAMETRO { get => Parametro; set => Parametro = value; }
        public string VALOR { get => Valor; set => Valor = value; }
    }
}
