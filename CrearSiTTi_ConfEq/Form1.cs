using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrearSiTTi_ConfEq
{
    public partial class Form1 : Form
    {
        public static String servivor = "148.223.153.43\\MSSQLSERVER1";
        public static String basededatos = "bd_SiTTi";
        public static String usuariobd = "sa";
        public static String passbd = "At3n4";
        public static string nsql = "server=" + servivor + "; database=" + basededatos + " ;User ID=" + usuariobd + ";Password=" + passbd + "; integrated security = false ; MultipleActiveResultSets=True";

        public static List<String[]> lista = new List<String[]>();
        public static int id_empleado;

        public Form1()
        {
            InitializeComponent();
        }


        public TimeSpan salida_trabajo(TimeSpan hora)
        {
            return hora;
        }


        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(nsql))
                {
                    conexion.Open();
                    SqlCommand comm = new SqlCommand("SELECT u.id_empleado, e.nombre,e.ap_paterno,e.ap_materno  FROM [bd_SiTTi].[dbo].[cg_usuario] u INNER JOIN [bd_SiTTi].[dbo].[cg_empleado] e ON e.id_empleado=u.id_empleado WHERE e.status='A' ORDER BY u.id_empleado DESC", conexion);
                    SqlDataReader nwReader = comm.ExecuteReader();
                    while (nwReader.Read())
                    {
                        String[] n = new String[5];
                        n[0] = nwReader["id_empleado"].ToString();
                        n[1] = nwReader["nombre"].ToString();
                        n[2] = nwReader["ap_paterno"].ToString();
                        n[3] = nwReader["ap_materno"].ToString();
                        n[4] = n[1] + " " + n[2] + " " + n[3];
                        lista.Add(n);
                    }
                }
            }
            catch (System.Exception ex)
            {
                e.Cancel = true;
                MessageBox.Show("Error en la busqueda\n\nMensaje: " + ex.Message, "Información del Equipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                Application.Exit();
            }
            if (lista.Any())
            {
                foreach (String[] n in lista)
                {
                    dataGridView1.Rows.Add(n[0], n[1]+" "+n[2]+" "+n[3]);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id_empleado = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString());

            CrearSiTTi crear = new CrearSiTTi();
            crear.ShowDialog();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                foreach (String[] empleado in lista)
                {
                    String nmayus = empleado[4].ToString().ToUpper();
                    if (nmayus.Contains(textBox1.Text.ToUpper()))
                    {
                        dataGridView1.Rows.Add(empleado[0], empleado[4]);
                    }
                }
            }
            else
            {
                foreach (String[] empleado in lista)
                {
                    dataGridView1.Rows.Add(empleado[0], empleado[4]);
                }
            }
        }
    }
}
