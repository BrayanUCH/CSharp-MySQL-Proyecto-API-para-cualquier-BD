using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal.CapaLogica.Logica
{
    public class Conexión
    {
        protected string server;
        protected string database;
        protected string uid;
        protected string pwd;
        protected string conexion;

        public Conexión()
        {
            
        }
        public Conexión(string Cserver, string Cdatabase, string Cuid, string Cpwd)
        {
            conexion = "server=" + Cserver + ";database=" + Cdatabase + ";uid=" + Cuid + ";pwd=" + Cpwd + ";";

        }
        

        public string Conexion { get => conexion; set => conexion = value; }
        public string Server { get => server; set => server = value; }
        public string Database { get => database; set => database = value; }
        public string Uid { get => uid; set => uid = value; }
        public string Pwd { get => pwd; set => pwd = value; }
    }
}
