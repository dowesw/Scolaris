using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scolaris.BLL;
using Scolaris.IHM;
using Scolaris.IHM.DB;
using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Load();
            //Start();
            View();
        }

        static void Load()
        {
            Configuration.Return();
            Constantes.LANGUE = LangueBLL.One(Constantes.LANGUE_ANGLAIS);
        }

        static void View()
        {
            Application.Run(new Form_Parent());
        }

        static void Start()
        {
            if (Connexion.isInfosServeur(ServeurBLL.ReturnServeur()))
            {
                Application.Run(new Form_Parent());
            }
            else
            {
                new IHM.Form_Serveur().ShowDialog();
            }
        }
    }
}
