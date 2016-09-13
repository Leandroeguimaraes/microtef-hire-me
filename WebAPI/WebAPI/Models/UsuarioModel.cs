using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class UsuarioModel
    {

        private int idCliente;
        private string conta;
        private string agencia;
        private string cartao;
        private string senha;
        private double saldo;
       

        public UsuarioModel()
        {
        }

        public UsuarioModel(int idCliente, string conta, string agencia,string cartao,string senha,double saldo)
        {
            this.idCliente = idCliente;
            this.conta = conta;
            this.agencia = agencia;
            this.cartao = cartao;
            this.senha = senha;
            this.saldo = saldo;
        }

        public void setIdCliente(int idCliente)
        {
            this.idCliente = idCliente;
        }
        public int getIdCliente()
        {
            return this.idCliente;
        }
        public void setConta(string conta)
        {
            this.conta = conta;
        }
        public string getConta()
        {
            return this.conta;
        }
        public void setAgencia(string agencia)
        {
            this.agencia = agencia;
        }
        public string getAgencia()
        {
            return this.agencia;
        }
        public void setCartao(string cartao)
        {
            this.cartao = cartao;
        }
        public string getCartao()
        {
            return this.cartao;
        }
        public void setSenha(string senha)
        {
            this.senha = senha;
        }
        public string getSenha()
        {
            return this.senha;
        }
        public void setSaldo(double saldo)
        {
            this.saldo = saldo;
        }
        public double getSaldo()
        {
            return this.saldo;
        }



    }
}