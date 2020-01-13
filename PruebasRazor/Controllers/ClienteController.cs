using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebasRazor.Models;

namespace PruebasRazor.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<ClienteCLS> listaCliente = null;
            using (var bd = new BDPasajeEntities())
            {
                listaCliente = (from cliente in bd.Cliente
                                where cliente.BHABILITADO == 1
                                select new ClienteCLS
                                {
                                    iidcliente = cliente.IIDCLIENTE,
                                    nombre = cliente.NOMBRE,
                                    apPaterno = cliente.APPATERNO,
                                    apMaterno = cliente.APMATERNO,
                                    telefonoFijo = cliente.TELEFONOFIJO
                                }).ToList();
            }
            return View(listaCliente);
        }

        List<SelectListItem> listaSexo;
        private void llenarSexo()
        {
            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione--", Value = "" });
            }
        }
        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;

            return View();
        }

        [HttpPost]
        public ActionResult Eliminar(int iidcliente)
        {
            using (var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(iidcliente)).First();
                oCliente.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ClienteCLS oClienteCLS = new ClienteCLS();
            using (var bd = new BDPasajeEntities())
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();
                oClienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.apPaterno = oCliente.APPATERNO;
                oClienteCLS.apMaterno = oCliente.APMATERNO;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.email = oCliente.EMAIL;
                oClienteCLS.iidsexo = (int)oCliente.IIDSEXO;
                oClienteCLS.telefonoFijo = oCliente.TELEFONOFIJO;
                oClienteCLS.telefonoCelular = oCliente.TELEFONOCELULAR;
            }
            return View(oClienteCLS);
        }

        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS)
        {
            int idcliente = oClienteCLS.iidcliente;
            string nombre = oClienteCLS.nombre;
            string apPaterno = oClienteCLS.apPaterno;
            string apMaterno = oClienteCLS.apMaterno;
            int nregistrosEncontrados = 0;
            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno) && p.IIDCLIENTE.Equals(idcliente)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados >= 1)
            {
                if (nregistrosEncontrados >= 1) oClienteCLS.mensajeError = "ya existe el cliente";
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oClienteCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idcliente)).First();
                oCliente.NOMBRE = oClienteCLS.nombre;
                oCliente.APPATERNO = oClienteCLS.apPaterno;
                oCliente.APMATERNO = oClienteCLS.apMaterno;
                oCliente.DIRECCION = oClienteCLS.direccion;
                oCliente.EMAIL = oClienteCLS.email;
                oCliente.IIDSEXO = oClienteCLS.iidsexo;
                oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;
                oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {
            int nregistrosEncontrados = 0;
            string nombre = oClienteCLS.nombre;
            string apPaterno = oClienteCLS.apPaterno;
            string apMaterno = oClienteCLS.apMaterno;
            using (var bd = new BDPasajeEntities())
            {
                nregistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();
            }
            if (!ModelState.IsValid || nregistrosEncontrados >= 1)
            {
                if (nregistrosEncontrados >= 1) oClienteCLS.mensajeError = "ya existe";
                llenarSexo();
                ViewBag.lista = listaSexo;
                return View(oClienteCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.apPaterno;
                    oCliente.APMATERNO = oClienteCLS.apMaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;
                    oCliente.IIDSEXO = oClienteCLS.iidsexo;
                    oCliente.BHABILITADO = 1;
                    bd.Cliente.Add(oCliente);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }
    }
}