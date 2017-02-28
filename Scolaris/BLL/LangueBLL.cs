using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.DAO;
using Scolaris.ENTITE;

namespace Scolaris.BLL
{
    class LangueBLL : Bll<Langue>
    {
        public LangueBLL() { Dao = new LangueDAO(); }

        public override Langue One(int id)
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
        public static Langue One(string code)
        {
            try
            {
                return new LangueDAO().One(code);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override Langue Insert(Langue bean)
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

        public override bool Update(Langue bean)
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

        public override bool Delete(Langue bean)
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

        public override List<Langue> List(string query)
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
