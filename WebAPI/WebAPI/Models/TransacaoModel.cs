using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI
{
    public class TransacaoModel
    {
        // atributos em publico para acesso do Json
        public int idCliente;
        public double valor;

        public TransacaoModel()
        {

        }
        public TransacaoModel(int idCliente, double valor)
        {
            this.idCliente=idCliente;
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