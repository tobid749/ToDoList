using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string contraseña)
        {
            Usuario usuario = BD.Login(username, contraseña);

            if (usuario != null)
            {
                HttpContext.Session.SetInt32("Id", usuario.Id);
                BD.ActualizarLogin(usuario.Id);
                return RedirectToAction("VerTareas", "Home");
            }

            ViewBag.Error = "La contraseña es incorrecta";
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario nuevoUsuario)
        {
            bool registrado = BD.Registrarse(nuevoUsuario);

            if (registrado)
            {
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Ya existe el username.";
            return View();
        }

        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
