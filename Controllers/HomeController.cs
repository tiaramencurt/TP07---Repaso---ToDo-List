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
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = (int)HttpContext.Session.GetInt32("IdUsuario");
        List<Tarea> tareas = BD.TraerTareas(idUsuario);
        return View("VerTareas", tareas);
    }
    public IActionResult CrearTarea()
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        return View("CrearTarea");
    }
    [HttpPost]
    public IActionResult CrearTarea(string Titulo, string Descripcion, DateTime Fecha)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = (int)HttpContext.Session.GetInt32("IdUsuario");
        Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
        BD.CrearTarea(tarea);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult FinalizarTarea(int idTarea)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        BD.FinalizarTarea(idTarea);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult EliminarTarea(int idTarea)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        BD.EliminarTarea(idTarea);
        return RedirectToAction("MostrarTareas");
    }
    public IActionResult EditarTarea(int idTarea)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        Tarea tarea = ; //Hay que crear metodo TraerTarea en BD
        if (tarea == null)
        {
            return RedirectToAction("MostrarTareas");
        }
        return View("ModificarTarea", tarea);
    }
    [HttpPost]
    public IActionResult EditarTarea(int Id, string Titulo, string Descripcion, DateTime Fecha)
    {
        if (HttpContext.Session.GetInt32("IdUsuario") == null)
        {
            return RedirectToAction("Login", "Account");
        }
        int idUsuario = (int)HttpContext.Session.GetInt32("IdUsuario");
        Tarea tarea = new Tarea(Titulo, Descripcion, Fecha, idUsuario);
        BD.ActualizarTarea(tarea, Id);
        return RedirectToAction("MostrarTareas");
    }
}
