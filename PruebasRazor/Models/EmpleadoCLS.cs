using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class EmpleadoCLS
    {
        [Display(Name = "Id Empleado")]
        [Required]
        public int iidEmpleado { get; set; }
        [Display(Name = "Nombre")]
        [StringLength(100,ErrorMessage ="Longitud maxima 100")]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        [Required]
        public string apPaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        [Required]
        public string apMaterno { get; set; }
        [Display(Name = "Fecha Contrato")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime fechaContrato { get; set; }
        [Display(Name = "Tipo Usuario")]
        [Required]
        public int iidtipoUsuario { get; set; }
        [Display(Name = "Tipo Contrato")]
        [Required]
        public int iidtipoContrato { get; set; }
        [Display(Name = "Sexo")]
        [Required]
        public int iidSexo { get; set; }
        [Display(Name = "Sueldo")]
        [Range(0,100000,ErrorMessage ="Fuera de rango")]
        [Required]
        public decimal sueldo { get; set; }
        public int bhabilitado { get; set; }

        //Propiedades adicionales
        [Display(Name = "Tipo Contrato")]
        public string nombreTipoContrato { get; set; }
        [Display(Name = "Tipo Usuario")]
        public string nombreTipoUsuario { get; set; }

    }
}