using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.ENTITE.DB;
using Scolaris.DAO.DB;
using Scolaris.DAO;

namespace Scolaris.BLL.DB
{
    class GroupeMatiereBLL : Bll<GroupeMatiere>
    {
        public GroupeMatiereBLL() { Dao = new GroupeMatiereDAO(); }

        public override GroupeMatiere One(int id)
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

        public override GroupeMatiere Insert(GroupeMatiere bean)
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

        public override bool Update(GroupeMatiere bean)
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

        public override bool Delete(GroupeMatiere bean)
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

        public override List<GroupeMatiere> List(string query)
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
