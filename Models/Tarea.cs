using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;
public class Tarea
{
    [JsonProperty]
    public int Id {get; private set;}
    [JsonProperty]
    public string Titulo {get; private set;}
    [JsonProperty]
    public string Descripcion {get; private set;}
    [JsonProperty]
    public DateTime Fecha {get; private set;}
    [JsonProperty]
    public bool Finalizada {get; private set;}
    [JsonProperty]
    public bool Eliminada {get; private set;}
    [JsonProperty]
    public int IdUsuario {get; private set;}
    public Tarea()
    {
    }
    public Tarea(string titulo, string descripcion, DateTime fecha, int idUsuario)
    {
        this.Titulo = titulo;
        this.Descripcion = descripcion;
        this.Fecha = fecha;
        this.Finalizada = false;
        this.Eliminada = false;
        this.IdUsuario = idUsuario;
    }
}