using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult VerTareas()
        {
            int idUsuario = (int)HttpContext.Session.GetInt32("Id");
            List<Tarea> tareas = BD.DevolverTareas(idUsuario);
            return View(tareas);
        }

        public IActionResult CrearTarea()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearTareaGuardar(Tarea tarea)
        {
            tarea.IdUsuarios = (int)HttpContext.Session.GetInt32("Id");
            BD.CrearTarea(tarea);
            return RedirectToAction("VerTareas");
        }

        public IActionResult EditarTarea(int id)
        {
            Tarea tarea = BD.DevolverTarea(id);
            return View(tarea);
        }

        [HttpPost]
        public IActionResult EditarTareaGuardar(Tarea tarea)
        {
            BD.ModificarTarea(tarea);
            return RedirectToAction("VerTareas");
        }

        public IActionResult EliminarTarea(int id)
        {
            BD.EliminarTarea(id);
            return RedirectToAction("VerTareas");
        }

        public IActionResult FinalizarTarea(int id)
        {
            BD.FinalizarTarea(id);
            return RedirectToAction("VerTareas");
        }
    }
}
