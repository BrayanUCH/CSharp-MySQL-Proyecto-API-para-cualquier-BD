using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.CapaConexion
{
    public class servicio
    {

        private MySqlConnection con;
        private string S;

        public servicio()
        {

        }

        //Server=myServerAddress;Database=myDataBase;
        //Uid=myUsername;Pwd=myPassword;
        public servicio(string StringdeConexion)
        {
            con = new MySqlConnection(StringdeConexion);
        }
        public void Conset(string StringdeConexion)
        {
            con = new MySqlConnection(StringdeConexion);
            S = StringdeConexion;
        }
        public MySqlConnection Conget() { 
            return con;
        }
        public string CongetS()
        {
            return S;
        }

        public void abrirConexion()
        {
            con.Open();
        }

        public void cerrarConexion()
        {
            con.Close();
        }
    }
}
