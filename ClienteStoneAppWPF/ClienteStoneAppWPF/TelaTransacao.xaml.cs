using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace ClienteStoneAppWPF
{
    /// <summary>
    /// Interaction logic for TelaTransacao.xaml
    /// </summary>
    public partial class TelaTransacao : Window
    {

        private int idCliente = -1;

        public TelaTransacao()
        {
            InitializeComponent();
      
        }     
      // Exibe as transações realizadas de determinado cliente
        private void preencheCampo(List<TransacaoModel> listaTransacao)
        {
            this.tBTransacoes.Text = "Id Cliente----------------------------------------Valor ";
            foreach (TransacaoModel t in listaTransacao)
            {
                this.tBTransacoes.Text += " " + t.getIdCliente().ToString() + "----------------------------------------------- " + t.getValor().ToString() + "\n";
            }
        }
     
        //Realiza a requisição por metodo GET para receber a lista de transações de determinado cliente.
        public void enviaDados(int idCliente)
        {
            this.setIdCliente(idCliente);
            var myHttpWebRequest = (HttpWebRequest)HttpWebRequest.Create("http://localhost:20556/api/usuario/exibirTransacaoPorCodigo/" + idCliente);
            myHttpWebRequest.Method = "GET";
            myHttpWebRequest.ContentType = "text/xml; encoding='utf-8'";

            //Get Response
            var httpResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                string respostaServidor = streamReader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(respostaServidor))
                {

                    List<TransacaoModel> listaTransacao = (List<TransacaoModel>)JsonConvert.DeserializeObject(respostaServidor, typeof(List<TransacaoModel>));

                    this.preencheCampo(listaTransacao);
                }
                else
                {
                    this.lblStatus.Content = " Cliente não encontrado";
                }
            }
        }
        private void btnRetornar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainWindow.setIdCliente(this.getIdCliente());
            mainWindow.lblIdCliente.Content = "IdCliente: "+this.getIdCliente().ToString();
            this.Close();
        }
        public void setIdCliente(int idCliente)
        {
            this.idCliente = idCliente;
        }
        public int getIdCliente()
        {
            return this.idCliente;
        }
    }
}
