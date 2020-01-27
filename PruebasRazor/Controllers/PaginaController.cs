using PruebasRazor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebasRazor.Controllers
{
    public class PaginaController : Controller
    {
        // GET: Pagina
        public ActionResult Index()
        {
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd = new BDPasajeEntities())
            {
                listaPagina = (from pagina in bd.Pagina
                               where pagina.BHABILITADO == 1
                               select new PaginaCLS
                               {
                                   iidpagina = pagina.IIDPAGINA,
                                   mensaje = pagina.MENSAJE,
                                   controlador = pagina.CONTROLADOR,
                                   accion = pagina.ACCION
                               }).ToList();
            }
            return View(listaPagina);
        }

        public ActionResult Filtrar(Pagina oPaginaCLS)
        {
            string mensaje = oPaginaCLS.MENSAJE;
            List<PaginaCLS> listaPagina = new List<PaginaCLS>();
            using (var bd = new BDPasajeEntities())
            {
                if (mensaje == null)
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }
                else
                {
                    listaPagina = (from pagina in bd.Pagina
                                   where pagina.BHABILITADO == 1
                                   && pagina.MENSAJE.Contains(mensaje)
                                   select new PaginaCLS
                                   {
                                       iidpagina = pagina.IIDPAGINA,
                                       mensaje = pagina.MENSAJE,
                                       controlador = pagina.CONTROLADOR,
                                       accion = pagina.ACCION
                                   }).ToList();
                }

            }
            return PartialView("_TablaPagina", listaPagina);
        }

        public int Guardar(PaginaCLS oPaginaCLS, int titulo)
        {
            int respuesta = 0;
            using (var bd = new BDPasajeEntities())
            {
                if (titulo.Equals(1))
                {
                    Pagina oPagina = new Pagina();
                    oPagina.MENSAJE = oPaginaCLS.mensaje;
                    oPagina.ACCION = oPaginaCLS.accion;
                    oPagina.CONTROLADOR = oPaginaCLS.controlador;
                    oPagina.BHABILITADO = 1;
                    bd.Pagina.Add(oPagina);
                    respuesta = bd.SaveChanges();
                }
            }
            return respuesta;
        }
    }
}