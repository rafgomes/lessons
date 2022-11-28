using Leitor_ABCConfig.Entities;
using Leitor_ABCConfig.Libs;
using Leitor_ABCConfig.Libs.Parsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leitor_ABCConfig
{
    public partial class Form1 : Form
    {
        public string pesquisa;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            pesquisa = comboIDC.Text;

            ABCConfigParser parser = new ABCConfigParser();
            ABCConfig abcConfig = parser.GetABCConfig(@"C:\Projets\abcparaguay\src\config\ABCConfig.xml");

            var printer = new IDCPrinter();
            printer.PrintIDC(abcConfig.ImageDummyClasses, pesquisa);

            txtWidth.Text = printer.width.ToString();
            txtHeight.Text = printer.height.ToString();
            txtX.Text = printer.x.ToString();
            txtY.Text = printer.y.ToString();

        }

        private void btnTXT_Click(object sender, EventArgs e)
        {
            ABCConfigParser parser = new ABCConfigParser();
            ABCConfig abcConfig = parser.GetABCConfig(@"C:\Projets\abcparaguay\src\config\ABCConfig.xml");

            var creattxt = new TXTSaver();
            creattxt.ToTXTFile(abcConfig.ImageDummyClasses, pesquisa);
        }
    }
}
