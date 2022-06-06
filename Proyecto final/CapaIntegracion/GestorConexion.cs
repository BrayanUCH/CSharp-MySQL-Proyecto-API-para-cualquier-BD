using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinal.CapaLogica.Logica;
using ProyectoFinal.CapaLogica.Servicios;
using ProyectoFinal.CapaConexion;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace CapaIntegracion
{
    public class GestorConexion : servicio, IDisposable
    {
        ServicioConexion SC = new ServicioConexion();
        public GestorConexion()
        {

        }

        public void Dispose()
        {

        }

        public string Probar(string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            Conexión cConexionon = new Conexión(Cserver, Cdatabase, Cuid, Cpwd);
            return SC.ServicioProbarConexion(cConexionon);
        }
        public int Generar(string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            Conexión cConexionon = new Conexión(Cserver,  Cdatabase,  Cuid,  Cpwd);
            return SC.ServicioGenerar(cConexionon);
        }

        public DataTable SqlSentencia(string snetencia, string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            Conexión cConexionon = new Conexión(Cserver, Cdatabase, Cuid, Cpwd);
            return SC.cargarSqlSentencia(snetencia, cConexionon);

        }
        public DataTable procedimientos(string Nombre, List<string> parametros, List<string> valores, string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            Conexión cConexionon = new Conexión(Cserver, Cdatabase, Cuid, Cpwd);
            return SC.procedimientos(Nombre, parametros, valores, cConexionon);
        }

        public DataTable funciones(string Nombre, List<string> parametros, List<string> valores, string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            Conexión cConexionon = new Conexión(Cserver, Cdatabase, Cuid, Cpwd);
            return SC.funciones(Nombre, parametros, valores, cConexionon);
        }


    }
}
