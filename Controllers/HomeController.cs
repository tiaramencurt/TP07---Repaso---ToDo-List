using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP07.Models;

namespace TP07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Login", "Account");
    }
    public IActionResult MostrarTareas(bool Eliminadas)
    {
        if (Eliminadas == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
            List<Tarea> tareas = BD.TraerTareas(idUsuario, Eliminadas);
            ViewBag.Usuario = BD.TraerUsuarioPorId(idUsuario);
            ViewBag.tareas = tareas;
            ViewBag.Eliminadas = Eliminadas;
            return View("MostrarTareas");
        }
    }
    public IActionResult CrearTarea()
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        return View("CrearTarea");
    }
    [HttpPost]
    public IActionResult CrearTarea(string Titulo, string Descripcion, DateTime Fecha)
    {
        if (Titulo == null || Descripcion == null || Fecha == null)
        {
            return RedirectToAction("CrearTarea");
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
            Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
            BD.CrearTarea(tarea);
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
    }
    public IActionResult FinalizarTarea(int idTarea)
    {
        if (idTarea == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            BD.FinalizarTarea(idTarea);
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
    }
    public IActionResult EliminarRecuperarTarea(int idTarea, bool EliminarRecuperar)
    {
        if (idTarea == null || EliminarRecuperar == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            BD.EliminarRecuperarTarea(idTarea, EliminarRecuperar);
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
    }
    public IActionResult EditarTarea(int idTarea)
    {
        if (idTarea == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Tarea tarea = BD.TraerTarea(idTarea);
            if (tarea == null)
            {
                return RedirectToAction("MostrarTareas", new { Eliminadas = false });
            }
            ViewBag.tarea = tarea;
            return View("ModificarTarea");
        }
    }
    [HttpPost]
    public IActionResult EditarTarea(int Id, string Titulo, string Descripcion, DateTime Fecha)
    {
        if (Id == null || Titulo == null || Descripcion == null || Fecha == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }else
        {
            if (HttpContext.Session.GetString("IdUsuario") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
            Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
            BD.ActualizarTarea(tarea, Id);
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
    }
    public IActionResult CompartirTarea(int idTarea)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        ViewBag.tarea = BD.TraerTarea(idTarea);
        ViewBag.UsuarioExiste = true;
        return View("CompartirTarea");
    }
    [HttpPost]
    public IActionResult CompartirTarea(int idTarea, string username)
    {
        if (username == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        Usuario usuarioDestino = BD.TraerUsuario(username);
        if (usuarioDestino == null)
        {
            ViewBag.UsuarioExiste = false;
            ViewBag.tarea = BD.TraerTarea(idTarea);
            return View("CompartirTarea");
        }
        Tarea tareaOriginal = BD.TraerTarea(idTarea);
        if (tareaOriginal == null)
        {
            return RedirectToAction("MostrarTareas", new { Eliminadas = false });
        }
        Tarea tareaNueva = new Tarea(tareaOriginal.Titulo, tareaOriginal.Descripcion, tareaOriginal.FechaLimite, usuarioDestino.Id);
        BD.CrearTarea(tareaNueva);
        return RedirectToAction("MostrarTareas", new { Eliminadas = false });
    }
}
