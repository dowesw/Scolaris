using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.DAO;
using Scolaris.ENTITE;

namespace Scolaris.BLL
{
    class ServeurBLL
    {
        public static bool CreateServeur(Serveur bean)
        {
            try
            {
                return ServeurDAO.CreateServeur(bean);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Serveur ReturnServeur()
        {
            try
            {
                return ServeurDAO.ReturnServeur();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Lecture de fichier", ex);
            }

        }
    }
}
