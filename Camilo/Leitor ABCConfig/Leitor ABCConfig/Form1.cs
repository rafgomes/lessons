using Leitor_ABCConfig.Entities;
using Leitor_ABCConfig.Libs;
using Leitor_ABCConfig.Libs.Parsers;
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

namespace Leitor_ABCConfig
{
    public partial class Form1 : Form
    {
        public string pesquisa;
        ABCConfig abcConfig;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ABCConfigParser parser = new ABCConfigParser();
            this.abcConfig = parser.GetABCConfig(@"C:\Projets\Lessons\Leitor ABCConfig\ABCConfig.xml");

            foreach (ImageDummyClass item in abcConfig.ImageDummyClasses)
            {
                comboIDC.Items.Add(item.Name);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            pesquisa = comboIDC.Text;

            IDCSearch search = new IDCSearch();
            ImageDummyClass idcResult = search.GetImageDummyClass(this.abcConfig.ImageDummyClasses, pesquisa);

            if (idcResult != null)
            {
                txtWidth.Text = idcResult.Width.ToString();
                txtHeight.Text = idcResult.Height.ToString();
                txtX.Text = idcResult.X.ToString();
                txtY.Text = idcResult.Y.ToString();
            }
        }

        private void btnTXT_Click(object sender, EventArgs e)
        {
            var creattxt = new TXTSaver();
            creattxt.ToTXTFile(this.abcConfig.ImageDummyClasses, pesquisa);
        }

        private void comboIDC_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var objetoSelecionado = comboIDC.SelectedItem;
            //string pesquisa = objetoSelecionado.ToString();

            //IDCSearch search = new IDCSearch();
            //ImageDummyClass idcResult = search.GetImageDummyClass(this.abcConfig.ImageDummyClasses, pesquisa);

            //if (idcResult != null)
            //{
            //    txtWidth.Text = idcResult.Width.ToString();
            //    txtHeight.Text = idcResult.Height.ToString();
            //    txtX.Text = idcResult.X.ToString();
            //    txtY.Text = idcResult.Y.ToString();

            //}
        }
    }
}
