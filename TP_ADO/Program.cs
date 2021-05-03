using EmployeDatas.Mysql;
using EmployeDatas.Oracle;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace ClientCommande
{
    class Program
    {
        static void Main()
        {
            //string host = "freesio.lyc-bonaparte.fr";
            //int port = 21521;
            //string sid = "slam";
            //string login = "schalckensado";
            //string pwd = "sio";
            string host = "10.10.2.10";
            int port = 1521;
            string sid = "slam";
            string login = "schalckensado";
            string pwd = "sio";
            try
            {
                EmployeOracle empOracle = new EmployeOracle(host, port, sid, login, pwd);
                empOracle.Ouvrir();
                //empOracle.InsereCategorie("Système et réseau");
                //empOracle.InsereCategorieV2("Sécurité informatique");
                List<String> parametresOracle = new List<String>();
                parametresOracle.Add("Développement web");
                parametresOracle.Add("BR020;PHP fondamentaux;4");
                parametresOracle.Add("BR081;PHP objet ;3");
                parametresOracle.Add("BR082;Symphony fondamentaux ;5");
                empOracle.InsereCategarieCours(parametresOracle);

                parametresOracle = new List<String>();
                parametresOracle.Add("Développement mobile");
                parametresOracle.Add("BR060;Android fondamentaux;4");
                parametresOracle.Add("BR060;Android avancé ;3");
                parametresOracle.Add("BR060;Angular JS fondamentaux ;2");
                empOracle.InsereCategarieCours(parametresOracle);

                empOracle.Fermer();
                Console.WriteLine("--- Fin normal du Program ---");
            }

            catch (OracleException ex)
            {

                Console.WriteLine("Erreur Oracle " + ex.Message);
            }

            string hostMysql = "127.0.0.1";
            int portMysql = 3306;
            string baseMysql = "dbadonet";
            string uidMysql = "employeado";
            string pwdMysql = "employeado";
            try
            {
                EmployeMysql cnMysql = new EmployeMysql(hostMysql, portMysql, baseMysql, uidMysql, pwdMysql);
                cnMysql.Ouvrir();

                //cnMysql.InserCategorie("Test3");
                List<String> parametresMySql = new List<String>();
                parametresMySql.Add("Développement web");
                parametresMySql.Add("BR020;PHP fondamentaux;4");
                parametresMySql.Add("BR081;PHP objet ;3");
                parametresMySql.Add("BR082;Symphony fondamentaux ;5");
                cnMysql.InsereCategarieCours(parametresMySql);

                parametresMySql = new List<String>();
                parametresMySql.Add("Développement mobile");
                parametresMySql.Add("BR060;Android fondamentauxx;4");
                parametresMySql.Add("BR060;Android avancé ;3");
                parametresMySql.Add("BR060;Angular JS fondamentaux ;2");
                cnMysql.InsereCategarieCours(parametresMySql);
                cnMysql.Fermer();

                Console.WriteLine("--- Fin normal du Program ---");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur Mysql " + ex.Message);
            }


            Console.ReadKey();
        }
    }
}
