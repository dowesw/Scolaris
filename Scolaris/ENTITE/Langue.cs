using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

using Scolaris.TOOLS;

namespace Scolaris.ENTITE
{
    [Serializable]
    public class Langue : Entite
    {
        private int id;
        private string code;
        private string intitule;
        private bool actif;

        public Langue() { }
        public Langue(int id) { this.id = id; }

        public bool Actif
        {
            get { return actif; }
            set { actif = value; }
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

        public string Reference
        {
            get { return "[" + code + "] " + intitule; }
            set { }
        }

        public Bitmap Icon
        {
            get
            {
                string file = Chemins.CheminImage() + Code + ".PNG";
                if (!File.Exists(file))
                    file = Chemins.CheminImage() + "GLO.PNG";
                return new Bitmap(Image.FromFile(file), new Size(16,16));
            }
            set { }
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
            Langue other = (Langue)obj;
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
            return "sco_base_langue";
        }
        public override String AddContraint(String query)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Messages.Exception("Langue (AddContraint) ", ex);
            }
            return query;
        }
    }
}
