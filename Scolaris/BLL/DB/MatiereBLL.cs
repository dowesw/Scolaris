using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.ENTITE.DB;
using Scolaris.DAO.DB;

namespace Scolaris.BLL.DB
{
    class MatiereBLL : Bll<Matiere>
    {
        public MatiereBLL() { Dao = new MatiereDAO(); }

        public override Matiere One(int id)
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

        public override Matiere Insert(Matiere bean)
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

        public override bool Update(Matiere bean)
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

        public override bool Delete(Matiere bean)
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

        public override List<Matiere> List(string query)
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
