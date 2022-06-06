using MySql.Data.MySqlClient;
using ProyectoFinal.CapaConexion;
using ProyectoFinal.CapaLogica.Logica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoFinal.CapaLogica.Servicios
{
    public class ServicioConexion : servicio, IDisposable
    {
        private MySqlCommand miComando;
        private string respuesta;
        servicio conexion;

        public void Dispose()
        {

        }

        public ServicioConexion()
        {
            miComando = new MySqlCommand();
            respuesta = "";
        }

        public string ServicioProbarConexion(Conexión cConexionon)
        {
            this.Conset(cConexionon.Conexion);
            try
            {
                this.abrirConexion();
                this.cerrarConexion();
                return "El string de conexión correcto";
            }
            catch(Exception e)
            {
                return "El string de conexión es incorrecto";
            }
        }

        public int ServicioGenerar(Conexión cConexionon)
        {
            this.Conset(cConexionon.Conexion);
            try
            {
                this.abrirConexion();
                this.cerrarConexion();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public DataTable cargarSqlSentencia(string sentencia,Conexión cConexionon)
        {
            DataTable data = new DataTable();
            try
            {
                this.Conset(cConexionon.Conexion);

                var comando = new MySqlCommand(string.Format(sentencia), this.Conget());

                var adapter = new MySqlDataAdapter(comando);

                adapter.Fill(data);

                MessageBox.Show("Sentencia ejecutada con exito");
                return data;
            }
            catch (Exception e)
            {
                MessageBox.Show("Hay problemas con la conexión de la base de datos");
                return data;
            }
        }

        public DataTable procedimientos(string Nombre, List<string> parametros, List<string> valores, Conexión cConexionon)
        {
            DataTable data = new DataTable();
            try
            {
                this.Conset(cConexionon.Conexion);

                MySqlCommand mySqlCommand = new MySqlCommand(string.Format(Nombre), this.Conget());
                mySqlCommand.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < parametros.Count; i++)
                {
                    //cmd.Parameters.AddWithValue("@idPrueb", 7);
                    //mySqlCommand.Parameters.AddWithValue($"'@{parametros[i]}'", $"'{valores[i]}'");
                    mySqlCommand.Parameters.AddWithValue($"@{parametros[i]}", $"{valores[i]}");
                }

                var adapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.Dispose();
                adapter.Fill(data);
                MessageBox.Show("Procedimiento ejecutado con exito");
            }
            catch (Exception e)
            {
                MessageBox.Show("Hay problemas con la conexión de la base de datos");
                return data;
            }
            return data;
        }

        public DataTable funciones(string Nombre, List<string> parametros, List<string> valores, Conexión cConexionon)
        {
            DataTable data = new DataTable();
            try
            {
                this.Conset(cConexionon.Conexion);

                string funcion = "SELECT " + Nombre + "(";

                for (int i = 0; i < parametros.Count; i++)
                {
                    if (i == 0)
                    {
                        funcion += $"'{valores[i]}'";
                    }
                    else
                    {
                        funcion += $",'{valores[i]}'";
                    }
                }
                funcion += ")";

                //MySqlCommand mySqlCommand = new MySqlCommand(string.Format("SELECT fc_Fecha('2000/12/3)"), this.Conget());
                //MySqlCommand mySqlCommand = new MySqlCommand(string.Format("SELECT fc_sumarnombre('22','22')"), this.Conget());
                MySqlCommand mySqlCommand = new MySqlCommand(string.Format(funcion), this.Conget());

                var adapter = new MySqlDataAdapter(mySqlCommand);

                mySqlCommand.Dispose();
                adapter.Fill(data);
                MessageBox.Show("Funcion ejecutada con exito");
            }
            catch (Exception e)
            {
                MessageBox.Show("Hay problemas con la conexión de la base de datos");
                return data;
            }
            return data;
        }

    }
}
