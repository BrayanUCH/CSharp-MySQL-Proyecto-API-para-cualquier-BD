using CapaIntegracion;
using CapaPresentacion.Clases;
using MySql.Data.MySqlClient;
using ProyectoFinal.CapaLogica.Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class MYSQL : Form
    {
        protected string server;
        protected string database;
        protected string uid;
        protected string pwd;
        protected int permiso;
        GestorConexion ges = new GestorConexion();
        List<ClaseTabla> listado = new List<ClaseTabla>();
        ClaseTabla persona;
        int numFila;

        public MYSQL()
        {
            InitializeComponent();
        }

        private void CbtnProbar_Click(object sender, EventArgs e)
        {
            //Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
            string resultado = ges.Probar(CtxtbServer.Text, CtxtbDataBase.Text, CtxtbUsuario.Text, CtxtbContraseña.Text);
            MessageBox.Show(resultado);
        }

        private void CbtnGenerar_Click(object sender, EventArgs e)
        {
            
            int resultado = ges.Generar(CtxtbServer.Text, CtxtbDataBase.Text, CtxtbUsuario.Text, CtxtbContraseña.Text);

            if (resultado == 1) {
                MessageBox.Show("El string de conexión generado");
                server = CtxtbServer.Text;
                database = CtxtbDataBase.Text;
                uid = CtxtbUsuario.Text;
                pwd = CtxtbContraseña.Text;
            }
            else
            {
                MessageBox.Show("Hubo un error al generar el string de conexión");
            }
            
        }

        private void DbtnEjecutar_Click(object sender, EventArgs e)
        {
            //DtxtSql
            if (DtxtSql.Text != "")
            {
                DdgvResultado.DataSource = ges.SqlSentencia(DtxtSql.Text, server, database, uid, pwd);
            }
            else
            {
                DdgvResultado.DataSource = null;
                MessageBox.Show("Verifique los datos");
            }
        }

        private void PbtnEjecutar_Click(object sender, EventArgs e)
        {
            //string sql = "SELECT fc_sumarEdad( 1, 1 )";

            //SELECT fc_Fecha('2000/12/30')

            //SELECT fc_holaMundoboolean(1);

            //SELECT fc_sumarEdad(1, 1);

            //SELECT fc_holaMundo('2');

            //SELECT fc_Fecha('2000/12/30')

            //SELECT fc_sumarnombre('1', '1');

            List<string> parametros = new List<string>();
            List<string> valores = new List<string>();


            if (("Procedimiento" == PcbxTipo.Text || "Funcion" == PcbxTipo.Text) && PdgvDatos.DataSource != null)
            {
                foreach (ClaseTabla lis in listado)
                {
                    parametros.Add(lis.PARAMETRO);
                    valores.Add(lis.VALOR);
                }
            }
            if ("Procedimiento" == PcbxTipo.Text)
            {
                PdgvResultado.DataSource = ges.procedimientos(PtxtNombrePF.Text, parametros, valores, server, database, uid, pwd);
            }
            else if ("Funcion" == PcbxTipo.Text)
            {
                PdgvResultado.DataSource = ges.funciones(PtxtNombrePF.Text, parametros, valores, server, database, uid, pwd);
            }
            else
            {
                MessageBox.Show("REQUIERE UN PROCEDIMIENTO O FUNCION");
            }

        }

        private void PbtnAgregar_Click(object sender, EventArgs e)
        {
            if (PtxtValor.Text != "") {
                persona = new ClaseTabla();

                persona.PARAMETRO = PtxtNombreValor.Text;
                persona.VALOR = PtxtValor.Text;

                listado.Add(persona);

                PdgvDatos.DataSource = null;

                PdgvDatos.DataSource = listado;

                PpanelActualizacion.Visible = false;
            }
            else
            {
                MessageBox.Show("Verifique la información");
            }
        }

        private void PbntLimpiar_Click(object sender, EventArgs e)
        {
            PtxtNombrePF.Text = "";
            PtxtNombreValor.Text = "";
            PtxtValor.Text = "";
            PdgvDatos.DataSource = null;
            PdgvResultado.DataSource = null;
            PcbxTipo.SelectedItem = null;
            PpanelActualizacion.Visible = false;
            listado.Clear();
        }

        private void PdgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClaseTabla per = new ClaseTabla();
            try
            {
                numFila = PdgvDatos.CurrentCell.RowIndex;
                per = this.listado[numFila];

                PAtxtValor.Text = per.VALOR; 
                PAtxtParametro.Text = per.PARAMETRO;

                PpanelActualizacion.Visible = true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Error al selecionar para actualizar");
            }
        }

        private void PbtnActualizar_Click(object sender, EventArgs e)
        {
            if (PAtxtValor.TextLength != 0) {
                try
                {
                    ClaseTabla per = new ClaseTabla();

                    per.VALOR = PAtxtValor.Text;
                    per.PARAMETRO = PAtxtParametro.Text;

                    this.listado[numFila] = per;

                    PdgvDatos.DataSource = null;

                    PdgvDatos.DataSource = listado;

                    PpanelActualizacion.Visible = false;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Error al actualizar");
                }
            }
            else
            {
                MessageBox.Show("Verifique la información");
            }
        }

        private void PbtnEliminar_Click(object sender, EventArgs e)
        {
            if (numFila != null)
            {
                try
                {
                    this.listado.RemoveAt(numFila);

                    PdgvDatos.DataSource = null;

                    PdgvDatos.DataSource = listado;

                    PpanelActualizacion.Visible = false;
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            else
            {
                MessageBox.Show("Verifique la información");
            }
        }

        private void DbtnGuardar_Click(object sender, EventArgs e)
        {
            List<string> parametros = new List<string>();
            List<string> valores = new List<string>();



            if (DtxtcCedula.Text != "" || DtxtNombre.Text != "" || DtxtCurso.Text != "") {

                parametros.Add("p_cedula");
                parametros.Add("p_nombre");
                parametros.Add("p_curso");

                valores.Add(DtxtcCedula.Text);
                valores.Add(DtxtNombre.Text);
                valores.Add(DtxtCurso.Text);

                PdgvResultado.DataSource = ges.procedimientos("sp_InsertarEstudiante", parametros, valores, server, database, uid, pwd);
            }
            else
            {
                MessageBox.Show("Verifique la información");
            }



        }
    }
}
