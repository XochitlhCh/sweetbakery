using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sweetbakery.Models;
using sweetbakery.Models.ViewModels;

namespace sweetbakery.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        SweetbakeryContext context = new SweetbakeryContext();

        [Route("productos")]
        public IActionResult Productos()
        {
            var prod = context.Categorias.OrderBy(x => x.Nombre)
                .Include(x=>x.Productos);
                
            if (prod == null)
            {
                return RedirectToAction("Index");
            }
            return View(prod);
        }


        [Route("recetas")]
        public IActionResult Recetas()
        {
            var rec = context.Categorias.OrderBy(x=>x.Nombre)
                .Include (x=>x.Recetas).Where(x=>x.Recetas.Count>0);

            if (rec == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(rec);
        }

        [Route("receta/{nombre}")]//recibe el nombre de la receta de la cual se va mostrar la informacion
        public IActionResult Receta(string nombre)
        {
            nombre = nombre.Replace("-"," ");/*los guiones medios se reemplazan con espacio*/
            RecetaViewModel vm = new RecetaViewModel();

            var receta = context.Recetas.FirstOrDefault(x => x.Nombre==nombre);
            if (receta == null)
            {
                RedirectToAction("recetas");
            }

            vm.Receta = receta;/*receta del viewmodel es igual a la receta que acabo de encontrar en la consulta*/

            /*llenar la lista de recetas del viewmodel*/
            Random r = new Random();/*para que seleccione las recetasd de manera aleatoria*/
            var masRecetas = context.Recetas
                .Where(x => x.Id != receta.Id)
                .ToList()/*lo convierte a lista para poder usarlo*/
                .OrderBy(x => r.Next())
                .Take(3);/*coleccion de recetas MENOS la que se encontro en receta */

            if (masRecetas==null)
            {
                RedirectToAction("recetas");
            }
            vm.ListaRecetas = masRecetas.ToList(); //llenar la lista de recetas del viewmodel
            return View(vm);
        }
    }
}