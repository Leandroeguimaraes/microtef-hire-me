using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using WebAPI.Models;

namespace WebAPI.DAO
{
    public class TransacaoDAO
    {   
        List<TransacaoModel> listTransacoes;
        OleDbConnection conn;
        OleDbCommand command;
        string dirBD = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source =C:/Users/Leandro/Documents/DataBaseAccess/Adquirente.accdb;Persist Security Info = False;";
        public TransacaoDAO()
        {
            conn = new OleDbConnection();
            command = new OleDbCommand();
            this.ConnectToAccess();
        }

        // Abre conexão com o BD
        private void ConnectToAccess()
        {
            conn.ConnectionString = dirBD;

            try
            {
                conn.Open();              
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Não foi possível conectar");
            }
            finally
            {
                conn.Close();
            }
        }

        public List<TransacaoModel> buscaTransacao(int cliente)
        {
            
            int idClient = 0;
            double valor = 0;
                
            try
            {
               
                listTransacoes = null;
              
                conn.Open();
               
                command.Connection = conn;
                
                command.CommandText = "select IdCliente,Valor from Transacao WHERE IdCliente= "+ cliente+ "";

                OleDbDataReader reader = command.ExecuteReader();
                            
                listTransacoes = new List<TransacaoModel>();
                
                while (reader.Read())
                {
                    idClient = int.Parse(reader["IdCliente"].ToString());     

                    valor = double.Parse(reader["Valor"].ToString());                  //mudou
                    
                    TransacaoModel trans = new TransacaoModel(idClient,valor);

                    listTransacoes.Add(trans);
                }              

                conn.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
                
            }

            return listTransacoes;
        }
        public void save(int idCliente, double valor)
        {        
            try
            {
                conn.Open();

                command.Connection = conn;
          
                command.CommandText = "INSERT INTO Transacao(idCliente,Valor) VALUES(@IdCliente, @Valor);";

                command.Parameters.Add("@IdCliente", OleDbType.Integer).Value = idCliente;
                command.Parameters.Add("@Valor", OleDbType.Double).Value = valor;   

                command.ExecuteNonQuery();
  
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.InnerException.Message);
               
            }
            conn.Close();

        }
       
        public void update(RequisicaoModel req)
        {
            conn.Open();

            command.Connection = conn;
            command.CommandText = "Update Transacao (idCliente,Valor) values('" + req.getIdCliente() + "','" + req.getValor() + "')";

            command.ExecuteNonQuery();


        }
       


    }
}