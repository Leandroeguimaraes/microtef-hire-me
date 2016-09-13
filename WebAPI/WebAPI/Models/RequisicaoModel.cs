using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{


    public class RequisicaoModel
    {
        // atributos em publico para acesso do Json

        public int idCliente;
        public string conta;
        public string agencia;
        public string senha;
        public double valor;
       
        public RequisicaoModel(int idCliente, string conta, string agencia,string senha, double valor)
        {
            this.idCliente = idCliente;
            this.conta = conta;
            this.agencia = agencia;
            this.senha = senha;
            this.valor = valor;
        }
       public void setIdCliente(int idCliente)
        {
            this.idCliente = idCliente;
        }
        public int getIdCliente()
        {
            return idCliente;
        }
        public void setConta(string conta)
        {
            this.conta = conta;
        }
        public string getConta()
        {
            return conta;
        }
        public void setAgencia(string agencia)
        {
            this.agencia = agencia;
        }
        public string getAgencia()
        {
            return agencia;
        }
        public void setSenha(string senha)
        {
            this.senha = senha;
        }
        public string getSenha()
        {
            return senha;
        }
        public void setValor(double valor)
        {
            this.valor = valor;
        }
        public double getValor()
        {
            return valor;
        }
    }
    
}