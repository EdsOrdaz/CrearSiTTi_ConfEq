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
    public partial class CrearSiTTi : Form
    {
        public CrearSiTTi()
        {
            InitializeComponent();
        }

        private DateTime checarhora(int horas)
        {
            DateTime ahora = DateTime.Now;
            TimeSpan iniciocomida = new TimeSpan(14, 0, 0);
            TimeSpan terminodia = new TimeSpan(18, 0, 0);
            DateTime agregado = DateTime.Now.AddHours(horas);

            DateTime ahora2 = DateTime.Now;
            for (int i = 1; i <= horas; i++)
            {
                ahora = ahora.AddHours(1);

                if (ahora2.TimeOfDay < iniciocomida && ahora.TimeOfDay > iniciocomida)
                {
                    ahora = ahora.AddHours(1);
                }

                if (ahora2.TimeOfDay < terminodia && ahora.TimeOfDay > terminodia)
                {
                    ahora = ahora.AddHours(14);
                }

                ahora2 = ahora.AddHours(-1);


                if (ahora.DayOfWeek == DayOfWeek.Saturday)
                {
                    ahora = ahora.AddDays(1);
                    ahora2 = ahora2.AddDays(1);
                }
                if (ahora.DayOfWeek == DayOfWeek.Sunday)
                {
                    ahora = ahora.AddDays(1);
                    ahora2 = ahora2.AddDays(1);
                }
                //Console.WriteLine(ahora);
            }
            return ahora;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(Form1.nsql))
                {
                    conexion.Open();
                    SqlCommand comm = new SqlCommand("SELECT e.id_empleado, ce.nombre as empresa, b.nombre as base, cc.nombre as cc, p.nombre as puesto  FROM [bd_SiTTi].[dbo].[cg_usuario] u INNER JOIN [bd_SiTTi].[dbo].[cg_empleado] e ON e.id_empleado=u.id_empleado INNER JOIN [bd_SiTTi].[dbo].[cg_base] b ON b.id_base=e.id_base INNER JOIN [bd_SiTTi].[dbo].[cg_cc] cc ON cc.id_cc=e.id_cc INNER JOIN [bd_SiTTi].[dbo].[cg_empresa] ce ON ce.id_empresa=cc.id_empresa INNER JOIN [bd_SiTTi].[dbo].[cg_puesto] p ON p.id_puesto=e.id_puesto WHERE u.id_empleado="+Form1.id_empleado, conexion);
                    SqlDataReader nwReader = comm.ExecuteReader();
                    while (nwReader.Read())
                    {
                        int id_empl_solicita = Convert.ToInt32(nwReader["id_empleado"].ToString());

                        DateTime fechasolicita = DateTime.Now.AddMinutes(-2);
                        String fecha_solicita = fechasolicita.ToString("yyyy-MM-ddTHH:mm:ss.fff");

                        DateTime hora;
                        String fecha_limite;

                        String insert2 = "";
                        if (checkBox1.Checked)
                        {
                            hora = checarhora(4);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CREAR CORREO ELECTRONICO DE LA EMPRESA.
                            insert2 = "INSERT INTO [dbo].[ms_ticket] ([id_empl_solicita] ,[fecha_solicita] ,[id_cat_ticket] ,[id_urgencia] ,[id_impacto] ,[id_prioridad] ,[id_area_soporte] ,[id_cat_soporte] ,[id_cis] ,[id_usr_asignado] ,[descripcion] ,[notas] ,[fecha_limite] ,[fecha_termino] ,[id_asigno] ,[empresa] ,[base] ,[cc] ,[puesto] ,[medioReporte] ,[fecha_asigno] ,[status] ,[uniTiempo] ,[Tiempo] ,[banJust] ,[just] ,[id_usr_just] ,[fecha_just] ,[prov_externo] ,[fecha_asigC] ,[error_usr] ,[scNAV] ,[scNAV_hardware] ,[scNAV_software] ,[no_SiSAC] ,[no_modif] ,[id_usr_ult_modif] ,[fecha_ult_modif] ,[desarrollo]) VALUES (" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 4, 27, 135, 4307, 'FAVOR DE CREAR CORREO ELECTRONICO DE LA EMPRESA.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 4, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox2.Checked)
                        {
                            hora = checarhora(4);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CONFIGURAR CORREO ELECTRONICO DE LA EMPRESA EN MI NUEVA LAPTOP.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 4, 27, 138, 4307, 'FAVOR DE CONFIGURAR CORREO ELECTRONICO DE LA EMPRESA EN MI NUEVA LAPTOP.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 4, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox3.Checked)
                        {
                            hora = checarhora(4);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CREAR USUARIO SITTI.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 1, 8, 43, 4307, 'FAVOR DE CREAR USUARIO SITTI.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 4, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox4.Checked)
                        {
                            hora = checarhora(27);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CONFIGURAR EQUIPO DE COMPUTO NUEVO.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 2, 15, 58, 4307, 'FAVOR DE CONFIGURAR EQUIPO DE COMPUTO NUEVO.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 27, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox5.Checked)
                        {
                            hora = checarhora(6);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE INSTALAR PAQUETERIA OFFICE EN MI EQUIPO.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 2, 15, 456, 4307, 'FAVOR DE INSTALAR PAQUETERIA OFFICE EN MI EQUIPO.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 6, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox6.Checked)
                        {
                            hora = checarhora(4);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CONFIGURAR INTERNET EN MI LAPTOP.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 4, 30, 141, 4307, 'FAVOR DE CONFIGURAR INTERNET EN MI LAPTOP.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 4, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox7.Checked)
                        {
                            hora = checarhora(6);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE INSTALAR Y CONFIGURAR NETSETMAN EN MI LAPTOP.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 2, 15, 456, 4307, 'FAVOR DE INSTALAR Y CONFIGURAR NETSETMAN EN MI LAPTOP.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 6, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        if (checkBox8.Checked)
                        {
                            hora = checarhora(6);
                            fecha_limite = hora.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                            //FAVOR DE CONFIGURAR LA IMPRESORA EN MI LAPTOP.
                            insert2 = "INSERT INTO[dbo].[ms_ticket] ([id_empl_solicita],[fecha_solicita],[id_cat_ticket],[id_urgencia],[id_impacto],[id_prioridad],[id_area_soporte],[id_cat_soporte],[id_cis],[id_usr_asignado],[descripcion],[notas],[fecha_limite],[fecha_termino],[id_asigno],[empresa],[base],[cc],[puesto],[medioReporte],[fecha_asigno],[status],[uniTiempo],[Tiempo],[banJust],[just],[id_usr_just],[fecha_just],[prov_externo],[fecha_asigC],[error_usr],[scNAV],[scNAV_hardware],[scNAV_software],[no_SiSAC],[no_modif],[id_usr_ult_modif],[fecha_ult_modif],[desarrollo]) VALUES(" + id_empl_solicita + ", '" + fecha_solicita + "', 1, 3, 3, 9, 2, 15, 3590, 4307, 'FAVOR DE CONFIGURAR LA IMPRESORA EN MI LAPTOP.', '', '" + fecha_limite + "', null, 4307, '" + nwReader["empresa"].ToString() + "', '" + nwReader["base"].ToString() + "', '" + nwReader["cc"].ToString() + "', '" + nwReader["puesto"].ToString() + "', 'SiTTi', '" + fecha_solicita + "', 'P', 'H', 6, 'N', null, null, null, null, '" + fecha_solicita + "', null, 'N', null, null, null, 0, null, null, 'N')";
                            SqlCommand comm2 = new SqlCommand(insert2, conexion);
                            comm2.ExecuteReader();
                        }
                        MessageBox.Show("SiTTis creados", "SiTTi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error\n\nMensaje: " + ex.Message, "Información del Equipo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
