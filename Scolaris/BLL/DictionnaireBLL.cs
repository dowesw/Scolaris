using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.DAO;
using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris.BLL
{
    class DictionnaireBLL : Bll<Dictionnaire>
    {
        public DictionnaireBLL() { Dao = new DictionnaireDAO(); }

        public override Dictionnaire One(int id)
        {
            try
            {
                return Dao.One(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Dictionnaire One(string nom, int langue)
        {
            try
            {
                return new DictionnaireDAO().One(nom, langue, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Dictionnaire One(string nom)
        {
            try
            {
                return new DictionnaireDAO().One(nom, Constantes.LANGUE.Id, !Constantes.LANGUE.Code.Equals(Constantes.LANGUE_FRANCAIS));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override Dictionnaire Insert(Dictionnaire bean)
        {
            try
            {
                return Dao.Insert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Update(Dictionnaire bean)
        {
            try
            {
                return Dao.Update(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override bool Delete(Dictionnaire bean)
        {
            try
            {
                return Dao.Delete(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public override List<Dictionnaire> List(string query)
        {
            try
            {
                return Dao.List(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
