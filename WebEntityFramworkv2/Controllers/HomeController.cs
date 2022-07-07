using DatLibreria;
using DatLibreria.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebEntityFramworkv2.Controllers
{
    public class HomeController : Controller
    {
        DaoLibreria db = new DaoLibreria();
        DaoCategorias dbc = new DaoCategorias();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Obtener());
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            try
            {
                List<Categorias> ls = dbc.ObtenerC();
                ViewBag.Categoria = new SelectList(ls, "IdC", "NombreC");
            }
            catch (Exception ex)
            {
                TempData["resu"] = ex.Message;
            } 
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Libros l)
        {
            try
            {
                // TODO: Add insert logic here
                List<Categorias> ls = dbc.ObtenerC();
                ViewBag.Categoria = new SelectList(ls, "IdC", "NombreC");

                db.ValidarRepetido(l);
                db.Agregar(l);
                TempData["resu"] = "Se agregó un nuevo registro.";
                return RedirectToAction("Create");
            }
            catch (Exception ex)//se agregó
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                db.ObtenerList(id);
                Libros l = db.ObtenerId(id);
                List<Categorias> ls = dbc.ObtenerC();
                ViewBag.Categoria = new SelectList(ls, "IdC", "NombreC", l.Categoria);
                return View("Edit", db.ObtenerId(l.Id));
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index",db.Obtener());
            }

        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Libros l)
        {
            try
            {
                db.Actulizar(l);
                TempData["resu"] = "Registro Actualizado";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                db.Eliminar(id);
                TempData["resu"] = "El registro fue eliminado.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return View("Index", db.Obtener());
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(Libros li)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult BuscarRegistro(string valor) 
        {
            try
            {
                List<Libros> ls = db.Buscar(valor);
                return View("Index", ls);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index");

            }
        }
    }

}
