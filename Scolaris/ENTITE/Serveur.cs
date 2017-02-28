using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolaris.ENTITE
{
    [Serializable]
    public class Serveur
    {
        private string adresse;
        private int port;
        private string user;
        private string password;
        private string database;

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string User
        {
            get { return user; }
            set { user = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        public static Serveur Default()
        {
            Serveur s = new Serveur();
            s.adresse = "127.0.0.1";
            s.port = 5432;
            s.database = "scolaris_erp";
            s.password = "yves1910/";
            s.user = "postgres";
            return s;
        }

        public Boolean Control(bool msg)
        {
            return Control(this, msg);
        }

        public static Boolean Control(Serveur bean, bool msg)
        {
            if (bean == null)
            {
                if (msg)
                    TOOLS.Messages.Error("Serveur Incorrect!");
                return false;
            }
            if (bean.adresse == null || bean.adresse.Trim().Equals(""))
            {
                if (msg)
                    TOOLS.Messages.Error("L'adresse du serveur ne peut pas être null!");
                return false;
            }
            if (bean.database == null || bean.database.Trim().Equals(""))
            {
                if (msg)
                    TOOLS.Messages.Error("La base de donnée ne peut pas être null!");
                return false;
            }
            if (bean.port < 0)
            {
                if (msg)
                    TOOLS.Messages.Error("Le numéro du port ne peut pas être inferieur a 0!");
                return false;
            }
            return true;
        }
    }
}
