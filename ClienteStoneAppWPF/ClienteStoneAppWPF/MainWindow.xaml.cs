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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization.Json;

namespace ClienteStoneAppWPF
{
   
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }
        private int idCliente = -1;
        private string url = "http://localhost:20556/api/usuario/efetuaTransacao";


        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            
            this.tBValor.Text = this.replaceChar(this.tBValor.Text);
          
            RequisicaoModel req = new RequisicaoModel(this.tBConta.Text, this.tBAgencia.Text, this.pBSenha.Password.ToString(), double.Parse(this.tBValor.Text));           
          
            this.enviaDados(req);
        }
        

        //envio dos dados para o servidor
        private void enviaDados(RequisicaoModel req)
        {

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = "application/json";
            httpWebRequest.ContentType = "application/json";
           
          
            httpWebRequest.Method = "POST";

            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                //Serialização para o  Json

                string sJson = JsonConvert.SerializeObject(req);     

                streamWriter.Write(sJson); 
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                
                string respostaServidor = streamReader.ReadToEnd();
               
                char divide = '.';
                string[] substrings = respostaServidor.Split(divide);

                // Caso a transação seja aprovada, o id do Cliente também é recebido

                if (substrings.Length == 2) {

                    string respostaAoUsuario = substrings[0].Remove(0,1);
                   
                    string numIdCliente = substrings[1].Remove(2);
                   
                    this.lblStatus.Content = "Status: " + respostaAoUsuario;

                    this.setIdCliente(int.Parse(numIdCliente));

                    this.lblIdCliente.Content = "IdCliente: " + numIdCliente;
                }
                else
                {
                    this.lblStatus.Content = "Status: " + respostaServidor;              
                    
                }

            }
        }
     
        private void limparCampos()
        {
            this.tBConta.Text = "";
            this.tBAgencia.Text = "";
            this.pBSenha.Password = "";
            this.tBValor.Text = "";
        }
        private void btnExibirTransacoes_Click(object sender, RoutedEventArgs e)
        {
            TelaTransacao telaTransacao = new TelaTransacao();
            telaTransacao.Show();
            telaTransacao.enviaDados(this.getIdCliente());          
            this.Close();
        }

        private void btnLimpar_Click(object sender, RoutedEventArgs e)
        {
            this.limparCampos();
        }
       
        // Tratamento do valor inserido
        private string replaceChar(string a)
        {
            int p = a.IndexOf('.');
            if(p!=1)
                return a.Replace('.', ',');
            return a;

        }
        public void setIdCliente(int idCliente)
        {
            this.idCliente=idCliente;
        }
        public int getIdCliente()
        {
            return this.idCliente;
        }
    }
}
