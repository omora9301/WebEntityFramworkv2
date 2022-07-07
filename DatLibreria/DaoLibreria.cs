using DatLibreria.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatLibreria
{
    public class DaoLibreria
    {
        public List<Libros> Obtener() 
        {
            LibreriaEntities db = new LibreriaEntities();

            //List<Libros> ls = db.Libros.Include("Categorias").ToList();//inner join("Tabla foranea")
            List<spObtener_Result> list = db.spObtener().ToList();
            List<Libros> libro = new List<Libros>();
            foreach (spObtener_Result l in list)
            {
                Libros li = new Libros();
                Categorias Ca = new Categorias();
                li.Id = l.Id;
                li.Titulo = l.Titulo;
                li.Autor = l.Autor;
                li.Categoria = l.Categoria;
                li.Editorial = l.Editorial;
                li.Edicion = l.Edicion;
                li.ISBN = l.ISBN;

                Ca.IdC = l.IdC;
                Ca.NombreC = l.NombreC;

                li.Categorias = Ca;
                libro.Add(li);
                
            }
            db.Dispose();
            return libro;
        }
        public void Agregar(Libros l) 
        {
            LibreriaEntities db = new LibreriaEntities();
            //db.Libros.Add(l);
            //db.SaveChanges();
            db.spAgregar(l.Titulo, l.Autor, l.Categoria, l.Editorial, l.Edicion, l.ISBN);
            db.Dispose();
        }
        public Libros ObtenerList(int id)
        {
            LibreriaEntities db = new LibreriaEntities();
            Libros li = db.Libros.Include("Categorias").Where(x => x.Id == id).FirstOrDefault();
            db.Dispose();
            return li;
        }
        public Libros ObtenerId(int id) 
        {
            LibreriaEntities db = new LibreriaEntities();
            //Libros li = db.Libros.Where(x => x.Id == id).FirstOrDefault();
            Libros li = db.Libros.Find(id);
            return li;
        }
        public void Actulizar(Libros l) 
        {
            LibreriaEntities db = new LibreriaEntities();
            //Libros l = db.Libros.Where(x => x.Id == id).FirstOrDefault();
            Libros li = db.Libros.Find(l.Id);
            li.Id = l.Id;
            li.Titulo = l.Titulo;
            li.Autor = l.Autor;
            li.Categoria = l.Categoria;
            li.Editorial = l.Editorial;
            li.Edicion = l.Edicion;
            li.ISBN = l.ISBN;

            db.SaveChanges();
            db.Dispose();
        }
        public void Eliminar(int id) 
        {
            LibreriaEntities db = new LibreriaEntities();
            Libros li = db.Libros.Where(x => x.Id == id).FirstOrDefault();
            db.Libros.Remove(li);
            db.SaveChanges();
            db.Dispose();
        }
        public List<Libros> Buscar(string valor)
        {
            LibreriaEntities db = new LibreriaEntities();
            List<Libros> ls = db.Libros.Include("Categorias").Where(x => x.Titulo.Contains(valor) ||
                                                                         x.Autor.Contains(valor) ||
                                                                         x.Editorial.Contains(valor) ||
                                                                         x.Edicion.Contains(valor) ||                                                                        
                                                                         x.ISBN.Contains(valor) ||
                                                                         x.Categorias.NombreC.Contains(valor)).ToList();
            db.Dispose();
            return ls;            
        }
        public void ValidarRepetido(Libros l)
        {
            LibreriaEntities db = new LibreriaEntities();

            List<Libros> ls = db.Libros.Where(x => x.ISBN.Contains(l.ISBN)).ToList();
            db.Dispose();
            if (ls.Count > 0)
            {
                throw new ApplicationException($"El registro ISBN: { l.ISBN} ya existe");
            }

            
        }
    }
}
