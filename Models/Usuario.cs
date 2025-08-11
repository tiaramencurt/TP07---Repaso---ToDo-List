using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;

namespace TP07.Models;
public class Usuario
{
    [JsonProperty]
    public int Id {get; private set;}
    [JsonProperty]
    public string Username {get; private set;}
    [JsonProperty]
    public string Password {get; private set;}
    [JsonProperty]
    public string Nombre {get; private set;}
    [JsonProperty]
    public string Apellido {get; private set;}
    [JsonProperty]
    public string Foto {get; private set;}
    [JsonProperty]
    public DateTime? UltimoLogin { get; private set; }
    public Usuario()
    {
    }
    public Usuario(string username, string password, string nombre, string apellido, string foto)
    {
        this.Username = username;
        this.Password = password;
        this.Nombre = nombre;
        this.Apellido = apellido;
        this.Foto = foto;
        this.UltimoLogin = null;
    }
}