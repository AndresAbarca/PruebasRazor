using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebasRazor.Models;

namespace PruebasRazor.Controllers
{
    public class RolPaginaController : Controller
    {
        // GET: RolPagina
        public ActionResult Index()
        {
            listarComboRol();
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();
            using (var bd = new BDPasajeEntities())
            {
                listaRol = (from rolpagina in bd.RolPagina
                            join rol in bd.Rol on rolpagina.IIDROL equals rol.IIDROL
                            join pagina in bd.Pagina
                            on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                            where rolpagina.BHABILITADO == 1
                            select new RolPaginaCLS
                            {
                                iidrolpagina = rolpagina.IIDROLPAGINA,
                                nombreRol = rol.NOMBRE,
                                nombreMensaje = pagina.MENSAJE
                            }).ToList();
            }
            return View(listaRol);
        }

        public ActionResult Filtrar(int iidrol)
        {
            List<RolPaginaCLS> listaRol = new List<RolPaginaCLS>();
            using (var bd = new BDPasajeEntities())
            {
                if (iidrol == 0)
                {
                    listaRol = (from rolpagina in bd.RolPagina
                                join rol in bd.Rol on rolpagina.IIDROL equals rol.IIDROL
                                join pagina in bd.Pagina
                                on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                                where rolpagina.BHABILITADO == 1
                                select new RolPaginaCLS
                                {
                                    iidrolpagina = rolpagina.IIDROLPAGINA,
                                    nombreRol = rol.NOMBRE,
                                    nombreMensaje = pagina.MENSAJE
                                }).ToList();
                }
                else 
                {
                    listaRol = (from rolpagina in bd.RolPagina
                                join rol in bd.Rol on rolpagina.IIDROL equals rol.IIDROL
                                join pagina in bd.Pagina
                                on rolpagina.IIDPAGINA equals pagina.IIDPAGINA
                                where rolpagina.BHABILITADO==1
                                && rolpagina.IIDROL == iidrol
                                select new RolPaginaCLS
                                {
                                    iidrolpagina = rolpagina.IIDROLPAGINA,
                                    nombreRol = rol.NOMBRE,
                                    nombreMensaje = pagina.MENSAJE
                                }).ToList();
                }

            }
            return PartialView("_TableRolPagina",listaRol);
        }

        public void listarComboRol()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Rol
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDROL.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaRol = lista;
            }
        }
        public void listarComboPagina()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Pagina
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.MENSAJE,
                             Value = item.IIDPAGINA.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaPagina = lista;
            }
        }
    }
}