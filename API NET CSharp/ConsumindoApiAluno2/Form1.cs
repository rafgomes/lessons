using ConsumindoApiAluno.Entities;
using ConsumindoApiAluno.Entities.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumindoApiAluno2
{
    public partial class Form1 : Form
    {
        public int id;
        public Aluno alunoEncontrado;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            AlunoServices alunoServices = new AlunoServices();
            alunoEncontrado = await alunoServices.Integracao(id);
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            id = int.Parse(txtBuscar.Text);

            if (!alunoEncontrado.Verificacao)
            {
                lblId.Text = alunoEncontrado.Id.ToString();
                lblNome.Text = alunoEncontrado.Nome.ToString();
                lblMatricula.Text = alunoEncontrado.Matricula.ToString();
                lblSituacao.Text = alunoEncontrado.Situacao.ToString();
            }
            else
            {
                MessageBox.Show("Aluno não encontrado!");
            }
        }
    }
}
