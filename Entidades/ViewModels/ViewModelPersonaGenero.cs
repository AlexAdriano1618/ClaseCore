using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entidades.ViewModels
{
    public class ViewModelPersonaGenero
    {
        public int IdPersona { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        [Display(Name = "Género")]
        public string DescripcionGenero { get; set; }
    }
}
