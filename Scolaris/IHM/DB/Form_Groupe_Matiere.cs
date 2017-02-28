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
using Scolaris.BLL.DB;
using Scolaris.ENTITE;
using Scolaris.ENTITE.DB;
using Scolaris.TOOLS;

namespace Scolaris.IHM.DB
{
    public partial class Form_Groupe_Matiere : Form
    {
        GroupeMatiereBLL dao = new GroupeMatiereBLL();
        GroupeMatiere entity = new GroupeMatiere();
        GroupeMatiere select = new GroupeMatiere();
        ObjectThread objet_groupe;

        public Form_Groupe_Matiere()
        {
            InitializeComponent();
            Configuration.Load(this);
            Dictionnaire.Traduction(this);
            objet_groupe = new ObjectThread(dgv_groupe);
        }

        private void LoadAll(bool avance, bool init)
        {
            dao.AddParam(new ParametreRequete("y.langue.id", "langue", Constantes.LANGUE.Id, "=", "AND"));
            dao.ExecuteDynamicQuery(GroupeMatiere.ToTable(), "y.position desc, y.code", avance, init);

            objet_groupe.ClearDataGridView(true);
            foreach (GroupeMatiere g in dao.Result)
            {
                Action(g, 1, true);
            }

            btn_prev.Enabled = !dao.disPrev;
            btn_next.Enabled = !dao.disNext;
            lb_position.Text = dao.currentPage + "/" + dao.totalPage;
        }

        private void Action(GroupeMatiere y, int action, bool load)// action : 1 pour sauvegarder - 2 pour modifier - 3 pour supprimer
        {
            int idx = Utils.GetRowData(dgv_groupe, y.Id);
            object[] row = new object[] { y.Id, y.Code, y.Intitule };
            switch (action)
            {
                case 1:
                    objet_groupe.WriteDataGridView(row);
                    if (dgv_groupe.Rows.Count > dao.max)
                    {
                        objet_groupe.RemoveDataGridView(dgv_groupe.Rows.Count - 1);
                    }
                    if (!load)
                        dao.Result.Add(y);
                    break;
                case 2:
                    if (idx > -1)
                    {
                        objet_groupe.RemoveDataGridView(idx);
                        objet_groupe.WriteDataGridView(idx, row);
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
                        objet_groupe.RemoveDataGridView(idx);
                    }
                    if (!load)
                        dao.Result.Remove(y);
                    break;
            }
        }

        private void ResetData()
        {
            for (int i = 0; i < dgv_groupe.Rows.Count; i++)
            {
                dgv_groupe.Rows[i].Selected = false;
            }
        }

        private void ResetView()
        {
            txt_code.ResetText();
            txt_intitule.ResetText();
            select = new GroupeMatiere();
            entity = new GroupeMatiere();
        }

        private void LoadOnView(GroupeMatiere y)
        {
            txt_code.Text = y.Code;
            txt_intitule.Text = y.Intitule;
            entity = y;
        }

        private GroupeMatiere RecopieView()
        {
            GroupeMatiere y = new GroupeMatiere(entity.Id);
            y.Code = txt_code.Text.Trim();
            y.Intitule = txt_intitule.Text.Trim();
            y.Position = entity.Position;
            return y;
        }

        private bool ControleView(GroupeMatiere y)
        {
            if (y.Code != null ? y.Code.Trim().Length < 1 : true)
            {
                Messages.Error("Vous devez specifier le code");
                return false;
            }
            return true;
        }

        private void Form_Groupe_Matiere_Load(object sender, EventArgs e)
        {
            LoadAll(true, true);
        }

        private void dgv_groupe_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_groupe.HitTest(e.X, e.Y); //get info
            int pos = dgv_groupe.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_groupe.Rows[pos].Cells["id"].Value != null)
                {
                    Int32 id = (Int32)dgv_groupe.Rows[pos].Cells["id"].Value;
                    if (id > 0)
                    {
                        GroupeMatiere y = dao.Result.Find(x => x.Id == id);
                        if (y != null ? y.Id > 0 : false)
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        select = y;
                                        ResetData();
                                        dgv_groupe.Rows[pos].Selected = true; //Select the row
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
                GroupeMatiere y = RecopieView();
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
                Logs.Exception("Form_Groupe_Matiere (btn_save_Click)", ex);
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
    }
}
