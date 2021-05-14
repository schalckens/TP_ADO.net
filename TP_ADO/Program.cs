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
            
            //try
            //{
            //    EmployeOracle empOracle = new EmployeOracle("OUT");
            //    empOracle.Ouvrir();
            //    ////empOracle.InsereCategorie("Système et réseau");
            //    ////empOracle.InsereCategorieV2("Sécurité informatique");
            //    //List<String> parametresOracle = new List<String>();
            //    //parametresOracle.Add("Développement web");
            //    //parametresOracle.Add("BR020;PHP fondamentaux;4");
            //    //parametresOracle.Add("BR081;PHP objet ;3");
            //    //parametresOracle.Add("BR082;Symphony fondamentaux ;5");
            //    //empOracle.InsereCategarieCours(parametresOracle);

            //    //parametresOracle = new List<String>();
            //    //parametresOracle.Add("Développement mobile");
            //    //parametresOracle.Add("BR060;Android fondamentaux;4");
            //    //parametresOracle.Add("BR060;Android avancé ;3");
            //    //parametresOracle.Add("BR060;Angular JS fondamentaux ;2");
            //    //empOracle.InsereCategarieCours(parametresOracle);

            //    empOracle.Fermer();
            //    Console.WriteLine("--- Fin normal du Program ---");
            //}

            //catch (OracleException ex)
            //{

            //    Console.WriteLine("Erreur Oracle " + ex.Message);
            //}


            try
            {
                EmployeMysql cnMysql = EmployeMysql.getInstance();
                cnMysql.Ouvrir();

                ////cnMysql.InserCategorie("Test3");
                //List<String> parametresMySql = new List<String>();
                //parametresMySql.Add("Développement web");
                //parametresMySql.Add("BR020;PHP fondamentaux;4");
                //parametresMySql.Add("BR081;PHP objet ;3");
                //parametresMySql.Add("BR082;Symphony fondamentaux ;5");
                //cnMysql.InsereCategarieCours(parametresMySql);

                //parametresMySql = new List<String>();
                //parametresMySql.Add("Développement mobile");
                //parametresMySql.Add("BR060;Android fondamentauxx;4");
                //parametresMySql.Add("BR060;Android avancé ;3");
                //parametresMySql.Add("BR060;Angular JS fondamentaux ;2");
                //cnMysql.InsereCategarieCours(parametresMySql);
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
