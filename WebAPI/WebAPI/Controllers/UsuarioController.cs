using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.DAO;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Http;
using Newtonsoft.Json;


namespace WebAPI.Controllers
{

    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private TransacaoDAO transacaoDAO = new TransacaoDAO();
        private UsuarioDAO usuarioDAO = new UsuarioDAO();       

        [AcceptVerbs("GET")]
        [Route("exibirTransacaoPorCodigo/{codigo}")]
        public List<TransacaoModel> exibirTransacaoPorCodigo(int codigo)
        {
            return this.transacaoDAO.buscaTransacao(codigo);
        }

        [AcceptVerbs("POST")]
        [Route("efetuaTransacao")]
        [HttpPost]
        public string efetuaTransacao(HttpRequestMessage request)
        {
            // Recebendo requisição e deserializando o conteudo do json
            var content = request.Content;

            string jsonContent = content.ReadAsStringAsync().Result;

            RequisicaoModel reqDes = (RequisicaoModel)JsonConvert.DeserializeObject(jsonContent, typeof(RequisicaoModel));

            RequisicaoModel req = new RequisicaoModel(-1, reqDes.conta, reqDes.agencia, reqDes.senha, reqDes.valor);

            UsuarioModel usuario = this.usuarioDAO.buscaCliente(req);

            //Validações 
            
            if (this.validaUsuario(usuario) == null)
                return  "Usuario não encontrado" ;
            if (this.validaSenha(req) == null)
                return "Senha inválida! Senha deve ter entre 4 a 6 digitos";          
            if (this.validaSaldo(usuario, req) == null)
                return "Saldo insuficiente";
            if (this.verificaValorMinimo(req) == null)
                return "Valor mínimo de 10 centavos";
            if (this.validaCartao(usuario) == null)
                 return  "Cartao bloqueado" ;

            TransacaoModel transacao = new TransacaoModel(usuario.getIdCliente(), req.getValor());

            this.usuarioDAO.update(usuario.getIdCliente(), this.atualizaSaldo(usuario, req));

            this.transacaoDAO.save(usuario.getIdCliente(),req.getValor());

            // Caso aprovada, é enviado o Id do Cliente

            return  "Transação Aprovada."+usuario.getIdCliente().ToString();
        }
                          
        public string validaUsuario(UsuarioModel usuario)
        {
            if (usuario != null)
                return "Usuario encontrado";
            return null;
        }
        public string validaSenha(RequisicaoModel req)
        {
            if (req.getSenha().Length >= 4 && req.getSenha().Length <= 6)
                return "Senha Correta";
            return null;
        }
        public string validaCartao(UsuarioModel usuario)
        {
            if (usuario.getCartao().Equals("Desbloqueado"))
                return "Cartão desbloqueado";
            return null;
        }     
        public string validaSaldo(UsuarioModel usuario, RequisicaoModel req)
        {

            if (usuario.getSaldo() > req.getValor())

                return "Saldo Suficiente";
            return null;
 
        }
        public string verificaValorMinimo(RequisicaoModel req)
        {
           
            if (req.getValor() >= 0.10)
                return "Valor válido";
            return null;
        }
        private double atualizaSaldo(UsuarioModel usuario, RequisicaoModel req)
        {                 
            return usuario.getSaldo() - req.getValor();
        }

       

    }
}