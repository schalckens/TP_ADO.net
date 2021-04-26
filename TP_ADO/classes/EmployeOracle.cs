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

        public void AfficherTousLesCours() 
        {
            string requete = @"select codecours, libellecours, nbjours from cours";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Code : " + reader.GetString(0) + " Libelle : " + reader.GetString(1) + " NbJours : " + reader.GetInt32(2);

                    Console.WriteLine(affichage);
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
            string requete = @"select count(projet.codeprojet) as nbprojet from projet";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                var reader = cmdOracle.ExecuteScalar();
                Console.WriteLine(reader);
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AfficherSalaireMoyenParProjet()
        {
            string requete = @"select coalesce(employe.codeprojet,'Aucun'), AVG(employe.salaire) as moysalaire, count(*) as nbemploye, coalesce(projet.nomprojet, 'null') from employe left join projet on employe.codeprojet=projet.codeprojet group by coalesce(employe.codeprojet,'Aucun'),coalesce(projet.nomprojet, 'null')";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Projet : " + reader.GetString(0) + " Salaire Moyen : " + reader.GetInt32(1)+ " Nb Employés : " + reader.GetInt32(2) + " Nom du projet : " + reader.GetString(3);

                    Console.WriteLine(affichage);
                }
                reader.Close();
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public int AugmenterSalaireCurseur()
        {
            OracleCommand cmdOracle;
            OracleTransaction transOracle = this.connexionAdo.BeginTransaction();
            string requete = @"select * from employe where codeprojet='PR1'";
            try
            {
                cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                int nbMaj = 0;
                while (reader.Read())
                {
                    string sqlUpdate = @"update employe set salaire=salaire*1.03 where numemp= :numemp";
                    OracleCommand cmdUpdate = new OracleCommand(sqlUpdate, this.connexionAdo);
                    cmdUpdate.Parameters.Add(new OracleParameter("numemp", OracleDbType.Int16));
                    cmdUpdate.Parameters[0].Value = reader.GetValue(0);
                    cmdUpdate.ExecuteNonQuery();
                    nbMaj++;
                }
                transOracle.Commit();
                reader.Close();
                return nbMaj;
            }
            catch (OracleException ex)
            {
                transOracle.Rollback();
                Console.WriteLine(ex.Message);
                throw new Exception("Erreur à la méthode AugmenterSalaireCurseur");
            }
            
        }
        public void AfficherEmployesSalaire(int salaire)
        {
            try
            {
                string requete = @"select numemp,nomemp,prenomemp,salaire from employe where salaire < :salaireLimite";
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("salaireLimite", OracleDbType.Decimal));
                cmdOracle.Parameters["salaireLimite"].Value = salaire;
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Numéro Emp : " + reader.GetInt32(0) + " Nom Emp : " + reader.GetString(1) + " Prenom Emp : " + reader.GetString(2) + " Salaire Emp : " + reader.GetInt32(3);

                    Console.WriteLine(affichage);
                }
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AfficherSalaireEmploye(int numemp)
        {
            string requete = "select numemp,nomemp,prenomemp,salaire from employe where numemp = :numemp";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("numemp", OracleDbType.Int16));
                cmdOracle.Parameters["numemp"].Value = numemp;
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = " Numéro Emp : " + reader.GetInt32(0) + " Nom Emp : " + reader.GetString(1) + " Prenom Emp : " + reader.GetString(2) + " Salaire Emp : " + reader.GetInt32(3);

                    Console.WriteLine(affichage);
                }
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void InsereCours(string codecours,string libelleCours,int nbJours)
        {
            string requete = "INSERT INTO cours(codecours,libellecours,nbjours) VALUES ( :codecours, :libellecours, :nbjours)";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("codecours", OracleDbType.Varchar2));
                cmdOracle.Parameters["codecours"].Value = codecours;
                cmdOracle.Parameters.Add(new OracleParameter("libellecours", OracleDbType.Varchar2));
                cmdOracle.Parameters["libellecours"].Value = libelleCours;
                cmdOracle.Parameters.Add(new OracleParameter("nbjours", OracleDbType.Int16));
                cmdOracle.Parameters["nbjours"].Value = nbJours;
                cmdOracle.ExecuteNonQuery();
                Console.WriteLine("Ligne insérée");
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void SupprimeCours(string codecours)
        {
            string requete = "DELETE FROM cours WHERE codecours = :codecours";
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                cmdOracle.Parameters.Add(new OracleParameter("codecours", OracleDbType.Varchar2));
                cmdOracle.Parameters["codecours"].Value = codecours;
                cmdOracle.ExecuteNonQuery();
                Console.WriteLine("Ligne supprimée");
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AugmenterSalaire(int pourcentage, string projet)
        {
            string requete = "select numemp, nomemp, prenomemp, salaire + ((salaire*"+Convert.ToString(pourcentage)+")/100) as newsalaire, codeprojet from employe where codeprojet = '"+ projet +"'";
            int cpt = 0;
            try
            {
                OracleCommand cmdOracle = new OracleCommand(requete, this.connexionAdo);
                OracleDataReader reader = cmdOracle.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Num Emp : " + reader.GetString(0) + " Nom Emp : " + reader.GetString(1) + " Prenom Emp :"+reader.GetString(2) + " Salaire : "+reader.GetString(3) + " Projet : " + reader.GetString(4) ;
                    cpt++;
                    Console.WriteLine(affichage);
                }
                Console.WriteLine("Il y a "+cpt+" ligne mises à jours");
            }
            catch (OracleException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

    }
}
