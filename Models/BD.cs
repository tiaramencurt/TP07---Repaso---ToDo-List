using Microsoft.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace TP07.Models;

public static class BD
{
    private static string _connectionString = @"Server=localhost; DataBase=BD; Integrated Security=True; TrustServerCertificate=True;";
    public static Usuario TraerUsuario(string username)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Usuarios WHERE Username = @Username";
            Usuario usuario = connection.QueryFirstOrDefault<Usuario>(query, new { Username });
            return usuario;
        }
    }
    public static void Registrarse(Usuario usuario)
    {
        if(TraerUsuario(usuario.Username) == null)
        {
            string query = "INSERT INTO Usuario (Username, Password, Nombre, Apellido, Foto) VALUES (@PUsername, @PPassword, @PNombre, @PApellido, @PFoto)";
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
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Tareas WHERE IdUsuario = @pIdUsuario";
            List<Tarea> tareas = connection.Query<Tarea>(query, new { pIdUsuario = IdUsuario }).ToList();
            return tareas;
        }
    }

}