using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_ADO.classes;

namespace EmployeDatas.Oracle
{
    class EmployeOracle
    {
        private OracleConnection connexionAdo;
        private static EmployeOracle instance;

        private EmployeOracle(String lieuConnexion)
        { 
            try
            {
                if (lieuConnexion == "OUT")
                {
                    ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["connexionOracle"];
                    string co = String.Format((connex.ConnectionString), ConfigurationManager.AppSettings["hostServerOut"], ConfigurationManager.AppSettings["portServerOut"], ConfigurationManager.AppSettings["sid"], ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["pwd"]);
                    this.connexionAdo = new OracleConnection(co);
                }
                else
                {
                    try
                    {
                        ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["connexionOracle"];
                        string ci = String.Format((connex.ConnectionString), ConfigurationManager.AppSettings["hostServerIn"], ConfigurationManager.AppSettings["portServerIn"], ConfigurationManager.AppSettings["sid"], ConfigurationManager.AppSettings["login"], ConfigurationManager.AppSettings["pwd"]);
                        this.connexionAdo = new OracleConnection(ci);
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
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
                Console.WriteLine("Connexion Oracle ouverte");
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
                Console.WriteLine("Connexion Oracle fermée");
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public void ListeCours()
        {
            string requete = @"select * from cours";
            List<Cours> cours = new List<Cours>();
            try
            {

                Categorie laCategorie;
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    laCategorie = new Categorie(reader.GetInt32(3));
                    Cours cour = new Cours(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), laCategorie);
                    cours.Add(cour);
                }

                foreach (Cours cour in cours)
                {
                    Console.WriteLine(cour);
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void ListeCours(string codeCours)
        {
            string requete = @"select * from cours where codecours = :codeCours ";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("codeCours", OracleDbType.Varchar2));
                cmdOracle.Parameters["codeCours"].Value = codeCours;
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    Categorie laCategorie = new Categorie(reader.GetInt32(3));
                    Cours cour = new Cours(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), laCategorie);
                    Console.WriteLine(cour);
                }

            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static EmployeOracle getInstance(string lieuConnexion)
        {
            if (EmployeOracle.instance == null)
            {
                EmployeOracle.instance = new EmployeOracle(lieuConnexion);
                
            }
            return EmployeOracle.instance;
        }

    }
}
