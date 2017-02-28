using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.ENTITE;

namespace Scolaris.TOOLS
{
    public class Constantes
    {
        public const string APP_NAME = "Scolaris Erp";
        public const string ADMINISTRATEUR = "MEG2710/";
        public const string FILE_SEPARATOR = "\\";
        public const int TRIAL_ESSAIE = 30;

        public static string[] MOIS = new string[] { "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

        public const string LANGUE_FRANCAIS = "FRA";
        public const string LANGUE_ANGLAIS = "ANG";

        public static string LAST_FORM = null;
        public static List<string> FIRST_FORM = new List<string>();
        public static IHM.Form_Dictionnaire FORM_DICTIONNAIRE;
        public static IHM.Form_Langue FORM_LANGUE;

        public static IHM.DB.Form_Groupe_Matiere FORM_GROUPE_MATIERE;

        public static Langue LANGUE = null;

        public static long MILLISECONDS
        {
            get
            {
                return Utils.MILLISECONDS(null);
            }
            set { }
        }
    }
}
