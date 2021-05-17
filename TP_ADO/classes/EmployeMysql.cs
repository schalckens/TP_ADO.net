using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_ADO.classes;

namespace EmployeDatas.Mysql
{
    class EmployeMysql
    {
        MySqlConnection connexionAdo; // l'objet connexion de la base de données
        private static EmployeMysql instance; // l'instance de sa prope classe

        /// <summary>
        /// Constructeur de la classe EmployeMysql
        /// </summary>
        private EmployeMysql()
        {
            ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["connexionMySql"];
            string csMysql = String.Format((connex.ConnectionString), ConfigurationManager.AppSettings["mysqlHost"], ConfigurationManager.AppSettings["mysqlPort"], ConfigurationManager.AppSettings["mysqlDatabase"], ConfigurationManager.AppSettings["mysqlUid"], ConfigurationManager.AppSettings["mysqlPwd"]);
            this.connexionAdo = new MySqlConnection(csMysql);
        }


        public void Ouvrir()
        {
            try
            {
                this.connexionAdo.Open();
                Console.WriteLine("Connexion MySql ouverte");
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
                EmployeMysql.instance = null;
                Console.WriteLine("Connexion MySql fermée");
            }
            catch (MySqlException ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        public void ListeCoursV1()
        {
            string requete = @"select * from cours";
            List<Cours> cours = new List<Cours>();
            try
            {
                
                Categorie laCategorie;
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                MySqlDataReader reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    laCategorie = new Categorie(reader.GetInt32(3));
                    Cours cour = new Cours(reader.GetString(0), reader.GetString(1),reader.GetInt32(2),laCategorie);
                    cours.Add(cour);
                }

                foreach(Cours cour in cours)
                {
                    Console.WriteLine(cour);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void ListeCoursV1(string codeCours)
        {
            string requete = @"select * from cours where codecours = @codeCours ";
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete, this.connexionAdo);
                cmdMySql.Parameters.AddWithValue("@codeCours", codeCours);
                MySqlDataReader reader = cmdMySql.ExecuteReader();
                while (reader.Read())
                {
                    Categorie laCategorie = new Categorie(reader.GetInt32(3));
                    Cours cour = new Cours(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), laCategorie);
                    Console.WriteLine(cour);
                }
                
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ListeCoursV2(Cours tableMySql)
        {
            
            try
            {
               
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Accesseur à la propre instance de la classe
        /// En mode static pour ne pas avoir à instancier la classe ......
        /// Du coup l'attribut instance sera en static
        /// </summary>
        /// <returns></returns>
        public static EmployeMysql getInstance()
        {
            if (EmployeMysql.instance == null)
            {
                EmployeMysql.instance = new EmployeMysql();
            }
            return EmployeMysql.instance;
        }
    }
}
