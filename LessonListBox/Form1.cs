using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public Form1()
        {
            InitializeComponent();
        }

        List<string> listaDeNames = new List<string>();

        public void Form1_Load(object sender, EventArgs e)
        {
            btnRemove.Enabled = false;
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            if(tbName.Text == "")
            {
                MessageBox.Show("Digite um nome!");
                tbName.Focus();
            }
            else
            {
                listaDeNames.Add(tbName.Text);
                tbName.Clear();
                lbNames.DataSource = null;
                lbNames.DataSource = listaDeNames;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if(lbNames.SelectedIndex > -1)
            {
                listaDeNames.RemoveAt(lbNames.SelectedIndex);
                lbNames.DataSource = null;
                lbNames.DataSource = listaDeNames;
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
            if(lbNames.SelectedIndex == -1) { 
                btnRemove.Enabled = false;
            }
            else
            {
                btnRemove.Enabled = true;   
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
    }
}
