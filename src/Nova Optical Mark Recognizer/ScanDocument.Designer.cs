namespace Nova_Optical_Mark_Recognizer
{
    partial class ScanDocument
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
            this.btn_scanimage = new System.Windows.Forms.Button();
            this.btn_proceed = new System.Windows.Forms.Button();
            this.pnl_omrpicbox_container = new System.Windows.Forms.Panel();
            this.picbox_displayomrsheet = new System.Windows.Forms.PictureBox();
            this.btn_saveimage = new System.Windows.Forms.Button();
            this.btn_rotateleft = new System.Windows.Forms.Button();
            this.btn_rotateright = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pnl_omrpicbox_container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_displayomrsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_scanimage
            // 
            this.btn_scanimage.Location = new System.Drawing.Point(530, 22);
            this.btn_scanimage.Name = "btn_scanimage";
            this.btn_scanimage.Size = new System.Drawing.Size(135, 90);
            this.btn_scanimage.TabIndex = 7;
            this.btn_scanimage.Text = "Scan Image";
            this.btn_scanimage.UseVisualStyleBackColor = true;
            this.btn_scanimage.Click += new System.EventHandler(this.btn_scanimage_Click);
            // 
            // btn_proceed
            // 
            this.btn_proceed.Location = new System.Drawing.Point(530, 247);
            this.btn_proceed.Name = "btn_proceed";
            this.btn_proceed.Size = new System.Drawing.Size(135, 90);
            this.btn_proceed.TabIndex = 9;
            this.btn_proceed.Text = "Proceed";
            this.btn_proceed.UseVisualStyleBackColor = true;
            this.btn_proceed.Click += new System.EventHandler(this.btn_proceed_Click);
            // 
            // pnl_omrpicbox_container
            // 
            this.pnl_omrpicbox_container.AutoScroll = true;
            this.pnl_omrpicbox_container.BackColor = System.Drawing.Color.Transparent;
            this.pnl_omrpicbox_container.Controls.Add(this.picbox_displayomrsheet);
            this.pnl_omrpicbox_container.Location = new System.Drawing.Point(42, 3);
            this.pnl_omrpicbox_container.Name = "pnl_omrpicbox_container";
            this.pnl_omrpicbox_container.Size = new System.Drawing.Size(466, 656);
            this.pnl_omrpicbox_container.TabIndex = 10;
            // 
            // picbox_displayomrsheet
            // 
            this.picbox_displayomrsheet.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.picbox_displayomrsheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picbox_displayomrsheet.Location = new System.Drawing.Point(3, 3);
            this.picbox_displayomrsheet.Name = "picbox_displayomrsheet";
            this.picbox_displayomrsheet.Size = new System.Drawing.Size(459, 650);
            this.picbox_displayomrsheet.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picbox_displayomrsheet.TabIndex = 4;
            this.picbox_displayomrsheet.TabStop = false;
            // 
            // btn_saveimage
            // 
            this.btn_saveimage.Location = new System.Drawing.Point(530, 132);
            this.btn_saveimage.Name = "btn_saveimage";
            this.btn_saveimage.Size = new System.Drawing.Size(135, 90);
            this.btn_saveimage.TabIndex = 11;
            this.btn_saveimage.Text = "Save";
            this.btn_saveimage.UseVisualStyleBackColor = true;
            this.btn_saveimage.Click += new System.EventHandler(this.btn_saveimage_Click);
            // 
            // btn_rotateleft
            // 
            this.btn_rotateleft.Location = new System.Drawing.Point(514, 588);
            this.btn_rotateleft.Name = "btn_rotateleft";
            this.btn_rotateleft.Size = new System.Drawing.Size(75, 68);
            this.btn_rotateleft.TabIndex = 12;
            this.btn_rotateleft.Text = "Rotate Left";
            this.btn_rotateleft.UseVisualStyleBackColor = true;
            this.btn_rotateleft.Click += new System.EventHandler(this.btn_rotateleft_Click);
            // 
            // btn_rotateright
            // 
            this.btn_rotateright.Location = new System.Drawing.Point(595, 588);
            this.btn_rotateright.Name = "btn_rotateright";
            this.btn_rotateright.Size = new System.Drawing.Size(75, 69);
            this.btn_rotateright.TabIndex = 13;
            this.btn_rotateright.Text = "Rotate Right";
            this.btn_rotateright.UseVisualStyleBackColor = true;
            this.btn_rotateright.Click += new System.EventHandler(this.btn_rotateright_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 502);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ScanDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 671);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_rotateright);
            this.Controls.Add(this.btn_rotateleft);
            this.Controls.Add(this.btn_saveimage);
            this.Controls.Add(this.pnl_omrpicbox_container);
            this.Controls.Add(this.btn_proceed);
            this.Controls.Add(this.btn_scanimage);
            this.Name = "ScanDocument";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ScanDocument";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnl_omrpicbox_container.ResumeLayout(false);
            this.pnl_omrpicbox_container.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_displayomrsheet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_scanimage;
        private System.Windows.Forms.Button btn_proceed;
        private System.Windows.Forms.Panel pnl_omrpicbox_container;
        private System.Windows.Forms.PictureBox picbox_displayomrsheet;
        private System.Windows.Forms.Button btn_saveimage;
        private System.Windows.Forms.Button btn_rotateleft;
        private System.Windows.Forms.Button btn_rotateright;
        private System.Windows.Forms.Button button1;
    }
}