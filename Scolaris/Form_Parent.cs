using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Scolaris.ENTITE;
using Scolaris.TOOLS;

namespace Scolaris
{
    public partial class Form_Parent : Form
    {
        private int childFormNumber = 0;

        public Form_Parent()
        {
            InitializeComponent();
            Dictionnaire.Traduction(this);
            OpenModule(0);
        }

        private void OpenModule(int module)
        {
            switch (module)
            {
                case 1:
                    {
                        break;
                    }
                default:
                    {
                        lb_name_module.te
                        break;
                    }
            }
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Fenêtre " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Fichiers texte (*.txt)|*.txt|Tous les fichiers (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void Form_Parent_Load(object sender, EventArgs e)
        {
            Managed();
        }

        private void Managed()
        {
            this.Text = Constantes.APP_NAME;

            Traduction();
        }

        private void Traduction()
        {

        }

        private void btn_slide_panel_Click(object sender, EventArgs e)
        {
            if (btn_slide_panel.Tag != null)
            {
                bool slide = Convert.ToBoolean(btn_slide_panel.Tag);
                panel_module.Visible = slide;
                slide = !slide;
                btn_slide_panel.Tag = slide;
                btn_slide_panel.Image = slide ? global::Scolaris.Properties.Resources.next : global::Scolaris.Properties.Resources.prec;
            }
        }

        private void voirLogServeurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (voirLogServeurToolStripMenuItem.Tag != null)
            {
                bool slide = Convert.ToBoolean(voirLogServeurToolStripMenuItem.Tag);
                //btn_view_log.Visible = slide;
                slide = !slide;
                voirLogServeurToolStripMenuItem.Tag = slide;
            }
        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_DICTIONNAIRE == null)
            {
                IHM.Form_Dictionnaire f = new IHM.Form_Dictionnaire();
                f.MdiParent = this;
                Constantes.FORM_DICTIONNAIRE = f;
                f.Show();
            }
            else
            {
                Constantes.FORM_DICTIONNAIRE.WindowState = FormWindowState.Normal;
                Constantes.FORM_DICTIONNAIRE.BringToFront();
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_DICTIONNAIRE == null)
            {
                IHM.Form_Langue f = new IHM.Form_Langue();
                f.MdiParent = this;
                Constantes.FORM_LANGUE = f;
                f.Show();
            }
            else
            {
                Constantes.FORM_LANGUE.WindowState = FormWindowState.Normal;
                Constantes.FORM_LANGUE.BringToFront();
            }
        }
    }
}
