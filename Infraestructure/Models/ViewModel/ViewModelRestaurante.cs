using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models.ViewModel
{
    public class ViewModelRestaurante
    {
        //Nombre del tipo de restauratane 
        public string NombreTipoRestaurante { get; set; }

        //Restaurante
        public string ID_Restaurante { get; set; }
        public string Nombre { get; set; }
        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string DireccionExacta { get; set; }
        //public byte[] Fotografia { get; set; }
        public string EstadoActual { get; set; }
    }
}
