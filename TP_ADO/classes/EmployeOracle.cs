using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void InsereCategorie(string pCategorie)
        {
            string requete = "insert into categorie (id,libelle) values (seq_categorie.nextval,:nomCategorie)";
           string requeteId = "select seq_categorie.currval from dual";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleCommand cmdId = new OracleCommand(requeteId, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("nomCategorie", OracleDbType.Varchar2, System.Data.ParameterDirection.Input));
                cmdOracle.Parameters["nomCategorie"].Value = pCategorie;
                cmdOracle.ExecuteNonQuery();
                var id = cmdId.ExecuteScalar();
                Console.WriteLine("Une ligne a été insérée dans catégorie et son Id est : " + id);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InsereCategorieV2(string pCategorie)
        {
            string requete = "insert into categorie(id,libelle) values (seq_categorie.nextval ,:nomCategorie) returning id into :newId ";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("nomCategorie", OracleDbType.Varchar2, System.Data.ParameterDirection.Input));
                cmdOracle.Parameters["nomCategorie"].Value = pCategorie;
                cmdOracle.Parameters.Add(new OracleParameter("newId", OracleDbType.Int16, System.Data.ParameterDirection.Output));
                cmdOracle.ExecuteNonQuery();
                var increm = cmdOracle.Parameters["newId"].Value;
                
                Console.WriteLine("Une ligne a été insérée dans catégorie et son Id est : " + increm);
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InsereCategarieCours(List<String> parametres)
        {
            OracleCommand cmdOracleUne;
            OracleCommand cmdOracleDeux;
            OracleTransaction transOracle = this.connexionAdo.BeginTransaction();
            string requeteUne = @"insert into categorie (id, libelle) values (seq_categorie.nextval , :categ) returning id into :idcateg";
            string requeteDeux = @"insert into cours (codecours, libellecours, nbjours, idcategorie) values (:pCodecours, :pLibelleCours,:pNbJours, :idcategfk)";
            try
            {
                cmdOracleUne = new OracleCommand(requeteUne, this.connexionAdo);
                cmdOracleDeux = new OracleCommand(requeteDeux, this.connexionAdo);
                //parametre de la première requete
                cmdOracleUne.Parameters.Add(new OracleParameter("categ", OracleDbType.Varchar2, System.Data.ParameterDirection.Input));
                cmdOracleUne.Parameters.Add(new OracleParameter("idcateg", OracleDbType.Int16, System.Data.ParameterDirection.Output));
                cmdOracleUne.Parameters["categ"].Value = parametres[0];
                cmdOracleUne.ExecuteNonQuery();
                var idcateg = cmdOracleUne.Parameters["idcateg"].Value;

                //parametre de la seconde requete
                cmdOracleDeux.Parameters.Add(new OracleParameter("pCodecours", OracleDbType.Varchar2, System.Data.ParameterDirection.Input));
                cmdOracleDeux.Parameters.Add(new OracleParameter("pLibelleCours", OracleDbType.Varchar2, System.Data.ParameterDirection.Input));
                cmdOracleDeux.Parameters.Add(new OracleParameter("pNbJours", OracleDbType.Int16, System.Data.ParameterDirection.Input));
                cmdOracleDeux.Parameters.Add(new OracleParameter("idcategfk", OracleDbType.Int16, System.Data.ParameterDirection.Input));
                cmdOracleDeux.Parameters["idcategfk"].Value = idcateg;

                for (int i = 1; i < parametres.Count; i++)
                {
                    String[] tablo = parametres[i].Split(';');
                    cmdOracleDeux.Parameters["pCodecours"].Value = tablo[0];
                    cmdOracleDeux.Parameters["pLibelleCours"].Value = tablo[1];
                    cmdOracleDeux.Parameters["pNbJours"].Value = tablo[2];
                    cmdOracleDeux.ExecuteNonQuery();
                }
                Console.WriteLine("Lignes insérées");
                transOracle.Commit();
                
                
            }
            catch (OracleException ex)
            {
                transOracle.Rollback();
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
