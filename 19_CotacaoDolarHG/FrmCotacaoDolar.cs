using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Windows.Forms;

namespace CotacaoDolarHG
{
    public partial class FrmCotacaoDolar : Form
    {
        public FrmCotacaoDolar()
        {
            InitializeComponent();
        }

        private void FrmCotacaoDolar_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string strURL = "https://api.hgbrasil.com/finance?array_limit=1&fields=only_results,USD&key=745c4038";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = client.GetAsync(strURL).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;

                        Market market = JsonConvert.DeserializeObject<Market>(result);

                        lblCompra.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Compra);
                        lblVenda.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", market.Currency.Venda);
                        lblVariacao.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:P}", market.Currency.Variacao / 100);
                    }
                    else
                    {
                        lblCompra.Text = "-";
                        lblVenda.Text = "-";
                        lblVariacao.Text = "-";
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                

            }
        }
    }
}
