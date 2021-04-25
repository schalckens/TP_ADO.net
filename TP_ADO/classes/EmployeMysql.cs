using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Mysql
{
    class EmployeMysql
    {
        private string host;
        private int port;
        private string db;
        private string login;
        private string pwd;
        MySqlConnection connexionAdo;

        public EmployeMysql(string host, int port, string db, string login, string pwd)
        {
            this.host = host;
            this.port = port;
            this.db = db;
            this.login = login;
            this.pwd = pwd;
            string csMysql = String.Format("Server = {0}; Port = {1}; Database = {2}; " + "Uid = {3}; " + "Pwd = {4}", host, port, db, login, pwd);
            this.connexionAdo = new MySqlConnection(csMysql);
        }
        public void Ouvrir()
        {
            try
            {
                this.connexionAdo.Open();
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }
        public void Fermer()
        {
            try
            {
                this.connexionAdo.Close();
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            
        }
        public void AfficherTousLesEmployes()
        {
            string requete = @"select numemp, nomemp, prenomemp from employe";
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                MySqlDataReader reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = "Num emp : " + reader.GetString(0) + " Nom emp : " + reader.GetString(1) + " Prenom emp : " + reader.GetString(2);
                    Console.WriteLine(affichage);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void AfficherNbSeminaire()
        {
            string requete = @"select count(seminaire.codesemi) as nbseminaire from seminaire";
            try
            {
                MySqlCommand cmdOracle = new MySqlCommand(requete, this.connexionAdo);
                var reader = cmdOracle.ExecuteScalar();
                Console.WriteLine(reader);
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public void AfficherNbInscritsParCours() 
        {
            string requete = @"select count(*) as nbinscrit, seminaire.codecours from inscrit inner join seminaire on inscrit.codesemi = seminaire.codesemi group by seminaire.codecours";
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                var reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = " Nombre d'inscrit : " + reader.GetString(0) + " Au cours : " + reader.GetString(1);
                    Console.WriteLine(affichage);
                }
                reader.Close();
                
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public int AugmenterSalaireCurseur() 
        {
            MySqlCommand cmdMySql;
            MySqlTransaction transMySql = this.connexionAdo.BeginTransaction();
            string requete = @"select * from employe where codeprojet='PR1'";
            try
            {
                cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                MySqlDataReader reader = cmdMySql.ExecuteReader();
                int nbMaj = 0;
                while (reader.Read())
                {
                    string sqlUpdate = @"update employe set salaire=salaire*1.03 where numemp= :numemp";
                    MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, this.connexionAdo);
                    cmdUpdate.Parameters.Add(new MySqlParameter("numemp", MySqlDbType.Int16));
                    cmdUpdate.Parameters[0].Value = reader.GetValue(0);
                    cmdUpdate.ExecuteNonQuery();
                    nbMaj++;
                }
                transMySql.Commit();
                reader.Close();
                return nbMaj;
            }
            catch (MySqlException ex)
            {
                transMySql.Rollback();
                Console.WriteLine(ex.Message);
                throw new Exception("Erreur à la méthode AugmenterSalaireCurseur");
            }
        }
        public void AfficherProjetNbEmployes(int nb) 
        {
            string requete = @"select codeprojet from employe where codeprojet is not null group by codeprojet having count(*) > " + nb;
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                var reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = reader.GetString(0);
                    Console.WriteLine(affichage);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SeminairesPosterieurs()
        {
            string requete = @"select dateinscrit from inscrit where dateinscrit < STR_TO_DATE( '15/12/2019' , '%d%m%Y') ";
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                //cmdMySql.Parameters.AddWithValue("@date",date);
                var reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    string affichage = reader.GetString(0);
                    Console.WriteLine(affichage);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void InsereProjet() { }
        public void SupprimeSeminaire() { }
        public void RajouterNbJoursCours() { }
    }
}
