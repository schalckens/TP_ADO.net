using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeDatas.Mysql
{
    class EmployeMysql
    {
        MySqlConnection connexionAdo;

        public EmployeMysql(string lieuConnexion)
        {
            if (lieuConnexion == "IN")
            {
                ConnectionStringSettings connex = ConfigurationManager.ConnectionStrings["connexionMySql"];
                string co = String.Format((connex.ConnectionString), ConfigurationManager.AppSettings["hostServerIn"], ConfigurationManager.AppSettings["hostServerIn"]);            }
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
            string requete = @"insert into categorie(libelle) values (@categ)";
            string requeteId = @"select last_insert_id() from categorie";
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
        public void InsereCategarieCours(List<String> parametres)
        {
            MySqlCommand cmdMySqlUne;
            MySqlCommand cmdMySqlDeux;
            MySqlTransaction transMySql = this.connexionAdo.BeginTransaction();
            string requeteUne = @"insert into categorie (libelle) values (@categ)";
            string requeteDeux = @"insert into cours (codecours, libellecours, nbjours, idcategorie) values (@codecours, @libellecours,@nbjours, @idcategfk)";
            try
            {
                cmdMySqlUne = new MySqlCommand(requeteUne, this.connexionAdo);
                cmdMySqlDeux = new MySqlCommand(requeteDeux, this.connexionAdo);
                //parametre de la première requete
                cmdMySqlUne.Parameters.AddWithValue("categ", parametres[0]);
                cmdMySqlUne.ExecuteNonQuery();
                var increm = cmdMySqlUne.LastInsertedId;

                //parametre de la seconde requete
                cmdMySqlDeux.Parameters.AddWithValue("idcategfk", increm);
                cmdMySqlDeux.Parameters.Add("codecours", MySqlDbType.VarChar);
                cmdMySqlDeux.Parameters.Add("libellecours", MySqlDbType.VarChar);
                cmdMySqlDeux.Parameters.Add("nbjours", MySqlDbType.Double);

                for (int i = 1; i < parametres.Count; i++)
                {
                    String[] tablo = parametres[i].Split(';');

                    cmdMySqlDeux.Parameters["codecours"].Value = tablo[0];
                    cmdMySqlDeux.Parameters["libellecours"].Value = tablo[1];
                    cmdMySqlDeux.Parameters["nbjours"].Value = tablo[2];
                    cmdMySqlDeux.ExecuteNonQuery();
                }
                Console.WriteLine("Lignes insérées");
                transMySql.Commit();

            }
            catch (MySqlException ex)
            {
                transMySql.Rollback();
                Console.WriteLine(ex.Message);
            }

        }
    }
}
