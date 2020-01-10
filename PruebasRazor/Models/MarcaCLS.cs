﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class MarcaCLS
    {
        [Display(Name = "Id Marca")]
        public int iidmarca { get; set; }
        [Display(Name = "Nombre marca")]
        [Required]
        [StringLength(100,ErrorMessage ="La longitud maxima es 100")]
        public string nombre { get; set; }
        [Display(Name = "Descripcion marca")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es 200")]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }
    }
}