using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public AccountController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Login()
    {
        ViewBag.mailExiste = true;
        ViewBag.contraseñaCoincide = true;
        return View("Login");
    }
    [HttpPost]
    public IActionResult Login(string Usuario, string Contraseña)
    {
        Usuario usuario = BD.Login(Usuario, Contraseña);
        if (usuario == null)
        {
            ViewBag.mailExiste = false;
            ViewBag.contraseñaCoincide = true;
            return View("Login");
        }
        HttpContext.Session.SetString("IdUsuario", usuario.Id.ToString());
        BD.ActualizarFechaLogin(usuario.Id);
        return RedirectToAction("MostrarTareas", "Home", new { Eliminadas = false });
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Remove("IdUsuario");
        return RedirectToAction("Login");
    }
    public IActionResult Registrarse()
    {
        ViewBag.contraseñaCoincide = true;
        ViewBag.mailExiste = false;
        return View("Registro");
    }
    [HttpPost]
    public IActionResult Registrarse(string Usuario, string Contraseña1, string Contraseña2, string Nombre, string Apellido, IFormFile Foto)
    {
        if (Contraseña1 != Contraseña2)
        {
            ViewBag.mailExiste = false;
            ViewBag.contraseñaCoincide = false;
            return View("Registro");
        }

        string fileName = null;
        if (Foto != null && Foto.Length > 0)
        {
            fileName = Path.GetFileName(Foto.FileName);
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            string filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Foto.CopyTo(stream);
            }
        }
        else
        {
            fileName = "default.png";
        }

        Usuario nuevoUsuario = new Usuario(Usuario, Contraseña1, Nombre, Apellido, fileName);
        bool registro = BD.Registrarse(nuevoUsuario);
        if (!registro)
        {
            ViewBag.contraseñaCoincide = true;
            ViewBag.mailExiste = true;
            return View("Registro");
        }
        return RedirectToAction("Login"); 
    }
    /*public IActionResult Registrarse(string Usuario, string Contraseña1, string Contraseña2, string Nombre, string Apellido, string Foto)
    {
        if (Contraseña1 != Contraseña2)
        {
            ViewBag.mailExiste = false;
            ViewBag.contraseñaCoincide = false;
            return View("Registro");
        }
        Usuario nuevoUsuario = new Usuario(Usuario, Contraseña1, Nombre, Apellido, Foto);
        bool registro = BD.Registrarse(nuevoUsuario);
        if (!registro)
        {
            ViewBag.contraseñaCoincide = true;
            ViewBag.mailExiste = true;
            return View("Registro");
        }
        return RedirectToAction("Login"); 
    }*/
}