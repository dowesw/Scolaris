using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Npgsql;

using Scolaris.TOOLS;
using Scolaris.BLL;

namespace Scolaris.TOOLS
{
    public class Connexion
    {
        NpgsqlConnection INSTANCE = null;

        public NpgsqlConnection Connection()
        {
            if (INSTANCE != null)
            {
                if (INSTANCE.State == System.Data.ConnectionState.Closed)
                {
                    INSTANCE.Open();
                }
            }
            else
            {
                ENTITE.Serveur bean = ServeurBLL.ReturnServeur();
                if ((bean != null) ? bean.Port != 0 : false)
                {
                    INSTANCE = returnConnexion(bean);
                }
            }
            return INSTANCE;
        }

        public static void Close(NpgsqlConnection con)
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
            }
            con = null;
        }

        public NpgsqlConnection onConnection()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection();
                string constr = "PORT=5432;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=scolaris_erp;HOST=127.0.0.1;PASSWORD=yves1910/;USER ID=postgres";
                con = new NpgsqlConnection(constr);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (onConnection) ", ex);
                return null;
            }
        }

        public NpgsqlConnection returnConnexion(ENTITE.Serveur bean)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection();
                if (isConnection(out con, bean))
                {
                    return con;
                }
                else
                {
                    if (DialogResult.Retry == Messages.ErrorRetryCancel("Connexion impossible !Entrer de nouveaux parametres"))
                    {
                        new IHM.Form_Serveur().ShowDialog();
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (returnConnexion) ", ex);
                return null;
            }
        }

        public bool isConnection(out NpgsqlConnection con, ENTITE.Serveur bean)
        {
            con = null;
            try
            {
                if (bean != null)
                {
                    string constr = "PORT=" + bean.Port + ";TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=" + bean.Database + ";HOST=" + bean.Adresse + ";PASSWORD=" + bean.Password + ";USER ID=" + bean.User + "";
                    con = new NpgsqlConnection(constr);
                    try
                    {
                        con.Open();
                        return true;
                    }
                    catch (System.StackOverflowException ex)
                    {
                        return isConnection(out con, bean);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool isInfosServeur(ENTITE.Serveur bean)
        {
            try
            {
                if (bean != null)
                {
                    string constr = "PORT=" + bean.Port + ";TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=" + bean.Database + ";HOST=" + bean.Adresse + ";PASSWORD=" + bean.Password + ";USER ID=" + bean.User + "";
                    NpgsqlConnection con = new NpgsqlConnection(constr);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (isInfosServeur) ", ex);
                return false;
            }
        }
    }
}
