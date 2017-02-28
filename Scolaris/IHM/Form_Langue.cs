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
    public partial class Form_Langue : Form
    {
        LangueBLL dao = new LangueBLL();
        Langue entity = new Langue();
        Langue select = new Langue();
        ObjectThread objet_langue;

        List<Langue> traductions = new List<Langue>();

        public Form_Langue()
        {
            InitializeComponent();
            Configuration.Load(this);
            Dictionnaire.Traduction(this);
            objet_langue = new ObjectThread(dgv_langue);
        }

        private void LoadAll(bool avance, bool init)
        {
            dao.ExecuteDynamicQuery(Langue.ToTable(), "y.code , y.intitule", avance, init);

            objet_langue.ClearDataGridView(true);
            foreach (Langue g in dao.Result)
            {
                Action(g, 1, true);
            }

            btn_prev.Enabled = !dao.disPrev;
            btn_next.Enabled = !dao.disNext;
            lb_position.Text = dao.currentPage + "/" + dao.totalPage;
        }

        private void Action(Langue y, int action, bool load)// action : 1 pour sauvegarder - 2 pour modifier - 3 pour supprimer
        {
            if (y != null ? y.Id > 0 : false)
            {
                int idx = Utils.GetRowData(dgv_langue, y.Id);
                object[] row = new object[] { y.Id, y.Icon, y.Code, y.Intitule, y.Actif };
                switch (action)
                {
                    case 1:
                        objet_langue.WriteDataGridView(row);
                        if (dgv_langue.Rows.Count > dao.max)
                        {
                            objet_langue.RemoveDataGridView(dgv_langue.Rows.Count - 1);
                        }
                        if (!load)
                            dao.Result.Add(y);
                        break;
                    case 2:
                        if (idx > -1)
                        {
                            objet_langue.RemoveDataGridView(idx);
                            objet_langue.WriteDataGridView(idx, row);
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
                            objet_langue.RemoveDataGridView(idx);
                        }
                        if (!load)
                            dao.Result.Remove(y);
                        break;
                }
            }
        }

        private void ResetData()
        {
            for (int i = 0; i < dgv_langue.Rows.Count; i++)
            {
                dgv_langue.Rows[i].Selected = false;
            }
        }

        private void ResetView()
        {
            txt_code.ResetText();
            txt_intitule.ResetText();
            chk_actif.Checked = true;

            select = new Langue();
            entity = new Langue();
        }

        private void LoadOnView(Langue y)
        {
            txt_code.Text = y.Code;
            txt_intitule.Text = y.Intitule;
            chk_actif.Checked = y.Actif;
            entity = y;
        }

        private Langue RecopieView()
        {
            Langue y = new Langue(entity.Id);
            y.Code = txt_code.Text.Trim();
            y.Intitule = txt_intitule.Text.Trim();
            y.Actif = chk_actif.Checked;
            return y;
        }

        private bool ControleView(Langue y)
        {
            if (y.Code != null ? y.Code.Trim().Length < 1 : true)
            {
                Messages.Error("Vous devez specifier le code");
                return false;
            }
            if (y.Intitule != null ? y.Intitule.Trim().Length < 1 : true)
            {
                Messages.Error("Vous devez specifier l'intitulé");
                return false;
            }
            Langue d = LangueBLL.One(y.Code);
            if (d != null ? (d.Id > 0 ? !d.Id.Equals(y.Id) : false) : false)
            {
                Messages.Error("Vous avez deja créer cette langue");
                return false;
            }
            return true;
        }

        private void Form_Langue_Load(object sender, EventArgs e)
        {
            LoadAll(true, true);
            ResetView();
        }

        private void dgv_groupe_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_langue.HitTest(e.X, e.Y); //get info
            int pos = dgv_langue.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_langue.Rows[pos].Cells["id"].Value != null)
                {
                    Int64 id = (Int32)dgv_langue.Rows[pos].Cells["id"].Value;
                    if (id > 0)
                    {
                        Langue y = dao.Result.Find(x => x.Id == id);
                        if (y != null ? y.Id > 0 : false)
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        select = y;
                                        ResetData();
                                        dgv_langue.Rows[pos].Selected = true; //Select the row
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string action = entity.Id > 0 ? "Modification" : "Insertion";
            try
            {
                Langue y = RecopieView();
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
                Logs.Exception("Form_Langue (btn_save_Click)", ex);
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
                p.OtherExpression.Add(new ParametreRequete("UPPER(y.code)", "code", num.ToUpper() + "%", "LIKE", "OR"));
                p.OtherExpression.Add(new ParametreRequete("UPPER(y.intitule)", "code", num.ToUpper() + "%", "LIKE", "OR"));
            }
            dao.AddParamToFind(p);
            LoadAll(true, true);
        }

        private void Form_Langue_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_LANGUE = null;
        }

        private void txt_intitule_Leave(object sender, EventArgs e)
        {
            string intitule = txt_intitule.Text.Trim();
            if (intitule != null ? intitule.Length > 0 : false)
            {
                txt_code.Text = (intitule.Length > 3 ? intitule.Substring(0, 3) : intitule).ToUpper();
            }
        }
    }
}
