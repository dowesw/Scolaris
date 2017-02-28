using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scolaris.BLL;
using Scolaris.TOOLS;

namespace Scolaris.ENTITE
{
    class Dictionnaire : Entite
    {
        private Int64 id;
        private string nom;
        private Langue langue = new Langue();
        private Dictionnaire francais;

        public Dictionnaire() { }
        public Dictionnaire(Int64 id) { this.id = id; }

        internal Dictionnaire Francais
        {
            get { return francais; }
            set { francais = value; }
        }

        public Langue Langue
        {
            get { return langue; }
            set { langue = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }

        public static string ToTable()
        {
            return "sco_base_dictionnaire";
        }


        public override String AddContraint(String query)
        {
            try
            {
                String mot = "francais";
                String francais = Utils.FindWord(query, mot);
                if (francais != null ? francais.Trim().Length > 0 : false)
                {
                    int idx = Utils.Occurence(francais, '.');
                    if (idx > 1)
                    {
                        query = query.Replace("WHERE", "INNER JOIN " + Dictionnaire.ToTable() + " fr ON " + Utils.FindNextWord(query, ToTable()) + "francais = fr.id WHERE");
                        idx = francais.IndexOf(mot);
                        if (idx > -1)
                        {
                            idx = (francais.Substring(0, idx) + "" + francais.Substring(idx, mot.Length)).Length;
                            query = query.Replace(francais, "fr" + francais.Substring(idx, francais.Length - idx));
                        }
                    }
                }

                mot = "langue";
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
                Messages.Exception("Dictionnaire (AddContraint) ", ex);
            }
            return query;
        }

        public static void Traduction(Control parent)
        {
            Dictionnaire y;
            if (parent.GetType().ToString() == "System.Windows.Forms.Form")
            {
                y = DictionnaireBLL.One(parent.Text);
                if (y != null ? y.id > 0 : false)
                    parent.Text = y.Nom;
            }
            foreach (Control crtl in parent.Controls)
            {
                if ((crtl.GetType().ToString() == "System.Windows.Forms.Label") ||
                         (crtl.GetType().ToString() == "System.Windows.Forms.CheckBox") ||
                            (crtl.GetType().ToString() == "System.Windows.Forms.RadioButton") ||
                                (crtl.GetType().ToString() == "System.Windows.Forms.Button"))
                {
                    y = DictionnaireBLL.One(crtl.Text);
                    if (y != null ? y.id > 0 : false)
                        crtl.Text = y.Nom;
                }
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.GroupBox") ||
                            (crtl.GetType().ToString() == "System.Windows.Forms.TabPage"))
                {
                    y = DictionnaireBLL.One(crtl.Text);
                    if (y != null ? y.id > 0 : false)
                        crtl.Text = y.Nom;
                    Traduction(crtl);
                }
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.Panel") ||
                            (crtl.GetType().ToString() == "System.Windows.Forms.TabControl") ||
                                (crtl.GetType().ToString() == "System.Windows.Forms.DataGridView"))
                {
                    Traduction(crtl);
                }
            }
            if (parent.GetType().ToString() == "System.Windows.Forms.DataGridView")
            {
                foreach (DataGridViewColumn crtl in ((DataGridView)parent).Columns)
                {
                    y = DictionnaireBLL.One(crtl.HeaderText);
                    if (y != null ? y.id > 0 : false)
                        crtl.HeaderText = y.Nom;
                }
            }
        }
    }
}
