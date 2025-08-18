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
    public IActionResult MostrarTareas()
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
        List<Tarea> tareas = BD.TraerTareas(idUsuario, false);
        ViewBag.tareas = tareas;
        return View("VerTareas");
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
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
        Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
        BD.CrearTarea(tarea);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult FinalizarTarea(int idTarea)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        BD.FinalizarTarea(idTarea);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult EliminarTarea(int idTarea)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        BD.EliminarRecuperarTarea(idTarea, true);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult RecuperarTarea(int idTarea)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        BD.EliminarRecuperarTarea(idTarea, false);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult EditarTarea(int idTarea)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        Tarea tarea = BD.TraerTarea(idTarea);
        if (tarea == null)
        {
            return RedirectToAction("MostrarTareas");
        }
        ViewBag.tarea = tarea;
        return View("ModificarTarea");
    }
    [HttpPost]
    public IActionResult EditarTarea(int Id, string Titulo, string Descripcion, DateTime Fecha)
    {
        if (HttpContext.Session.GetString("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
        Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
        BD.ActualizarTarea(tarea, Id);
        return RedirectToAction("MostrarTareas");
    }
}
