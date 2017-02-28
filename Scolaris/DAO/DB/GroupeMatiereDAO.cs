using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;
using Npgsql;

using Scolaris.BLL;
using Scolaris.ENTITE.DB;
using Scolaris.TOOLS;

namespace Scolaris.DAO.DB
{
    class GroupeMatiereDAO : Dao<GroupeMatiere>
    {
        public GroupeMatiereDAO() { Entite = new GroupeMatiere(); }

        public override GroupeMatiere Return(NpgsqlDataReader lect)
        {
            GroupeMatiere y = new GroupeMatiere();
            try
            {
                y.Id = Convert.ToInt32(lect["id"].ToString());
                y.Code = lect["code"].ToString();
                y.Intitule = lect["intitule"].ToString();
                y.Position = (Int32)((lect["position"] != null) ? (!lect["position"].ToString().Trim().Equals("") ? lect["position"] : 0) : 0);
                y.Langue = new LangueDAO().One((Int32)((lect["langue"] != null) ? (!lect["langue"].ToString().Trim().Equals("") ? lect["langue"] : 0) : 0));
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (Return) ", ex);
            }
            return y;
        }

        public override GroupeMatiere One(int id)
        {
            GroupeMatiere y = new GroupeMatiere();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from " + GroupeMatiere.ToTable() + " where id =" + id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        y = Return(lect);
                    }
                }
                return y;
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override GroupeMatiere Insert(GroupeMatiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into " + GroupeMatiere.ToTable() + "(code, intitule, langue, position) Values('" + bean.Code + "','" + bean.Intitule + "'," + Constantes.LANGUE.Id + "," + bean.Position + ")";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();

                List<GroupeMatiere> l = List("select * from " + GroupeMatiere.ToTable() + " order by id desc limit 1");
                if (l.Count > 0)
                {
                    return l[0];
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (Insert) ", ex);
                return new GroupeMatiere(); ;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Update(GroupeMatiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update " + GroupeMatiere.ToTable() + " set code='" + bean.Code + "', intitule='" + bean.Intitule + "', position=" + bean.Position + " where id=" + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Delete(GroupeMatiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from " + GroupeMatiere.ToTable() + " where id = " + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override List<GroupeMatiere> List(string query)
        {
            List<GroupeMatiere> list = new List<GroupeMatiere>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        list.Add(Return(lect));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiereDAO (List) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }


        public new List<GroupeMatiere> LoadList(String query, String[] champ, Object[] val)
        {
            return base.LoadList(query, champ, val);
        }

        public new List<GroupeMatiere> LoadList(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            return base.LoadList(query, champ, val, offset, limtit);
        }

        public new String NameQuery(String query, String[] champ, Object[] val)
        {
            base.NameQuery(query, champ, val);
            query = new GroupeMatiere().AddContraint(query);
            return query;
        }

        public new String NameQuery(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            base.NameQuery(query, champ, val, offset, limtit);
            query = new GroupeMatiere().AddContraint(query);
            return query;
        }
    }
}
