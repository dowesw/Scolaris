using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Scolaris.TOOLS;
using Scolaris.ENTITE;

namespace Scolaris.ENTITE.DB
{
    [Serializable]
    public class GroupeMatiere : Entite
    {
        private int id;
        private string code;
        private string intitule;
        private int position;
        private Langue langue = new Langue();

        public GroupeMatiere() { }

        public GroupeMatiere(int id) { this.id = id; }

        public Langue Langue
        {
            get { return langue; }
            set { langue = value; }
        }

        public string Intitule
        {
            get { return intitule; }
            set { intitule = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Position
        {
            get { return position > 0 ? position : 1; }
            set { position = value; }
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 53 * hash + (int)(this.id ^ (this.id >> 32));
            return hash;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            GroupeMatiere other = (GroupeMatiere)obj;
            if (this.id != other.id)
            {
                return false;
            }
            return true;
        }

        public static string ToTable()
        {
            return "sco_base_groupe_matiere";
        }

        public override String AddContraint(String query)
        {
            try
            {
                String mot = "langue";
                String langue = Utils.FindWord(query, mot);
                if (langue != null ? langue.Trim().Length > 0 : false)
                {
                    int idx = Utils.Occurence(langue, '.');
                    if (idx > 1)
                    {
                        query = query.Replace("WHERE", "INNER JOIN " + Langue.ToTable() + " lang ON " + Utils.FindNextWord(query, ToTable()) + "langue = lang.id WHERE");
                        idx = langue.IndexOf(mot);
                        if (idx > -1)
                        {
                            idx = (langue.Substring(0, idx) + "" + langue.Substring(idx, mot.Length)).Length;
                            query = query.Replace(langue, "lang" + langue.Substring(idx, langue.Length - idx));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("GroupeMatiere (AddContraint) ", ex);
            }
            return query;
        }
    }
}
