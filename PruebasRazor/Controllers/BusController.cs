using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebasRazor.Models;
namespace PruebasRazor.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index()
        {
            List<BusCLS> listaBus = null;
            using (var bd = new BDPasajeEntities())
            {
                listaBus = (from bus in bd.Bus
                            join sucursal in bd.Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            join tipoBus in bd.TipoBus
                            on bus.IIDTIPOBUS equals tipoBus.IIDTIPOBUS
                            join tipoModelo in bd.Modelo
                            on bus.IIDMODELO equals tipoModelo.IIDMODELO
                            where bus.BHABILITADO == 1
                            select new BusCLS
                            {
                                iidBus = bus.IIDBUS,
                                nombreModelo = tipoModelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipoBus.NOMBRE,
                                placa = bus.PLACA
                            }).ToList();
            }
            return View(listaBus);
        }

        public void listarMarca()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Marca
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMARCA.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaMarca = lista;
            }
        }

        public void listarTipoBus()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoBus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaTipoBus = lista;
            }
        }

        public void listarSucursal()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Sucursal
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDSUCURSAL.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaSucursal = lista;
            }
        }

        public void listarModelo()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Modelo
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMODELO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaModelo = lista;
            }
        }

        public void listarCombos() 
        {
            listarModelo();
            listarMarca();
            listarTipoBus();
            listarSucursal();
        }

        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }

        public ActionResult Editar(int id)
        {
            BusCLS oBusCLS = new BusCLS();
            listarCombos();
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                oBusCLS.iidBus = oBus.IIDBUS;
                oBusCLS.iidSucursal = (int) oBus.IIDSUCURSAL;
                oBusCLS.iidTipoBus = (int) oBus.IIDTIPOBUS;
                oBusCLS.iidmarca = (int) oBus.IIDMARCA;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.fechaCompra = (DateTime) oBus.FECHACOMPRA;
                oBusCLS.iidmodelo = (int) oBus.IIDMODELO;
                oBusCLS.numeroColumnas = (int) oBus.NUMEROCOLUMNAS;
                oBusCLS.numeroFilas = (int) oBus.NUMEROFILAS;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.observacion = oBus.OBSERVACION;
            }
                return View(oBusCLS);
        }

        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {
            int idBus = oBusCLS.iidBus;
            if (!ModelState.IsValid)
            {
                return View(oBusCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();
                oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                oBus.IIDMODELO = oBusCLS.iidmodelo;
                oBus.IIDMARCA = oBusCLS.iidmarca;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.PLACA = oBusCLS.placa;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = new Bus();
                oBus.BHABILITADO = 1;
                oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                oBus.IIDMODELO = oBusCLS.iidmodelo;
                oBus.IIDMARCA = oBusCLS.iidmarca;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.PLACA = oBusCLS.placa;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                bd.Bus.Add(oBus);
                bd.SaveChanges();
            }
                listarCombos();
            return RedirectToAction("Index");
        }
    }
}