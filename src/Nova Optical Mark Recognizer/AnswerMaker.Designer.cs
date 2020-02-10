namespace Nova_Optical_Mark_Recognizer
{
    partial class AnswerMaker
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_ansgrid = new System.Windows.Forms.DataGridView();
            this.question = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.answer = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btn_applyansongrid = new System.Windows.Forms.Button();
            this.tb_numofquesingrid = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.num_wrongans = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.num_unattemptedans = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.num_attemptedans = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_wronganspreview = new System.Windows.Forms.Label();
            this.lbl_unattemptedanspreview = new System.Windows.Forms.Label();
            this.lbl_attemptedanspreview = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lv_preview = new System.Windows.Forms.ListView();
            this.lvcol_question = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvcol_answer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CreateAnswerSheet = new System.Windows.Forms.Button();
            this.error_noofques = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ansgrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_wrongans)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_unattemptedans)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_attemptedans)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_noofques)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_ansgrid);
            this.groupBox2.Controls.Add(this.btn_applyansongrid);
            this.groupBox2.Controls.Add(this.tb_numofquesingrid);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(13, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 498);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create Answer";
            // 
            // dgv_ansgrid
            // 
            this.dgv_ansgrid.AllowUserToAddRows = false;
            this.dgv_ansgrid.AllowUserToDeleteRows = false;
            this.dgv_ansgrid.AllowUserToResizeColumns = false;
            this.dgv_ansgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.dgv_ansgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_ansgrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ansgrid.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgv_ansgrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ansgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.question,
            this.answer});
            this.dgv_ansgrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_ansgrid.Location = new System.Drawing.Point(9, 53);
            this.dgv_ansgrid.Name = "dgv_ansgrid";
            this.dgv_ansgrid.RowHeadersVisible = false;
            this.dgv_ansgrid.Size = new System.Drawing.Size(316, 439);
            this.dgv_ansgrid.TabIndex = 4;
            this.dgv_ansgrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ansgrid_CellEnter);
            this.dgv_ansgrid.Leave += new System.EventHandler(this.dgv_ansgrid_Leave);
            // 
            // question
            // 
            this.question.FillWeight = 68.02031F;
            this.question.HeaderText = "Question";
            this.question.Name = "question";
            this.question.ReadOnly = true;
            // 
            // answer
            // 
            this.answer.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.answer.FillWeight = 131.9797F;
            this.answer.HeaderText = "Answer";
            this.answer.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"});
            this.answer.Name = "answer";
            // 
            // btn_applyansongrid
            // 
            this.btn_applyansongrid.Location = new System.Drawing.Point(269, 14);
            this.btn_applyansongrid.Name = "btn_applyansongrid";
            this.btn_applyansongrid.Size = new System.Drawing.Size(56, 29);
            this.btn_applyansongrid.TabIndex = 3;
            this.btn_applyansongrid.Text = "Apply";
            this.btn_applyansongrid.UseVisualStyleBackColor = true;
            this.btn_applyansongrid.Click += new System.EventHandler(this.btn_applyansongrid_Click);
            // 
            // tb_numofquesingrid
            // 
            this.tb_numofquesingrid.Location = new System.Drawing.Point(145, 19);
            this.tb_numofquesingrid.Name = "tb_numofquesingrid";
            this.tb_numofquesingrid.Size = new System.Drawing.Size(118, 20);
            this.tb_numofquesingrid.TabIndex = 2;
            this.tb_numofquesingrid.Validating += new System.ComponentModel.CancelEventHandler(this.tb_numofquesingrid_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Enter Number of Questions:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.num_wrongans);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(359, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(178, 115);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Negative Marking";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "(-ve)";
            // 
            // num_wrongans
            // 
            this.num_wrongans.Location = new System.Drawing.Point(45, 64);
            this.num_wrongans.Name = "num_wrongans";
            this.num_wrongans.Size = new System.Drawing.Size(103, 20);
            this.num_wrongans.TabIndex = 5;
            this.num_wrongans.ValueChanged += new System.EventHandler(this.num_wrongans_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Negative mark per wrong answer:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.num_unattemptedans);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(359, 144);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(178, 115);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Unattempted Questions";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "(-ve)";
            // 
            // num_unattemptedans
            // 
            this.num_unattemptedans.Location = new System.Drawing.Point(45, 67);
            this.num_unattemptedans.Name = "num_unattemptedans";
            this.num_unattemptedans.Size = new System.Drawing.Size(103, 20);
            this.num_unattemptedans.TabIndex = 4;
            this.num_unattemptedans.ValueChanged += new System.EventHandler(this.num_unattemptedans_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mark per unattempted answer:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.num_attemptedans);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Location = new System.Drawing.Point(359, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(178, 108);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Attempted Questions";
            // 
            // num_attemptedans
            // 
            this.num_attemptedans.Location = new System.Drawing.Point(45, 62);
            this.num_attemptedans.Name = "num_attemptedans";
            this.num_attemptedans.Size = new System.Drawing.Size(103, 20);
            this.num_attemptedans.TabIndex = 4;
            this.num_attemptedans.ValueChanged += new System.EventHandler(this.num_attemptedans_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mark per attempted answer:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.lbl_wronganspreview);
            this.panel1.Controls.Add(this.lbl_unattemptedanspreview);
            this.panel1.Controls.Add(this.lbl_attemptedanspreview);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lv_preview);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(549, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 525);
            this.panel1.TabIndex = 22;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(160, 441);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 441);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "Total Questions:";
            // 
            // lbl_wronganspreview
            // 
            this.lbl_wronganspreview.AutoSize = true;
            this.lbl_wronganspreview.Location = new System.Drawing.Point(160, 503);
            this.lbl_wronganspreview.Name = "lbl_wronganspreview";
            this.lbl_wronganspreview.Size = new System.Drawing.Size(41, 13);
            this.lbl_wronganspreview.TabIndex = 8;
            this.lbl_wronganspreview.Text = "label12";
            // 
            // lbl_unattemptedanspreview
            // 
            this.lbl_unattemptedanspreview.AutoSize = true;
            this.lbl_unattemptedanspreview.Location = new System.Drawing.Point(160, 482);
            this.lbl_unattemptedanspreview.Name = "lbl_unattemptedanspreview";
            this.lbl_unattemptedanspreview.Size = new System.Drawing.Size(41, 13);
            this.lbl_unattemptedanspreview.TabIndex = 7;
            this.lbl_unattemptedanspreview.Text = "label11";
            // 
            // lbl_attemptedanspreview
            // 
            this.lbl_attemptedanspreview.AutoSize = true;
            this.lbl_attemptedanspreview.Location = new System.Drawing.Point(160, 461);
            this.lbl_attemptedanspreview.Name = "lbl_attemptedanspreview";
            this.lbl_attemptedanspreview.Size = new System.Drawing.Size(41, 13);
            this.lbl_attemptedanspreview.TabIndex = 6;
            this.lbl_attemptedanspreview.Text = "label10";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 503);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Mark per wrong answer:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 482);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Mark per unattempted answer:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 461);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Mark per attempted answer:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(137, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Preview:";
            // 
            // lv_preview
            // 
            this.lv_preview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvcol_question,
            this.lvcol_answer});
            this.lv_preview.Location = new System.Drawing.Point(3, 23);
            this.lv_preview.Name = "lv_preview";
            this.lv_preview.Size = new System.Drawing.Size(313, 407);
            this.lv_preview.TabIndex = 0;
            this.lv_preview.UseCompatibleStateImageBehavior = false;
            this.lv_preview.View = System.Windows.Forms.View.Details;
            // 
            // lvcol_question
            // 
            this.lvcol_question.Text = "Question";
            this.lvcol_question.Width = 83;
            // 
            // lvcol_answer
            // 
            this.lvcol_answer.Text = "Answer";
            // 
            // CreateAnswerSheet
            // 
            this.CreateAnswerSheet.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CreateAnswerSheet.Location = new System.Drawing.Point(368, 423);
            this.CreateAnswerSheet.Name = "CreateAnswerSheet";
            this.CreateAnswerSheet.Size = new System.Drawing.Size(155, 81);
            this.CreateAnswerSheet.TabIndex = 23;
            this.CreateAnswerSheet.Text = "Create Answer Sheet";
            this.CreateAnswerSheet.UseVisualStyleBackColor = true;
            this.CreateAnswerSheet.Click += new System.EventHandler(this.CreateAnswerSheet_Click);
            // 
            // error_noofques
            // 
            this.error_noofques.ContainerControl = this;
            // 
            // AnswerMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 525);
            this.Controls.Add(this.CreateAnswerSheet);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AnswerMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Answer Maker";
            this.Load += new System.EventHandler(this.AnswerMaker_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ansgrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_wrongans)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_unattemptedans)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_attemptedans)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.error_noofques)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_unattemptedans;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown num_attemptedans;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lv_preview;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button CreateAnswerSheet;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown num_wrongans;
        private System.Windows.Forms.Label lbl_wronganspreview;
        private System.Windows.Forms.Label lbl_unattemptedanspreview;
        private System.Windows.Forms.Label lbl_attemptedanspreview;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_applyansongrid;
        private System.Windows.Forms.TextBox tb_numofquesingrid;
        private System.Windows.Forms.DataGridView dgv_ansgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn question;
        private System.Windows.Forms.DataGridViewComboBoxColumn answer;
        private System.Windows.Forms.ColumnHeader lvcol_question;
        private System.Windows.Forms.ColumnHeader lvcol_answer;
        private System.Windows.Forms.ErrorProvider error_noofques;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}