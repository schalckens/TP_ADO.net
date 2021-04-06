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
        OracleConnection connexionAdo;

        public EmployeOracle(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            string cs = String.Format("Data Source= " + "(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))" + "(CONNECT_DATA = (SERVICE_NAME = {2}))); User Id = {3}; Password = {4};", host, port, db, login, pwd);
            try
            {
                OracleConnection connexionAdo = new OracleConnection(cs);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void Ouvrir()
        {
            this.connexionAdo.Open();
        }
        public void Fermer()
        {
            this.connexionAdo.Close();
        }

        public void AfficherTousLesCours() 
        {
            string requete = "select * from cours";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Code : " + reader.
                    Console.WriteLine();
                }
                reader.Close();
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AfficherNbProjets()
        {

        }
        public void AfficherSalaireMoyenParProjet()
        {

        }
        public 
    }
}
