using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.DAO;
using Scolaris.ENTITE;

namespace Scolaris.BLL
{
    public abstract class Bll<T> where T : Entite
    {
        public int offset;
        public int limit;
        public bool disPrev;
        public bool disNext;
        public Int64 totalResult;
        public int totalPage;
        public int currentPage;
        public int max = 15;

        private Dao<T> dao;
        public Dao<T> Dao
        {
            get { return dao; }
            set { dao = value;}
        }

        private List<T> result;
        private String[] champ;
        private Object[] val;
        private String nameQueriCount = "", nameQueri = "";
        private List<ParametreRequete> param = new List<ParametreRequete>();

        public List<ParametreRequete> Param
        {
            get { return param; }
            set { param = value; }
        }

        public List<T> Result
        {
            get { return result; }
            set { result = value; }
        }

        public abstract T One(int id);
        public abstract T Insert(T bean);
        public abstract bool Update(T bean);
        public abstract bool Delete(T bean);
        public abstract List<T> List(string query);

        public void AddParamToFind(ParametreRequete prq)
        {
            offset = 0;
            AddParam(prq);
        }

        public void AddParam(ParametreRequete prq)
        {
            if (prq != null)
            {
                int idx = param.FindIndex(x => x.ParamNome == prq.ParamNome);
                if (idx > -1)
                    param.RemoveAt(idx);
                if (prq.Objet != null)
                    param.Add(prq);
            }
        }

        public void Clear()
        {
            param.Clear();
        }

        public Bll<T> LoadResult(String nameQueriCount, String nameQuery, String[] param, Object[] valParam, int first, int nbLimit)
        {
            nameQueriCount = Dao.NameQuery(nameQueriCount, param, valParam);
            totalResult = (Int64)Dao<T>.LoadOneObject(nameQueriCount);
            if (first > totalResult || first < 0)
                first = 0;
            offset = first;
            int i = (int)Convert.ToDouble((double)totalResult / (double)nbLimit);
            double dec = (((double)totalResult / (double)nbLimit) - i);//partie décimle
            totalPage = (dec > 0) ? (i + 1) : i;//si la partie décimale est >0, on arrondi le nombre de page à l'unité supérieure
            int ii = (totalResult != 0) ? (int)Convert.ToDouble((double)totalPage * (double)first / (double)totalResult) : 1;
            double decc = (totalResult != 0) ? (((double)totalPage * (double)first / (double)totalResult) - ii) : 0;//partie décimle
            currentPage = (decc > 0) ? (ii + 1) : ii;
            currentPage = (currentPage == 0) ? 1 : currentPage;
            result = Dao.LoadList(nameQuery, param, valParam, first, nbLimit);
            return this;
        }

        public List<T> ExecuteDynamicQuery(String entity, String paramsOrder, bool avance, bool init)
        {
            return ExecuteDynamicQuery("y.*", "y.*", entity + " y", paramsOrder, avance, init);
        }

        public List<T> ExecuteDynamicQuery(String field, String fieldCount, String entity, String paramsOrder, bool avance, bool init)
        {
            nameQueriCount = (param.Count > 0) ? BuildDynamicQuery(param, "SELECT COUNT(" + fieldCount + ") FROM " + entity + " WHERE") : BuildDynamicQuery(param, "SELECT COUNT(" + fieldCount + ") FROM " + entity);
            nameQueri = (param.Count > 0) ? BuildDynamicQuery(param, "SELECT " + field + " FROM " + entity + " WHERE") : BuildDynamicQuery(param, "SELECT " + field + " FROM " + entity);
            //Ajouter les critère de tri
            if (paramsOrder != null ? paramsOrder.Trim().Length > 0 : false)
            {
                nameQueri = nameQueri + " ORDER BY " + paramsOrder;
            }
            return pagineResult(avance, init, max);
        }

        public List<T> pagineResult(bool avancer, bool init, int limit)
        {
            if (avancer)
                offset = ((init) ? 0 : (offset + limit));
            else
                offset = ((init) ? 0 : (offset - limit));
            if (offset < 0)
                offset = 0;

            LoadResult(nameQueriCount, nameQueri, champ, val, offset, limit);

            totalPage = totalPage > 0 ? totalPage : 1;
            if ((totalPage < 2) || (offset + limit) >= this.totalResult)
                disNext = true;
            else
                disNext = false;
            if (this.currentPage > 1)
                disPrev = false;
            else
                disPrev = true;
            if (offset < 0)
                limit = 0;
            return this.result;
        }

        //reconstruit la  requête en y appliquant la chaine de paramètres dynamiques  préalablement construite au niveau des formulaires
        private String BuildDynamicQuery(List<ParametreRequete> param, String basicQuery)
        {
            BuildDinamycParam(param);
            return basicQuery + " " + BuildRequete(param);
        }

        //rempli les paramètres constrits sous forme de liste d'objets dans les tableaux champs / valeur accepté par notre exécuteur de requête
        private void BuildDinamycParam(List<ParametreRequete> param)
        {
            List<ParametreRequete> lp = new List<ParametreRequete>();
            foreach (ParametreRequete p in param)
                AjouteParam(p, lp);
            int i = 0;
            champ = new String[lp.Count];
            val = new Object[lp.Count];
            foreach (ParametreRequete p in lp)
            {
                champ[i] = p.ParamNome;
                val[i] = p.Objet;
                i++;
            }
        }

        private List<ParametreRequete> AjouteParam(ParametreRequete p, List<ParametreRequete> lp)
        {
            if (p.Attribut != null && !(p.Operation.Trim().Equals("IS NOT NULL") || p.Operation.Trim().Equals("IS NULL")))
            {
                if (!p.Operation.Trim().Equals("BETWEEN"))
                    lp.Add(p);
                else
                {
                    lp.Add(p);
                    p = new ParametreRequete(p.Attribut, p.ParamNome + "1", p.OtherObjet);
                    lp.Add(p);
                }
            }
            if (p.OtherExpression.Count > 0)
            {
                foreach (ParametreRequete pp in p.OtherExpression)
                    lp.AddRange(AjouteParam(pp, lp));
            }
            return lp;
        }

        /*Suite de méhode qui permettent de construire la chaine de paramètre sous forme de String*/
        private String BuildRequete(List<ParametreRequete> lp)
        {
            String re = "";
            foreach (ParametreRequete p in lp)
            {
                int i = lp.FindIndex(x => x.ParamNome == p.ParamNome);
                if (i != (lp.Count - 1))
                    p.LastParam = false;
                else
                    p.LastParam = true;
                re += DecomposeRequete(p) + "" + ((p.LastParam) ? "" : " " + p.Predicat) + " ";
            }
            return re;
        }

        private String DecomposeRequete(ParametreRequete p)
        {
            String re = "";
            if (p.Attribut != null)
                re += ConcateneParam(p);
            if (p.OtherExpression.Count > 0)
            {
                p.OtherExpression[p.OtherExpression.Count - 1].LastParam = true;
                if (p.Attribut != null)
                    re += " " + p.Predicat + " (";
                else
                    re += "(";
                foreach (ParametreRequete p1 in p.OtherExpression)
                    re += DecomposeRequete(p1) + "" + ((p1.LastParam) ? "" : " " + p1.Predicat) + " ";
                re += ")";
            }
            return re;
        }
        /**/
        private String ConcateneParam(ParametreRequete p)
        {
            String re = "";
            if (p.Operation.Trim().Equals("BETWEEN"))
                re += "(" + p.Attribut + " " + p.Operation + " :" + p.ParamNome + " AND :" + p.ParamNome + "" + 1 + ")";
            else
                re += "" + p.Attribut + " " + p.Operation + ((p.Operation.Trim().Equals("IS NOT NULL") || p.Operation.Trim().Equals("IS NULL")) ? "" : " :" + p.ParamNome);
            return re;
        }
    }

    public class ParametreRequete
    {
        //indique l'attribut sur lequel on veux filtrer
        private String attribut;
        //indique le paramètre nomé
        private String paramNome;
        //valeur à comparer
        private Object objet;
        private Object otherObjet;
        //AND ou OR
        private String predicat;
        //=, BETWEEN, != IN
        private String operation;
        private bool clauseOr;
        // 1-Sur l'attribue  2-Sur la valeur 3-Sur une suite de valeur
        private int typeClauseOr;
        private List<ParametreRequete> otherExpression;
        private bool lastParam = false;

        public ParametreRequete()
        {
            otherExpression = new List<ParametreRequete>();
        }

        public ParametreRequete(String fieldParam)
        {
            this.paramNome = fieldParam;
            otherExpression = new List<ParametreRequete>();
        }

        public ParametreRequete(String attribut, String fielParam, Object valeur)
        {
            this.attribut = attribut;
            this.objet = valeur;
            this.paramNome = fielParam;
            otherExpression = new List<ParametreRequete>();
        }

        public ParametreRequete(String attribut, String fielParam, Object valeur, String operation, String predicat)
        {
            this.attribut = attribut;
            this.objet = valeur;
            this.paramNome = "_"+fielParam;
            this.operation = operation;
            this.predicat = predicat;
            otherExpression = new List<ParametreRequete>();
        }

        public ParametreRequete(String attribut, String fielParam, Object valeur, Object otherObjet, String operation, String predicat)
        {
            this.attribut = attribut;
            this.paramNome = fielParam;
            this.objet = valeur;
            this.otherObjet = otherObjet;
            this.predicat = predicat;
            this.operation = operation;
            otherExpression = new List<ParametreRequete>();
        }

        public String Attribut
        {
            get { return attribut; }
            set { attribut = value; }
        }

        public String ParamNome
        {
            get { return paramNome; }
            set { paramNome = value; }
        }

        public Object Objet
        {
            get { return objet; }
            set { objet = value; }
        }

        public Object OtherObjet
        {
            get { return otherObjet; }
            set { otherObjet = value; }
        }

        public String Predicat
        {
            get { return predicat; }
            set { predicat = value; }
        }


        public String Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        public bool ClauseOr
        {
            get { return clauseOr; }
            set { clauseOr = value; }
        }

        public int TypeClauseOr
        {
            get { return typeClauseOr; }
            set { typeClauseOr = value; }
        }

        public List<ParametreRequete> OtherExpression
        {
            get { return otherExpression; }
            set { otherExpression = value; }
        }

        public bool LastParam
        {
            get { return lastParam; }
            set { lastParam = value; }
        }
    }
}
