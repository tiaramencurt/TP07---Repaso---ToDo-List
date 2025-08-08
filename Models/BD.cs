using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using BCrypt.Net;
namespace TP07.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=BD; Integrated Security=True; TrustServerCertificate=True;";
    public static Usuario TraerUsuario(string username)
    {
        string query = "SELECT * FROM Usuarios WHERE Username = @Username";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username });
            return usuario;
        }
    }
    public static Usuario Login(string username, string password)
    {
        Usuario usuario = TraerUsuario(username);
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password";
            Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username = username, Password = password });
            return usuario;
        }
    }
    public static bool Registrarse(Usuario usuario)
    {
        if(TraerUsuario(usuario.Username) == null)
        {
            string query = "INSERT INTO Usuarios (Username, Password, Nombre, Apellido, Foto) VALUES (@PUsername, @PPassword, @PNombre, @PApellido, @PFoto)";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { PUsername = usuario.Username, PPassword = usuario.Password, PNombre = usuario.Nombre, PApellido = usuario.Apellido, PFoto = usuario.Foto  });
            }
            return true;
        }else
        {
            return false;
        }
    }
    public static List<Tareas> TraerTareas(int IdUsuario)
    {
        string query = "SELECT * FROM Tareas WHERE IdUsuario = @pIdUsuario AND Eliminada = 0";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            List<Tarea> tareas = connection.Query<Tarea>(query, new { pIdUsuario = IdUsuario }).ToList();
            return tareas;
        }
    }
    public static void CrearTarea(Tarea tarea)
    {
        string query = "INSERT INTO Tareas (Titulo, Descripcion, Fecha, Finalizada, Eliminada, IdUsuario) VALUES (@PTitulo, @PDescripcion, @PFecha, @PFinalizada, @PEliminada, @PIdUsuario)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { PTitulo = tarea.Titulo, PDescripcion = tarea.Descripcion, PFecha = tarea.Fecha, PFinalizada = tarea.Finalizada, PEliminada = tarea.Eliminada, PIdUsuario = tarea.IdUsuario  });
        }
    }

    public static void EliminarTarea(int IdTarea)
    {
        string query = "DELETE FROM Tareas WHERE Id = @IdTarea";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { IdTarea = IdTarea });
        }
    }

    public static void ActualizarTarea(Tarea tarea)
    {
        string query = @"UPDATE Tareas SET Titulo = @PTitulo, Descripcion = @PDescripcion, Fecha = @PFecha, Finalizada = @PFinalizada, Eliminada = @PEliminada, IdUsuario = @PIdUsuario WHERE Id = @PId";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new {     PTitulo = tarea.Titulo, PDescripcion = tarea.Descripcion, PFecha = tarea.Fecha, PFinalizada = tarea.Finalizada, PEliminada = tarea.Eliminada, PIdUsuario = tarea.IdUsuario, PId = tarea.Id });
        }
    }
}