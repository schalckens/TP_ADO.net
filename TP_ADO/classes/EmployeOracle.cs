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

    }
}
