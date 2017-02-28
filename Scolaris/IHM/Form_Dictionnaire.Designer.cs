namespace Scolaris.IHM
{
    partial class Form_Dictionnaire
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grp_information = new System.Windows.Forms.GroupBox();
            this.com_traduction = new System.Windows.Forms.ComboBox();
            this.com_langue = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nom = new System.Windows.Forms.TextBox();
            this.lb_langue = new System.Windows.Forms.Label();
            this.lb_code = new System.Windows.Forms.Label();
            this.grp_list = new System.Windows.Forms.GroupBox();
            this.dgv_dico = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.langue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.context_dico = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.com_rows = new System.Windows.Forms.ComboBox();
            this.btn_next = new System.Windows.Forms.Button();
            this.lb_position = new System.Windows.Forms.Label();
            this.btn_prev = new System.Windows.Forms.Button();
            this.grp_button = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.grp_search = new System.Windows.Forms.GroupBox();
            this.com_langue_search = new System.Windows.Forms.ComboBox();
            this.txt_num_search = new System.Windows.Forms.TextBox();
            this.lb_num_search = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grp_information.SuspendLayout();
            this.grp_list.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dico)).BeginInit();
            this.context_dico.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grp_button.SuspendLayout();
            this.grp_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_information
            // 
            this.grp_information.Controls.Add(this.com_traduction);
            this.grp_information.Controls.Add(this.com_langue);
            this.grp_information.Controls.Add(this.label1);
            this.grp_information.Controls.Add(this.txt_nom);
            this.grp_information.Controls.Add(this.lb_langue);
            this.grp_information.Controls.Add(this.lb_code);
            this.grp_information.Location = new System.Drawing.Point(3, 1);
            this.grp_information.Name = "grp_information";
            this.grp_information.Size = new System.Drawing.Size(384, 94);
            this.grp_information.TabIndex = 0;
            this.grp_information.TabStop = false;
            this.grp_information.Text = "Information";
            // 
            // com_traduction
            // 
            this.com_traduction.Enabled = false;
            this.com_traduction.FormattingEnabled = true;
            this.com_traduction.Location = new System.Drawing.Point(75, 65);
            this.com_traduction.Name = "com_traduction";
            this.com_traduction.Size = new System.Drawing.Size(303, 21);
            this.com_traduction.TabIndex = 1;
            this.com_traduction.SelectedIndexChanged += new System.EventHandler(this.com_traduction_SelectedIndexChanged);
            // 
            // com_langue
            // 
            this.com_langue.FormattingEnabled = true;
            this.com_langue.Location = new System.Drawing.Point(75, 14);
            this.com_langue.Name = "com_langue";
            this.com_langue.Size = new System.Drawing.Size(121, 21);
            this.com_langue.TabIndex = 1;
            this.com_langue.SelectedIndexChanged += new System.EventHandler(this.com_langue_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Traduction";
            // 
            // txt_nom
            // 
            this.txt_nom.Location = new System.Drawing.Point(75, 40);
            this.txt_nom.Name = "txt_nom";
            this.txt_nom.Size = new System.Drawing.Size(303, 20);
            this.txt_nom.TabIndex = 0;
            // 
            // lb_langue
            // 
            this.lb_langue.AutoSize = true;
            this.lb_langue.Location = new System.Drawing.Point(11, 17);
            this.lb_langue.Name = "lb_langue";
            this.lb_langue.Size = new System.Drawing.Size(43, 13);
            this.lb_langue.TabIndex = 0;
            this.lb_langue.Text = "Langue";
            // 
            // lb_code
            // 
            this.lb_code.AutoSize = true;
            this.lb_code.Location = new System.Drawing.Point(11, 43);
            this.lb_code.Name = "lb_code";
            this.lb_code.Size = new System.Drawing.Size(32, 13);
            this.lb_code.TabIndex = 0;
            this.lb_code.Text = "Code";
            // 
            // grp_list
            // 
            this.grp_list.Controls.Add(this.dgv_dico);
            this.grp_list.Location = new System.Drawing.Point(3, 97);
            this.grp_list.Name = "grp_list";
            this.grp_list.Size = new System.Drawing.Size(516, 258);
            this.grp_list.TabIndex = 1;
            this.grp_list.TabStop = false;
            this.grp_list.Text = "Liste";
            // 
            // dgv_dico
            // 
            this.dgv_dico.AllowUserToAddRows = false;
            this.dgv_dico.AllowUserToDeleteRows = false;
            this.dgv_dico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_dico.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_dico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_dico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.mot,
            this.langue,
            this.parent});
            this.dgv_dico.ContextMenuStrip = this.context_dico;
            this.dgv_dico.Location = new System.Drawing.Point(2, 17);
            this.dgv_dico.Name = "dgv_dico";
            this.dgv_dico.ReadOnly = true;
            this.dgv_dico.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_dico.Size = new System.Drawing.Size(507, 235);
            this.dgv_dico.TabIndex = 0;
            this.dgv_dico.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_groupe_MouseDown);
            // 
            // id
            // 
            this.id.HeaderText = "";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // mot
            // 
            this.mot.HeaderText = "Mot";
            this.mot.Name = "mot";
            this.mot.ReadOnly = true;
            // 
            // langue
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.langue.DefaultCellStyle = dataGridViewCellStyle1;
            this.langue.FillWeight = 30F;
            this.langue.HeaderText = "Langue";
            this.langue.Name = "langue";
            this.langue.ReadOnly = true;
            // 
            // parent
            // 
            this.parent.HeaderText = "Traduction";
            this.parent.Name = "parent";
            this.parent.ReadOnly = true;
            // 
            // context_dico
            // 
            this.context_dico.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.context_dico.Name = "context_groupe";
            this.context_dico.Size = new System.Drawing.Size(130, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Scolaris.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.deleteToolStripMenuItem.Text = "Supprimer";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.com_rows);
            this.panel1.Controls.Add(this.btn_next);
            this.panel1.Controls.Add(this.lb_position);
            this.panel1.Controls.Add(this.btn_prev);
            this.panel1.Location = new System.Drawing.Point(374, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(135, 23);
            this.panel1.TabIndex = 2;
            // 
            // com_rows
            // 
            this.com_rows.FormattingEnabled = true;
            this.com_rows.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "25",
            "50",
            "100",
            "150",
            "200"});
            this.com_rows.Location = new System.Drawing.Point(0, 1);
            this.com_rows.Name = "com_rows";
            this.com_rows.Size = new System.Drawing.Size(39, 21);
            this.com_rows.TabIndex = 3;
            this.com_rows.Text = "15";
            this.com_rows.SelectedIndexChanged += new System.EventHandler(this.com_rows_SelectedIndexChanged);
            // 
            // btn_next
            // 
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Image = global::Scolaris.Properties.Resources.next;
            this.btn_next.Location = new System.Drawing.Point(104, 0);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(30, 23);
            this.btn_next.TabIndex = 0;
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // lb_position
            // 
            this.lb_position.AutoSize = true;
            this.lb_position.Location = new System.Drawing.Point(75, 5);
            this.lb_position.Name = "lb_position";
            this.lb_position.Size = new System.Drawing.Size(24, 13);
            this.lb_position.TabIndex = 0;
            this.lb_position.Text = "1/1";
            // 
            // btn_prev
            // 
            this.btn_prev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_prev.Image = global::Scolaris.Properties.Resources.prec;
            this.btn_prev.Location = new System.Drawing.Point(39, 0);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(30, 23);
            this.btn_prev.TabIndex = 1;
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // grp_button
            // 
            this.grp_button.Controls.Add(this.btn_cancel);
            this.grp_button.Controls.Add(this.btn_save);
            this.grp_button.Location = new System.Drawing.Point(393, 1);
            this.grp_button.Name = "grp_button";
            this.grp_button.Size = new System.Drawing.Size(126, 69);
            this.grp_button.TabIndex = 1;
            this.grp_button.TabStop = false;
            this.grp_button.Text = "Boutons";
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = global::Scolaris.Properties.Resources.cancel;
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.Location = new System.Drawing.Point(15, 41);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(103, 23);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Nouveau";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Image = global::Scolaris.Properties.Resources.save;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_save.Location = new System.Drawing.Point(15, 15);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(103, 23);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Enregistrer";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // grp_search
            // 
            this.grp_search.Controls.Add(this.panel1);
            this.grp_search.Controls.Add(this.com_langue_search);
            this.grp_search.Controls.Add(this.txt_num_search);
            this.grp_search.Controls.Add(this.lb_num_search);
            this.grp_search.Controls.Add(this.label2);
            this.grp_search.Location = new System.Drawing.Point(3, 354);
            this.grp_search.Name = "grp_search";
            this.grp_search.Size = new System.Drawing.Size(516, 51);
            this.grp_search.TabIndex = 2;
            this.grp_search.TabStop = false;
            this.grp_search.Text = "Recherche";
            // 
            // com_langue_search
            // 
            this.com_langue_search.FormattingEnabled = true;
            this.com_langue_search.Location = new System.Drawing.Point(183, 30);
            this.com_langue_search.Name = "com_langue_search";
            this.com_langue_search.Size = new System.Drawing.Size(121, 21);
            this.com_langue_search.TabIndex = 1;
            this.com_langue_search.SelectedIndexChanged += new System.EventHandler(this.com_langue_search_SelectedIndexChanged);
            // 
            // txt_num_search
            // 
            this.txt_num_search.Location = new System.Drawing.Point(23, 30);
            this.txt_num_search.Name = "txt_num_search";
            this.txt_num_search.Size = new System.Drawing.Size(154, 20);
            this.txt_num_search.TabIndex = 1;
            this.txt_num_search.TextChanged += new System.EventHandler(this.txt_num_search_TextChanged);
            // 
            // lb_num_search
            // 
            this.lb_num_search.AutoSize = true;
            this.lb_num_search.Location = new System.Drawing.Point(20, 15);
            this.lb_num_search.Name = "lb_num_search";
            this.lb_num_search.Size = new System.Drawing.Size(57, 13);
            this.lb_num_search.TabIndex = 0;
            this.lb_num_search.Text = "Reference";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Langue";
            // 
            // Form_Dictionnaire
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(521, 411);
            this.Controls.Add(this.grp_search);
            this.Controls.Add(this.grp_button);
            this.Controls.Add(this.grp_list);
            this.Controls.Add(this.grp_information);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(537, 450);
            this.MinimumSize = new System.Drawing.Size(537, 450);
            this.Name = "Form_Dictionnaire";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dictionnaire";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Dictionnaire_FormClosed);
            this.Load += new System.EventHandler(this.Form_Dictionnaire_Load);
            this.grp_information.ResumeLayout(false);
            this.grp_information.PerformLayout();
            this.grp_list.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dico)).EndInit();
            this.context_dico.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grp_button.ResumeLayout(false);
            this.grp_search.ResumeLayout(false);
            this.grp_search.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_information;
        private System.Windows.Forms.GroupBox grp_list;
        private System.Windows.Forms.GroupBox grp_button;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox grp_search;
        private System.Windows.Forms.Label lb_code;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.DataGridView dgv_dico;
        private System.Windows.Forms.ContextMenuStrip context_dico;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
        private System.Windows.Forms.Label lb_position;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox com_rows;
        private System.Windows.Forms.TextBox txt_num_search;
        private System.Windows.Forms.Label lb_num_search;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn mot;
        private System.Windows.Forms.DataGridViewTextBoxColumn langue;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent;
        private System.Windows.Forms.ComboBox com_traduction;
        private System.Windows.Forms.ComboBox com_langue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_langue;
        private System.Windows.Forms.ComboBox com_langue_search;
        private System.Windows.Forms.Label label2;
    }
}