using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using WebAPI.Models;

namespace WebAPI.DAO
{
    public class UsuarioDAO
    {
        UsuarioModel usuario;
        OleDbConnection conn;
        OleDbCommand command;
        string dirBD = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:/Users/Leandro/Documents/DataBaseAccess/Adquirente.accdb;Persist Security Info = False;";
        public UsuarioDAO()
        {
            conn = new OleDbConnection();
            command = new OleDbCommand();
            this.ConnectToAccess();
        }

        private void ConnectToAccess()
        {   
            conn.ConnectionString = dirBD;

            try
            {
                conn.Open();
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(" Não foi possível conectar");
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public UsuarioModel buscaCliente(RequisicaoModel req)
        {

            int idCliente = 0;
           double saldo = 0;
            string cartao = "";
            string conta = "";
            string agencia = "";
            string senha = "";

            try
            {
                conn.Open();

                command.Connection = conn;

                //command.CommandText = "select * from Cliente where Conta = '" + req.getConta() + "'and Agencia ='" + req.getAgencia() + "'and Senha ='" + req.getSenha() + "'";
                command.CommandText = "select * from Cliente where Conta = '" + req.getConta() + "'and Agencia ='" + req.getAgencia() +  "'";

                OleDbDataReader reader = command.ExecuteReader();

                int count = 0;

                while (reader.Read())
                {
                    count++;
                    
                    //Guarda os dados recebidos do BD

                    idCliente = int.Parse(reader["idCliente"].ToString());
                    saldo = double.Parse(reader["Saldo"].ToString());
                    conta = (reader["Conta"].ToString());
                    cartao = reader["Cartao"].ToString();
                    agencia = reader["Agencia"].ToString();
                    senha = reader["Senha"].ToString();

                }

                usuario = new UsuarioModel(idCliente, conta, agencia, cartao, senha, saldo);

                if(count==0)
                    return null;               

                conn.Close();
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(" Dados Inválidos. Transação negada");
                throw;
            }

            return usuario;
        }
        public void save(UsuarioModel usuario)
        {

            try
            {
                conn.Open();

              

                command.Connection = conn;
 
                command.CommandText = "insert into Transacao ('" + usuario + "')";

                command.ExecuteNonQuery();


            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine(" Não foi possível salvar ");
                throw;
            }
            conn.Close();

        }
       
        public void update(int idCliente, double saldo)
        {
            conn.Open();
          
            command.Connection = conn;
            
            command.CommandText = "UPDATE Cliente SET Saldo = @Saldo Where IdCliente = @idCliente";

            command.Parameters.Add("@Saldo", OleDbType.Double).Value = saldo;
            command.Parameters.Add("@idCliente", OleDbType.Integer).Value = idCliente;

            command.ExecuteNonQuery();

        }

    }
}