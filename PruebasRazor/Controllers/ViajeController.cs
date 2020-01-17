using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebasRazor.Models;
namespace PruebasRazor.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = null;
            using (var bd = new BDPasajeEntities())
            {
                listaViaje = (from viaje in bd.Viaje
                              join lugarOrigen in bd.Lugar
                              on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                              join lugarDestino in bd.Lugar
                              on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join bus in bd.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              select new ViajeCLS
                              {
                                  iidViaje = viaje.IIDVIAJE,
                                  nombreBus = bus.PLACA,
                                  nombreLugarDestino = lugarOrigen.NOMBRE,
                                  nombreLugarOrigen = lugarDestino.NOMBRE
                              }).ToList();
            }
                return View(listaViaje);
        }

        public void listarLugar()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Lugar
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDLUGAR.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaLugar = lista;
            }
        }

        public void listarBus()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Bus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.PLACA,
                             Value = item.IIDBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
                ViewBag.listaBus = lista;
            }
        }

        public void listarCombos() {
            listarBus();
            listarLugar();
        }

        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }

        public ActionResult Editar(int id)
        {
            listarCombos();
            ViajeCLS oViajeCLS = new ViajeCLS();
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Where(p => p.IIDVIAJE.Equals(id)).First();
                oViajeCLS.iidViaje = oViaje.IIDVIAJE;
                oViajeCLS.precio = (decimal) oViaje.PRECIO;
                oViajeCLS.iidLugarDestino = (int) oViaje.IIDLUGARDESTINO;
                oViajeCLS.iidLugarOrigen = (int) oViaje.IIDLUGARORIGEN;
                oViajeCLS.fechaViaje = (DateTime) oViaje.FECHAVIAJE;
                oViajeCLS.iidBus = (int) oViaje.IIDBUS;
                oViajeCLS.numeroAsientosDisponibles = (int) oViaje.NUMEROASIENTOSDISPONIBLES;
            }
            return View(oViajeCLS);
        }

        [HttpPost]
        public ActionResult Editar(ViajeCLS oViajeCLS)
        {
            int idviaje = oViajeCLS.iidViaje;
            if (!ModelState.IsValid)
            {
                return View(oViajeCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Where(p => p.IIDVIAJE.Equals(idviaje)).First();
                oViaje.IIDVIAJE = oViajeCLS.iidViaje;
                oViaje.PRECIO = oViajeCLS.precio;
                oViaje.IIDLUGARDESTINO = oViajeCLS.iidLugarDestino;
                oViaje.IIDLUGARORIGEN = oViajeCLS.iidLugarOrigen;
                oViaje.FECHAVIAJE = oViajeCLS.fechaViaje;
                oViaje.IIDBUS = oViajeCLS.iidBus;
                oViaje.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroAsientosDisponibles;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Agregar(ViajeCLS oViajeCLS)
        {
            //puede haber varios viajes al dia con estas caracteristicas
            if (!ModelState.IsValid)
            {
                listarCombos();
                return View(oViajeCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = new Viaje();
                oViaje.IIDLUGARORIGEN = oViajeCLS.iidLugarOrigen;
                oViaje.IIDLUGARDESTINO = oViajeCLS.iidLugarDestino;
                oViaje.PRECIO = oViajeCLS.precio;
                oViaje.FECHAVIAJE = oViajeCLS.fechaViaje;
                oViaje.IIDBUS = oViajeCLS.iidBus;
                oViaje.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroAsientosDisponibles;
                oViaje.BHABILITADO = 1;
                bd.Viaje.Add(oViaje);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int txtIdViaje)
        {
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Where(p => p.IIDVIAJE.Equals(txtIdViaje)).First();
                oViaje.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}