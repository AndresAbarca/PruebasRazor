﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebasRazor.Models
{
    public class ClienteCLS
    {
        [Display(Name ="Id Cliente")]
        public int iidcliente { get; set; }
        [Display(Name = "Nombre Cliente")]
        [StringLength(100,ErrorMessage ="Longitud maxima 100")]
        [Required]
        public string nombre { get; set; }
        [Display(Name = "Apellido Paterno")]
        [StringLength(150, ErrorMessage = "Longitud maxima 150")]
        [Required]
        public string apPaterno { get; set; }
        [Display(Name = "Apellido Materno")]
        [StringLength(150, ErrorMessage = "Longitud maxima 150")]
        [Required]
        public string apMaterno { get; set; }
        [Required]
        [Display(Name = "Email")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Direccion")]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        public string direccion { get; set; }
        [Required]
        [Display(Name = "Sexo")]
        public int iidsexo { get; set; }
        [Required]
        [Display(Name = "Telefono Fijo")]
        [StringLength(10, ErrorMessage = "Longitud maxima 10")]
        public string telefonoFijo { get; set; }
        [Required]
        [Display(Name = "Telefono Celular")]
        [StringLength(10, ErrorMessage = "Longitud maxima 10")]
        public string telefonoCelular { get; set; }
        public int bhabilitado { get; set; }
    }
}