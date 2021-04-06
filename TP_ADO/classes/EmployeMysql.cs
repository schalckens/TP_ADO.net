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
            this.connexionAdo.Open();
        }
        public void Fermer()
        {
            this.connexionAdo.Close();
        }
    }
}
