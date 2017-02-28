using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NpgsqlTypes;
using Npgsql;

using Scolaris.BLL;
using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris.DAO
{
    public abstract class Dao<T> where T : Entite
    {

        public abstract T Return(NpgsqlDataReader lect);
        public abstract T One(int id);
        public abstract T Insert(T bean);
        public abstract bool Update(T bean);
        public abstract bool Delete(T bean);
        public abstract List<T> List(string query);

        private Entite entite;
        public Entite Entite
        {
            get { return entite; }
            set { entite = value; }
        }

        public static bool RequeteLibre(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("DAO (RequeteLibre) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static object LoadOneObject(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            return lect[0];
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Messages.Exception("DAO (LoadOneObject) ", ex);
                return null;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<string> LoadListObject(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                List<string> list = new List<string>();
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            list.Add(lect[0].ToString());
                        }
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("DAO (LoadListObject) ", ex);
                return null;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public Object LoadOneObject(String query, String[] champ, Object[] val)
        {
            return LoadOneObject(NameQuery(query, champ, val));
        }

        public List<string> LoadListObject(String query, String[] champ, Object[] val)
        {
            return LoadListObject(NameQuery(query, champ, val));
        }

        public List<T> LoadList(String query, String[] champ, Object[] val)
        {
            return List(NameQuery(query, champ, val));
        }

        public List<T> LoadList(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            return List(NameQuery(query, champ, val, offset, limtit));
        }

        public String NameQuery(String query, String[] champ, Object[] val)
        {
            for (int i = 0; i < champ.Length; i++)
            {
                Object v = val[i];
                String c = ":" + champ[i];
                query = query.Replace(c, Utils.IsNumeric(v) ? v.ToString() : "'" + v + "'");
            }
            return entite.AddContraint(query);
        }

        public String NameQuery(String query, String[] champ, Object[] val, int offset, int limtit)
        {
            query = NameQuery(query, champ, val) + " offset " + offset + " limit " + limtit;
            return query;
        }
    }
}
