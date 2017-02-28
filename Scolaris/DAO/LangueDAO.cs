using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;
using Npgsql;

using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris.DAO
{
    class LangueDAO : Dao<Langue>
    {
        public LangueDAO() { Entite = new Langue(); }

        public override Langue Return(NpgsqlDataReader lect)
        {
            Langue y = new Langue();
            y.Id = Convert.ToInt32(lect["id"].ToString());
            y.Code = lect["code"].ToString();
            y.Intitule = lect["intitule"].ToString();
            y.Actif = (Boolean)((lect["actif"] != null) ? (!lect["actif"].ToString().Trim().Equals("") ? lect["actif"] : false) : false);
            return y;
        }

        public override Langue One(int id)
        {
            Langue y = new Langue();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from " + Langue.ToTable() + " where id =" + id + ";";
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
                Messages.Exception("LangueDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public Langue One(string code)
        {
            Langue y = new Langue();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from " + Langue.ToTable() + " where code ='" + code + "';";
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
                Messages.Exception("LangueDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override Langue Insert(Langue bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into " + Langue.ToTable() + "(code, intitule, actif) Values('" + bean.Code + "','" + bean.Intitule + "','" + bean.Actif + "')";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();

                List<Langue> l = List("select * from " + Langue.ToTable() + " order by id desc limit 1");
                if (l.Count > 0)
                {
                    return l[0];
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("LangueDAO (Insert) ", ex);
                return new Langue(); ;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Update(Langue bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update " + Langue.ToTable() + " set code ='" + bean.Code + "', intitule = '"+bean.Intitule+"', actif = '"+bean.Actif+"' where id=" + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("LangueDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Delete(Langue bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from " + Langue.ToTable() + " where id = " + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("LangueDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override List<Langue> List(string query)
        {
            List<Langue> list = new List<Langue>();
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
                Messages.Exception("LangueDao (List) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public new String NameQuery(String query, String[] champ, Object[] val)
        {
            base.NameQuery(query, champ, val);
            query = new Dictionnaire().AddContraint(query);
            return query;
        }

        public new String NameQuery(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            base.NameQuery(query, champ, val, offset, limtit);
            query = new Dictionnaire().AddContraint(query);
            return query;
        }
    }
}
