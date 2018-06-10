using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Negocio
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public int? IdGenero { get; set; }
        public Genero IdGeneroNavigation { get; set; }
    }
}
