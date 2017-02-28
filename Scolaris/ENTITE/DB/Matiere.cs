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
    public class Matiere : Entite
    {
        private int id;
        private string code;
        private string intitule;
        private string type;
        private GroupeMatiere groupe = new GroupeMatiere();

        public Matiere() { }

        internal GroupeMatiere Groupe
        {
            get { return groupe; }
            set { groupe = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
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
            Matiere other = (Matiere)obj;
            if (this.id != other.id)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 53 * hash + (int)(this.id ^ (this.id >> 32));
            return hash;
        }

        public static string ToTable()
        {
            return "sco_base_matiere";
        }


        public override String AddContraint(String query)
        {
            try
            {
                String mot = "groupe";
                String groupe = Utils.FindWord(query, mot);
                if (groupe != null ? groupe.Trim().Length > 0 : false)
                {
                    int idx = Utils.Occurence(groupe, '.');
                    if (idx > 1)
                    {
                        query = query.Replace("WHERE", "INNER JOIN " + GroupeMatiere.ToTable() + " grp ON " + Utils.FindNextWord(query, ToTable()) + "groupe = grp.id WHERE");
                        idx = groupe.IndexOf(mot);
                        if (idx > -1)
                        {
                            idx = (groupe.Substring(0, idx) + "" + groupe.Substring(idx, mot.Length)).Length;
                            query = query.Replace(groupe, "grp" + groupe.Substring(idx, groupe.Length - idx));
                        }
                        query = new GroupeMatiere().AddContraint(query);
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Matiere (AddContraint) ", ex);
            }
            return query;
        }
    }
}
