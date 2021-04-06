using EmployeDatas.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;

namespace ClientCommande
{
    class Program
    {
        static void Main()
        {
            string host = "freesio.lyc-bonaparte.fr";
            int port = 21521;
            string sid = "slam";
            string login = "schalckensado";
            string pwd = "sio";
            try
            {
                EmployeOracle empOracle = new EmployeOracle(host, port, sid, login, pwd);
                empOracle.Ouvrir();
                //empOracle.AfficherTousLesCours();
                empOracle.Fermer();
                Console.WriteLine("connecxion terminé");
            }

            catch (OracleException ex)
            {

                Console.WriteLine("Erreur Oracle " + ex.Message);
            }

            //string hostMysql = "127.0.0.1";
            //int portMysql = 3306;
            //string baseMysql = "dbadonet";
            //string uidMysql = "emploueado";
            //string pwdMysql = "employeado";
            //try
            //{
            //    string csMysql = String.Format("Server = {0}; Port = {1}; Database = {2}; " + "Uid = {3}; " + "Pwd = {4}", hostMysql, portMysql, baseMysql, uidMysql, pwdMysql);
            //    MySqlConnection cnMysql = new MySqlConnection(csMysql);
            //    cnMysql.Open();
            //    Console.WriteLine("connecté Mysql");
            //    cnMysql.Close();
            //    Console.WriteLine("déconnecté Mysql");
            //}
            //catch (MySqlException ex)
            //{
            //    Console.WriteLine("Erreur Mysql " + ex.Message);
            //}


            Console.ReadKey();
        }
    }
}
