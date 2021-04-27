using EmployeDatas.Mysql;
using EmployeDatas.Oracle;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;

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
            //try
            //{
            //    EmployeOracle empOracle = new EmployeOracle(host, port, sid, login, pwd);
            //    empOracle.Ouvrir();
            //    
            //    empOracle.Fermer();
            //    Console.WriteLine("--- Fin normal du Program ---");
            //}

            //catch (OracleException ex)
            //{

            //    Console.WriteLine("Erreur Oracle " + ex.Message);
            //}

            string hostMysql = "127.0.0.1";
            int portMysql = 3306;
            string baseMysql = "dbadonet";
            string uidMysql = "emploueado";
            string pwdMysql = "employeado";
            try
            {
                EmployeMysql cnMysql = new EmployeMysql(hostMysql, portMysql, baseMysql, uidMysql, pwdMysql);
                cnMysql.Ouvrir();

                cnMysql.InserCategorie("Test3");

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
