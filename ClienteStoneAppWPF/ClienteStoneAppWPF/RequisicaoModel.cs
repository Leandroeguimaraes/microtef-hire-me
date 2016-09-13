using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteStoneAppWPF
{
    //Atributos em publico para identificação do Json
    class RequisicaoModel
    {
        public string conta;
        public string agencia;
        public string senha;
        public double valor;


        public RequisicaoModel(string conta, string agencia, string senha, double valor)
        {
            this.conta = conta;
            this.agencia = agencia;
            this.senha = senha;
            this.valor = valor;
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
