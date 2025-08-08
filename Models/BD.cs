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
    public static Integrante TraerTareas(string mail)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM Integrante WHERE mail = @mail";
            Integrante integrante = connection.QueryFirstOrDefault<Integrante>(query, new { mail });
            return integrante;
        }
    }
    public static void GuardarIntegrante(Integrante integrante)
    {
        string query = "INSERT INTO Integrante (mail, contraseña, nombre, nombreEquipo, apellido, genero, fechaNacimiento, datoCurioso, foto) VALUES (@pmail, @pcontraseña, @pnombre, @pnombreEquipo, @papellido, @pgenero, @pfechaNacimiento, @pdatoCurioso, @pfoto)";
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Execute(query, new { pmail = integrante.mail, pcontraseña = integrante.contraseña, pnombre = integrante.nombre, pnombreEquipo = integrante.nombreEquipo, papellido = integrante.apellido, pgenero = integrante.genero, pfechaNacimiento = integrante.fechaNacimiento, pdatoCurioso = integrante.datoCurioso, pfoto = integrante.foto });
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