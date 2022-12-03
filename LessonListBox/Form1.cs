using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LessonListBox
{
    public partial class Form1 : Form
    {   
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        
        public Form1()
        {
            InitializeComponent();
            conn.ConnectionString = "Server=.;Database=BancoTeste;Trusted_Connection=True;";
        }

        List<string> listaDeNames = new List<string>();

        private void GetData()
        {
           try
            {
                string query = "SELECT Name FROM ListBoxNames"; //carrega os dados da tabela para o listbox
                cmd = new SqlCommand("SELECT Name FROM ListBoxNames", conn); //instancias novos comandos no SQL
                cmd.CommandText = query;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd; //busca os dados no banco
                dt = new DataTable();
                da.Fill(dt); //atualiza as linhas
                lbNames.DataSource = dt;
                lbNames.DisplayMember = "Name";
                listaDeNames = dt.Rows.OfType<DataRow>().Select(dr => dr.Field<string>("Name")).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                da.Dispose();
            }
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            GetData();
            
            btnRemove.Enabled = false;
        }

        
        public void btnAdd_Click(object sender, EventArgs e)
        {
            string listboxnames = tbName.Text;
            if(listboxnames == "")
            {
                MessageBox.Show("Digite um nome!");
                tbName.Focus();
            }
            else
            {
                string sqlinsert = $@"INSERT INTO [dbo].[ListBoxNames] ([Name]) VALUES ('{listboxnames}')";
                listaDeNames.Add(listboxnames);
                tbName.Clear();
                lbNames.DataSource = null;
                lbNames.DataSource = listaDeNames;

                using (cmd = new SqlCommand(sqlinsert, conn))
                { 
                    conn.Open();
                    //cmd.CommandText = sqlinsert;
                    cmd.ExecuteNonQuery();
                }
            } 
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int listboxnames = (lbNames.SelectedIndex);
            string nome = lbNames.Text;
            if (lbNames.SelectedIndex > -1)
            {
                string sqldelete = $@"DELETE FROM [dbo].[ListBoxNames] WHERE ([Name]) = ('{nome}')";
                listaDeNames.RemoveAt(listboxnames);
                lbNames.DataSource = null;
                lbNames.DataSource = listaDeNames;

                using (cmd = new SqlCommand(sqldelete, conn))
                {
                    conn.Open();
                    //cmd.CommandText = sqldelete;
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {

                MessageBox.Show("Selecione um Nome!");
            }
        }

        private void lbNames_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void lbNames_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void lbNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbNames.SelectedIndex > -1)
            {
                btnRemove.Enabled = true;
            }
            else
            {
                btnRemove.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textBoxName = tbName.Text;

            if (textBoxName == "")
            {
                lbNames.DataSource = null;
                lbNames.DataSource = listaDeNames;
            }
            else
            {
                List<string> armazenaResultado = new List<string>();
                armazenaResultado = (from item in listaDeNames where item.StartsWith(textBoxName) select item).ToList();

                lbNames.DataSource = null;
                lbNames.DataSource = armazenaResultado;
            }
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
