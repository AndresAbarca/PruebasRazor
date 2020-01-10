using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class ViajeCLS
    {
        [Display(Name="Id Viaje")]
        public int iidViaje { get; set; }
        [Display(Name = "Lugar Origen")]
        [Required]
        public int iidLugarOrigen { get; set; }
        [Display(Name = "Lugar Destino")]
        [Required]
        public int iidLugarDestino { get; set; }
        [Display(Name = "Precio")]
        [Required]
        public double precio { get; set; }
        [Display(Name = "Fecha Viaje")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] 
        [Required]
        public DateTime fechaViaje { get; set; }
        [Display(Name = "Bus")]
        [Required]
        public string iidBus { get; set; }
        [Display(Name = "Numero Asientos Disponibles")]
        [Required]
        public int numeroAsientosDisponibles { get; set; }

        //Propiedades adicionales
        [Display(Name = "Lugar Origen")]
        public string nombreLugarOrigen { get; set; }
        [Display(Name = "Lugar Destino")]
        public string nombreLugarDestino { get; set; }
        [Display(Name = "Nombre Bus")]
        public string nombreBus { get; set; }
    }
}