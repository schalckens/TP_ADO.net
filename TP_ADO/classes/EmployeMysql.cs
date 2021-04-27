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
        public void InserCategorie(string categorie)
        {
            string requete = @"insert into categorie(libelle) values (@categ);";
            string requeteId = @"select last_insert_id() from categorie;";
            try
            {
                MySqlCommand cmdMySql = new MySqlCommand(requete,this.connexionAdo);
                MySqlCommand cmdId = new MySqlCommand(requeteId, this.connexionAdo);
                cmdMySql.Parameters.AddWithValue("categ", categorie);
                cmdMySql.ExecuteNonQuery();
                var increment = cmdMySql.LastInsertedId;
                var incrementv2 = cmdId.ExecuteScalar();
                Console.WriteLine("Il y a une catégorie inséré et son identifiant est : "+increment);
                Console.WriteLine("derniere id v2 : " + incrementv2);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
