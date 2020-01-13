using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class BusCLS
    {
        [Display(Name="Id Bus")]
        [Required]
        public int iidBus { get; set; }
        [Display(Name = "Nombre Sucursal")]
        [Required]
        public int iidSucursal { get; set; }
        [Display(Name = "Tipo Bus")]
        [Required]
        public int iidTipoBus { get; set; }
        [Display(Name = "Placa")]
        [StringLength(100, ErrorMessage = "Longitud maxima 100")]
        [Required]
        public string placa { get; set; }
        [Display(Name = "Fecha Compra")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime fechaCompra { get; set; }
        [Display(Name = "Id Modelo")]
        [Required]
        public int iidmodelo { get; set; }
        [Display(Name = "Numero Filas")]
        [Required]
        public int numeroFilas { get; set; }
        [Display(Name = "Numero Columnas")]
        [Required]
        public int numeroColumnas { get; set; }
        public int bhabilitado { get; set;}
        [Display(Name = "Descripcion")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        [Required]
        public string descripcion { get; set; }
        [Display(Name = "Observacion")]
        [StringLength(200,ErrorMessage ="Longitud maxima 200")]
        [Required]
        public string observacion { get; set; }
        [Display(Name = "Nombre Marca")]
        [Required]
        public int iidmarca { get; set; }

        //Propiedades adicionales
        [Display(Name = "Nombre Sucursal")]
        public string nombreSucursal { get; set; }
        [Display(Name = "Nombre Tipo Bus")]
        public string nombreTipoBus { get; set; }
        [Display(Name = "Nombre Modelo")]
        public string nombreModelo { get; set; }

        public string mensajeError { get; set; }
    }
}