﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class RolPaginaCLS
    {
        [Display(Name = "Id Rol Página")]
        public int iidrolpagina { get; set; }
        public int iidrol { get; set; }
        public int iidpagina { get; set; }
        public int bhabilitado { get; set; }
        //propiedades adicionales
        [Display(Name="Nombre Rol")]
        public string nombreRol { get; set; }
        [Display(Name = "Nombre Mensaje")]
        public string nombreMensaje { get; set; }

    }
}