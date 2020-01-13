using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebasRazor.Models;
using PruebasRazor.Controllers;

namespace PruebasRazor.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {
            List<SucursalCLS> listaSucursal = null;
            using (var bd = new BDPasajeEntities())
            {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO == 1
                                 select new SucursalCLS
                                 {
                                     iidsucursal = sucursal.IIDSUCURSAL,
                                     nombre = sucursal.NOMBRE,
                                     telefono = sucursal.TELEFONO,
                                     email = sucursal.EMAIL
                                 }).ToList();
            }
            return View(listaSucursal);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        public ActionResult Eliminar(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursal.BHABILITADO = 0;
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
            int nregistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            int idSucursal = oSucursalCLS.iidsucursal;
            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal) && !p.IIDSUCURSAL.Equals(idSucursal)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados >= 1)
            {
                if (nregistrosEncontrados >= 1) oSucursalCLS.mensajeError = "ya existe la sucursal";
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

        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            int nregistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados >= 1)
            {
                if (nregistrosEncontrados >= 1) oSucursalCLS.mensajeError = "ya existe la sucursal a ingresar";
                return View(oSucursalCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = new Sucursal();
                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.TELEFONO = oSucursalCLS.telefono;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                oSucursal.BHABILITADO = 1;
                bd.Sucursal.Add(oSucursal);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}