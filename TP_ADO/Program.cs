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

            try
            {
                EmployeOracle empOracle = EmployeOracle.getInstance("IN");
                empOracle.Ouvrir();
                empOracle.Fermer();
                Console.WriteLine("--- Fin normal du Program ---");
            }

            catch (OracleException ex)
            {

                Console.WriteLine("Erreur Oracle " + ex.Message);
            }


            try
            {
                EmployeMysql cnMysql = EmployeMysql.getInstance();
                cnMysql.Ouvrir();
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
