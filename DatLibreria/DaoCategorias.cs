using DatLibreria.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatLibreria
{
    public class DaoCategorias
    {
        public List<Categorias> ObtenerC() 
        {
            LibreriaEntities db = new LibreriaEntities();
            List<Categorias> lc = db.Categorias.ToList();
            return lc;
        }
        
    }
}
