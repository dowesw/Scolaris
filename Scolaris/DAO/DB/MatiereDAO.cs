using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;
using Npgsql;

using Scolaris.ENTITE.DB;
using Scolaris.TOOLS;

namespace Scolaris.DAO.DB
{
    class MatiereDAO : Dao<Matiere>
    {
        public MatiereDAO() { Entite = new Matiere(); }

        public override Matiere Return(NpgsqlDataReader lect)
        {
            Matiere y = new Matiere();
            y.Id = Convert.ToInt32(lect["id"].ToString());
            y.Code = lect["code"].ToString();
            y.Intitule = lect["intitule"].ToString();
            y.Type = lect["type"].ToString();
            y.Groupe = new GroupeMatiereDAO().One((Int32)((lect["groupe"] != null) ? (!lect["groupe"].ToString().Trim().Equals("") ? lect["groupe"] : 0) : 0));
            return y;
        }

        public override Matiere One(int id)
        {
            Matiere y = new Matiere();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from "+Matiere.ToTable()+" where id =" + id;
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
                Messages.Exception("MatiereDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override Matiere Insert(Matiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into " + Matiere.ToTable() + "(code, intitule, type, groupe) Values('" + bean.Code + "','" + bean.Intitule + "','" + bean.Type + "'," + bean.Groupe.Id + ")";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();

                List<Matiere> l = List("select * from " + Matiere.ToTable() + " order by id desc limit 1");
                if (l.Count > 0)
                {
                    return l[0];
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("MatiereDAO (Insert) ", ex);
                return new Matiere(); ;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Update(Matiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update " + Matiere.ToTable() + " set code='" + bean.Code + "', intitule='" + bean.Intitule + "', type='" + bean.Type + "', groupe=" + bean.Groupe.Id + " where id=" + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("MatiereDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Delete(Matiere bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from " + Matiere.ToTable() + " where id = " + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("MatiereDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override List<Matiere> List(string query)
        {
            List<Matiere> list = new List<Matiere>();
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
                Messages.Exception("MatiereDAO (List) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public String NameQuery(String query, String[] champ, Object[] val)
        {
            base.NameQuery(query, champ, val);
            query = new Matiere().AddContraint(query);
            return query;
        }

        public String NameQuery(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            base.NameQuery(query, champ, val, offset, limtit);
            query = new Matiere().AddContraint(query);
            return query;
        }
    }
}
