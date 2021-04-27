using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Oracle
{
    class EmployeOracle
    {
        private string host;
        private int port;
        private string db;
        private string login;
        private string pwd;
        private OracleConnection connexionAdo;

        public EmployeOracle(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            string cs = String.Format("Data Source= " + 
                "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))" +
                "(CONNECT_DATA = (SERVICE_NAME = {2}))); User Id = {3}; Password = {4};",
                this.host, this.port, this.db, this.login, this.pwd);
            try
            {
                this.connexionAdo = new OracleConnection(cs);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void Ouvrir()
        {
            try
            {
                connexionAdo.Open();
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }
        public void Fermer()
        {
            try
            {
                connexionAdo.Close();
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void InsereCategorie(string categorie)
        {
            string requete = "insert into categorie(id,categorie) values (seq_categorie.nextval,:categorie);";
            string requeteId = "select seq_categorie.currval from dual;";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleCommand cmdId = new OracleCommand(requeteId, this.connexionAdo);
                cmdOracle.Parameters.Add("categorie", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
                cmdOracle.Parameters["categorie"].Value = categorie;
                cmdOracle.ExecuteNonQuery();
                var id = cmdId.ExecuteScalar();
                Console.WriteLine("Une ligne a été insérée dans catégorie et son Id est : "+id);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsereCategorieV2(string categorie)
        {
            string requete = "insert into categorie(id,categorie) values (seq_categorie.nextval,:categorie) returning id into :increment;";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add("categorie", OracleDbType.Varchar2, System.Data.ParameterDirection.Input);
                cmdOracle.Parameters.Add("increment", OracleDbType.Int16, System.Data.ParameterDirection.Output);


            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
