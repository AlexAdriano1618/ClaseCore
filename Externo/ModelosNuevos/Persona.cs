using System;
using System.Collections.Generic;

namespace Externo.ModelosNuevos
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int? IdGenero { get; set; }

        public Genero IdGeneroNavigation { get; set; }
    }
}
