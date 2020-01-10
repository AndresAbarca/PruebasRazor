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

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ViajeCLS oViajeCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oViajeCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = new Viaje();
                oViaje.placa = oViajeCLS.nombre;
                oViaje.NOMBRE = oViajeCLS.nombre;
                oViaje.NOMBRE = oViajeCLS.nombre;
                oViaje.NOMBRE = oViajeCLS.nombre;
                oViaje.NOMBRE = oViajeCLS.nombre;
                oViaje.NOMBRE = oViajeCLS.nombre;
                oViaje.BHABILITADO = 1;
                bd.Sucursal.Add(oSucursal);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursalCLS.iidsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura = (DateTime)oSucursal.FECHAAPERTURA;
            }
            return View(oSucursalCLS);
        }

        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int idSucursal = oSucursalCLS.iidsucursal;
            if (!ModelState.IsValid)
            {
                return View(oSucursalCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();
                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}