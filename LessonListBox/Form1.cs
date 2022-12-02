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
            listaDeNames.RemoveAt(lbNames.SelectedIndex);
            lbNames.DataSource = null;
            lbNames.DataSource = listaDeNames;
        }
    }
}
