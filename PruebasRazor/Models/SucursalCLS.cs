using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class SucursalCLS
    {
        [Display(Name = "Id Sucursal")]
        public int iidsucursal { get; set; }
        [Display(Name = "Nombre Sucursal")]
        [StringLength(100,ErrorMessage ="Longitud maxima 100")]
        public string nombre { get; set; }
        [Display(Name = "Telefono Sucursal")]
        [StringLength(10, ErrorMessage = "Longitud maxima 10")]
        public string telefono { get; set; }
        [Display(Name = "Direccion")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        [Required]
        public string direccion { get; set; }
        [Display(Name = "Email Sucursal")]
        [EmailAddress(ErrorMessage ="Email no valido")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud maxima 100")]
        public string email { get; set; }
        [Display(Name = "Fecha Apertura")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Required]
        [DataType(DataType.Date)]
        public DateTime fechaApertura { get; set; }
        public int bhabilitado { get; set; }
    }
}