using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scolaris.BLL;
using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris.IHM
{
    public partial class Form_Dictionnaire : Form
    {
        DictionnaireBLL dao = new DictionnaireBLL();
        Dictionnaire entity = new Dictionnaire();
        Dictionnaire select = new Dictionnaire();
        ObjectThread objet_dico;

        List<Dictionnaire> traductions = new List<Dictionnaire>();

        public Form_Dictionnaire()
        {
            InitializeComponent();
            Configuration.Load(this);
            Dictionnaire.Traduction(this);
            objet_dico = new ObjectThread(dgv_dico);
        }

        private void LoadLangue()
        {
            List<Langue> langues = new LangueBLL().List("select * from " + Langue.ToTable());
            com_langue.DisplayMember = "Reference";
            com_langue.ValueMember = "Id";
            com_langue.DataSource = new BindingSource(langues, null);

            foreach (Langue l in langues)
            {
                com_langue.AutoCompleteCustomSource.Add(l.Reference);
            }
            com_langue.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            com_langue.AutoCompleteSource = AutoCompleteSource.CustomSource;

            com_langue_search.Items.Clear();
            com_langue_search.Items.Add("---");
            foreach (Langue l in langues)
            {
                com_langue_search.Items.Add(l.Reference);
                com_langue_search.AutoCompleteCustomSource.Add(l.Reference);
            }
            com_langue_search.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            com_langue_search.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void LoadTraduction()
        {
            List<Dictionnaire> traductions = new DictionnaireBLL().List("select * from " + Dictionnaire.ToTable() + " where francais is null order by nom");
            com_traduction.DisplayMember = "Nom";
            com_traduction.ValueMember = "Id";
            com_traduction.DataSource = new BindingSource(traductions, null);

            foreach (Dictionnaire l in traductions)
            {
                com_traduction.AutoCompleteCustomSource.Add(l.Nom);
            }
            com_traduction.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            com_traduction.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void LoadAll(bool avance, bool init)
        {
            dao.ExecuteDynamicQuery(Dictionnaire.ToTable(), "y.langue asc , y.nom", avance, init);

            objet_dico.ClearDataGridView(true);
            foreach (Dictionnaire g in dao.Result)
            {
                Action(g, 1, true);
            }

            btn_prev.Enabled = !dao.disPrev;
            btn_next.Enabled = !dao.disNext;
            lb_position.Text = dao.currentPage + "/" + dao.totalPage;
        }

        private void Action(Dictionnaire y, int action, bool load)// action : 1 pour sauvegarder - 2 pour modifier - 3 pour supprimer
        {
            if (y != null ? y.Id > 0 : false)
            {
                int idx = Utils.GetRowData(dgv_dico, y.Id);
                object[] row = new object[] { y.Id, y.Nom, y.Langue.Code, (y.Francais != null ? y.Francais.Nom : "") };
                switch (action)
                {
                    case 1:
                        objet_dico.WriteDataGridView(row);
                        if (dgv_dico.Rows.Count > dao.max)
                        {
                            objet_dico.RemoveDataGridView(dgv_dico.Rows.Count - 1);
                        }
                        if (!load)
                            dao.Result.Add(y);
                        break;
                    case 2:
                        if (idx > -1)
                        {
                            objet_dico.RemoveDataGridView(idx);
                            objet_dico.WriteDataGridView(idx, row);
                        }
                        if (!load)
                        {
                            idx = dao.Result.FindIndex(x => y.Id == x.Id);
                            if (idx > -1)
                                dao.Result[idx] = y;
                        }
                        break;
                    default:
                        if (idx > -1)
                        {
                            objet_dico.RemoveDataGridView(idx);
                        }
                        if (!load)
                            dao.Result.Remove(y);
                        break;
                }
            }
        }

        private void ResetData()
        {
            for (int i = 0; i < dgv_dico.Rows.Count; i++)
            {
                dgv_dico.Rows[i].Selected = false;
            }
        }

        private void ResetView()
        {
            txt_nom.ResetText();
            com_langue.ResetText();
            com_traduction.ResetText();
            com_traduction.Enabled = false;

            select = new Dictionnaire();
            entity = new Dictionnaire();
        }

        private void LoadOnView(Dictionnaire y)
        {
            txt_nom.Text = y.Nom;
            int idx = com_langue.FindStringExact(y.Langue.Reference, 0);
            if (idx > -1)
                com_langue.SelectedIndex = idx;
            idx = com_traduction.FindStringExact(y.Francais.Nom, 0);
            if (idx > -1)
                com_traduction.SelectedIndex = idx;
            entity = y;
        }

        private Dictionnaire RecopieView()
        {
            Dictionnaire y = new Dictionnaire(entity.Id);
            y.Nom = txt_nom.Text.Trim();
            y.Langue = entity.Langue;
            y.Francais = entity.Francais;
            return y;
        }

        private bool ControleView(Dictionnaire y)
        {
            if (y.Nom != null ? y.Nom.Trim().Length < 1 : true)
            {
                Messages.Error("Vous devez specifier le mot");
                return false;
            }
            if (y.Langue != null ? y.Langue.Id < 1 : true)
            {
                Messages.Error("Vous devez specifier la langue");
                return false;
            }
            if (!y.Langue.Code.Equals(Constantes.LANGUE_FRANCAIS))
            {
                if (y.Francais != null ? y.Francais.Id < 1 : true)
                {
                    Messages.Error("Vous devez specifier la traduction en francais");
                    return false;
                }
            }
            Dictionnaire d = DictionnaireBLL.One(y.Nom, y.Langue.Id);
            if (d != null ? (!d.Id.Equals(y.Id)) : false)
            {
                Messages.Error("Vous avez deja créer ce mot");
                return false;
            }
            return true;
        }

        private void Form_Dictionnaire_Load(object sender, EventArgs e)
        {
            LoadLangue();
            LoadTraduction();
            LoadAll(true, true);
            ResetView();
        }

        private void dgv_groupe_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_dico.HitTest(e.X, e.Y); //get info
            int pos = dgv_dico.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_dico.Rows[pos].Cells["id"].Value != null)
                {
                    Int64 id = (Int64)dgv_dico.Rows[pos].Cells["id"].Value;
                    if (id > 0)
                    {
                        Dictionnaire y = dao.Result.Find(x => x.Id == id);
                        if (y != null ? y.Id > 0 : false)
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        select = y;
                                        ResetData();
                                        dgv_dico.Rows[pos].Selected = true; //Select the row
                                    }
                                    break;
                                default:
                                    LoadOnView(y);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            LoadAll(true, false);
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            LoadAll(false, false);
        }

        private void com_rows_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = com_rows.SelectedIndex;
            if (idx > -1)
            {
                string row = com_rows.Items[idx].ToString();
                dao.max = Convert.ToInt16(row);
                LoadAll(true, true);
            }
        }

        private void com_langue_SelectedIndexChanged(object sender, EventArgs e)
        {
            Langue a = com_langue.SelectedItem as Langue;
            entity.Langue = a;
            com_traduction.Enabled = !a.Code.Equals(Constantes.LANGUE_FRANCAIS);
            com_traduction.ResetText();
            entity.Francais = new Dictionnaire();
        }

        private void com_traduction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionnaire a = com_traduction.SelectedItem as Dictionnaire;
            entity.Francais = a;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string action = entity.Id > 0 ? "Modification" : "Insertion";
            try
            {
                Dictionnaire y = RecopieView();
                if (ControleView(y))
                {
                    if (y.Id > 0)
                    {
                        dao.Update(y);
                        Action(y, 2, false);
                    }
                    else
                    {
                        y = dao.Insert(y);
                        entity.Id = y.Id;
                        Action(y, 1, false);
                    }
                    ResetView();
                    Messages.Succes();
                }
            }
            catch (Exception ex)
            {
                Messages.Error(action + " impossible!");
                Logs.Exception("Form_Dictionnaire (btn_save_Click)", ex);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (select != null ? select.Id > 0 : false)
            {
                if (DialogResult.Yes == Messages.Confirmation("supprimer"))
                {
                    if (dao.Delete(select))
                    {
                        Action(select, 3, false);
                        if (entity.Id == select.Id)
                            ResetView();
                        Messages.Succes();
                    }
                }
            }
        }

        private void txt_num_search_TextChanged(object sender, EventArgs e)
        {
            ParametreRequete p = new ParametreRequete("y.code", "code", null, "LIKE", "AND");
            String num = txt_num_search.Text;
            if (num != null ? num.Trim().Length > 0 : false)
            {
                p = new ParametreRequete(null, "code", num.ToUpper() + "%", "LIKE", "AND");
                p.OtherExpression.Add(new ParametreRequete("UPPER(y.nom)", "nom", num.ToUpper() + "%", "LIKE", "OR"));
                p.OtherExpression.Add(new ParametreRequete("UPPER(y.francais.nom)", "nom", num.ToUpper() + "%", "LIKE", "OR"));
            }
            dao.AddParamToFind(p);
            LoadAll(true, true);
        }

        private void com_langue_search_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = com_langue.FindStringExact(com_langue_search.Items[com_langue_search.SelectedIndex].ToString(), 0);
            ParametreRequete p = new ParametreRequete("y.langue", "langue", null, "=", "AND");
            if (idx > -1)
            {
                Langue l = com_langue.Items[idx] as Langue;
                p = new ParametreRequete("y.langue", "langue", l.Id, "=", "AND");
            }
            dao.AddParamToFind(p);
            LoadAll(true, true);
        }

        private void Form_Dictionnaire_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_DICTIONNAIRE = null;
        }
    }
}
