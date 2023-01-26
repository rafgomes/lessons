using ConsumindoApiAluno.Entities;
using ConsumindoApiAluno.Entities.Services;
using System;
using System.Windows.Forms;

namespace ConsumindoApiAluno2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBuscar.Text);
            AlunoServices alunoServices = new AlunoServices();
            Aluno alunoEncontrado = await alunoServices.Integracao(id);

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
