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
            //    //empOracle.AfficherTousLesCours(); //6 lignes
            //    //empOracle.AfficherNbProjets(); //5 lignes
            //    //empOracle.AfficherSalaireMoyenParProjet(); //6 lignes
            //    //empOracle.AfficherEmployesSalaire(10000); //4 lignes
            //    //empOracle.InsereCours("BR099", "Apprentissage JDBC", 4); //missing comma ou invalid character
            //    //empOracle.SupprimeCours("BR099");
            //    //empOracle.AfficherSalaireEmploye(13); //1 lignes
            //    //empOracle.AugmenterSalaire(2, "PR2"); //5 lignes
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
                //cnMysql.AfficherTousLesEmployes(); //21 lignes
                //cnMysql.AfficherNbSeminaire(); //19 lignes
                cnMysql.AfficherNbInscritsParCours(); //5 lignes
                cnMysql.AugmenterSalaireCurseur();
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
