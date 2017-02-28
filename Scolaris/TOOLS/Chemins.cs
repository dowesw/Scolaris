using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Scolaris.TOOLS
{
    public class Chemins
    {

        public static string cheminStartup = Application.StartupPath;
        public static string cheminDefault = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string cheminRoot = System.IO.Directory.GetDirectoryRoot(Environment.ExpandEnvironmentVariables("%windir%"));
        public static string cheminWindows = Environment.ExpandEnvironmentVariables("%windir%");
        public static string cheminSystem32 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "System32";
        public static string cheminSystem64 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "SysWOW64";
        public static string domainName = Environment.UserDomainName;
        public static string usersName = Environment.UserName;
        public static string machineName = Environment.MachineName.Normalize();

        public static string CheminParametre()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Parametres";
        }

        public static string CheminConfiguration()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Configurations";
        }

        public static string CheminServeur()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Serveur";
        }

        public static string CheminUsers()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Users";
        }

        private static string CheminStart()
        {
            string chemin = cheminStartup;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminDatabase()
        {
            string chemin = CheminStart() + "Database";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminImage()
        {
            string chemin = CheminStart() + "Images";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminUser()
        {
            string chemin = cheminDefault;
            chemin = chemin.Substring(0, 1);
            chemin += Constantes.FILE_SEPARATOR + "Users" + Constantes.FILE_SEPARATOR + usersName;
            return chemin + Constantes.FILE_SEPARATOR;
        }
    }
}
