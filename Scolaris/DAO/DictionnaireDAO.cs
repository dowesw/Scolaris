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
    class DictionnaireDAO : Dao<Dictionnaire>
    {
        public DictionnaireDAO() { Entite = new Dictionnaire(); }

        public override Dictionnaire Return(NpgsqlDataReader lect)
        {
            Dictionnaire y = new Dictionnaire();
            try
            {
                y.Id = Convert.ToInt64(lect["id"].ToString());
                y.Nom = lect["nom"].ToString();
                y.Langue = new LangueDAO().One((Int32)((lect["langue"] != null) ? (!lect["langue"].ToString().Trim().Equals("") ? lect["langue"] : 0) : 0));
                Object parent = lect["francais"];
                if (parent != null ? !parent.ToString().Trim().Equals("") : false)
                    y.Francais = One(Convert.ToInt32(parent));
            }
            catch (Exception ex)
            {
                Messages.Exception("DictionnaireDAO (Return) ", ex);
            }
            return y;
        }

        public override Dictionnaire One(int id)
        {
            Dictionnaire y = new Dictionnaire();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from " + Dictionnaire.ToTable() + " where id =" + id + ";";
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
                Messages.Exception("DictionnaireDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public Dictionnaire One(string nom, int langue, bool traduction)
        {
            Dictionnaire y = new Dictionnaire();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from " + Dictionnaire.ToTable() + " where nom ='" + nom + "' and langue = " + langue + ";";
                if (traduction)
                    query = "select y.* from " + Dictionnaire.ToTable() + " y inner join " + Dictionnaire.ToTable() + " f on y.francais = f.id where f.nom ='" + nom + "' and y.langue = " + langue + ";";
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
                Messages.Exception("DictionnaireDAO (One) ", ex);
                return y;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override Dictionnaire Insert(Dictionnaire bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into " + Dictionnaire.ToTable() + "(nom,  langue) Values('" + bean.Nom + "'," + bean.Langue.Id + ")";
                if (bean.Francais != null ? bean.Francais.Id > 0 : false)
                    query = "insert into " + Dictionnaire.ToTable() + "(nom,  langue, francais) Values('" + bean.Nom + "'," + bean.Langue.Id + ", " + bean.Francais.Id + ")";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();

                List<Dictionnaire> l = List("select * from " + Dictionnaire.ToTable() + " order by id desc limit 1");
                if (l.Count > 0)
                {
                    return l[0];
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("DictionnaireDAO (Insert) ", ex);
                return new Dictionnaire(); ;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Update(Dictionnaire bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update " + Dictionnaire.ToTable() + " set nom='" + bean.Nom + "' where id=" + bean.Id;
                if (bean.Francais != null ? bean.Francais.Id > 0 : false)
                    query = "update " + Dictionnaire.ToTable() + " set nom='" + bean.Nom + "', francais = " + bean.Francais.Id + " where id=" + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("DictionnaireDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override bool Delete(Dictionnaire bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from " + Dictionnaire.ToTable() + " where id = " + bean.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("DictionnaireDAO (Delete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public override List<Dictionnaire> List(string query)
        {
            List<Dictionnaire> list = new List<Dictionnaire>();
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
                Messages.Exception("DictionnaireDao (List) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public new List<Dictionnaire> LoadList(String query, String[] champ, Object[] val)
        {
            return base.LoadList(query, champ, val);
        }

        public new List<Dictionnaire> LoadList(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            return base.LoadList(query, champ, val, offset, limtit);
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
