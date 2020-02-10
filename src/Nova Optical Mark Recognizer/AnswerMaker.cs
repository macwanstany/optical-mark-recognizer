using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;

namespace Nova_Optical_Mark_Recognizer
{
    public partial class AnswerMaker : Form
    {
        List<int> answerkey = new List<int>();
        int maxrows = 0;
        public string returnval_ansdest { get; set; } 

        public AnswerMaker()
        {
            InitializeComponent();

            dgv_ansgrid.UserAddedRow += dgv_ansgrid_RowCountChanged;
            dgv_ansgrid.UserDeletedRow += dgv_ansgrid_RowCountChanged;
        }

        private void AnswerMaker_Load(object sender, EventArgs e)
        {
            num_attemptedans.Value = 1;
            num_unattemptedans.Value = 0;
            num_wrongans.Value = 1;

            lbl_attemptedanspreview.Text = num_attemptedans.Value.ToString();
            lbl_unattemptedanspreview.Text = num_unattemptedans.Value.ToString();
            lbl_wronganspreview.Text = "(-ve) " + num_wrongans.Value.ToString();         
        }

        private void dgv_ansgrid_RowCountChanged(object sender, EventArgs e)
        {
            CheckRowCount();
        }

        private void CheckRowCount()
        {
            if (dgv_ansgrid.Rows != null && dgv_ansgrid.Rows.Count > maxrows)
            {
                dgv_ansgrid.AllowUserToAddRows = false;
            }
            else if (!dgv_ansgrid.AllowUserToAddRows)
            {
                dgv_ansgrid.AllowUserToAddRows = true;
            }
        }

        private void num_attemptedans_ValueChanged(object sender, EventArgs e)
        {
            lbl_attemptedanspreview.Text = num_attemptedans.Value.ToString();
        }

        private void num_unattemptedans_ValueChanged(object sender, EventArgs e)
        {
            if (num_unattemptedans.Value > 0)
            {
                lbl_unattemptedanspreview.Text = "(-ve) " + num_unattemptedans.Value.ToString();
            }
            else
            {
                lbl_unattemptedanspreview.Text = num_unattemptedans.Value.ToString();
            }
        }

        private void num_wrongans_ValueChanged(object sender, EventArgs e)
        {
            if (num_wrongans.Value > 0)
            {
                lbl_wronganspreview.Text = "(-ve) " + num_wrongans.Value.ToString();
            }
            else
            {
                lbl_wronganspreview.Text = num_wrongans.Value.ToString();
            }
            
        }

        private void btn_applyansongrid_Click(object sender, EventArgs e)
        {
            int numofrows;
            if(int.TryParse(tb_numofquesingrid.Text, out numofrows))
            {
                this.maxrows = numofrows;
                dgv_ansgrid.Rows.Add(numofrows);

                int cnt = 0;
                foreach (DataGridViewRow dgrow in dgv_ansgrid.Rows)
                {
                    dgrow.Cells["question"].Value = ++cnt;
                }
            }
            else
            {
                MessageBox.Show("Kindly Enter a numeric value in textbox corresponding to section 'Enter Number of Question'", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int totalques = dgv_ansgrid.Rows.Count;
            label11.Text = totalques.ToString();
        }

        private void dgv_ansgrid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_ansgrid.CurrentCell.ReadOnly)
                SendKeys.Send("{TAB}");
        }

        private void dgv_ansgrid_Leave(object sender, EventArgs e)
        {
            if(dgv_ansgrid.Rows.Count > 0)
                dgv_ansgrid.CurrentCell.Selected = false;

            dgv_ansgrid.EndEdit(DataGridViewDataErrorContexts.Commit);
                
            if (lv_preview.Items.Count > 0)
                lv_preview.Items.Clear();

            int rowcount = dgv_ansgrid.Rows.Count;
            
            if(rowcount > 0)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    string ans; 
                    if(dgv_ansgrid.Rows[i].Cells[1].Value == null)
                    {
                       ans = "-";
                    }
                    else
                    {
                        ans = dgv_ansgrid.Rows[i].Cells[1].Value.ToString();
                    }
                    ListViewItem lvitem = lv_preview.Items.Add((i+1).ToString());
                    lvitem.SubItems.Add(ans);
                    ans = null;
                }
                lv_preview.Update();
            }
            else if(rowcount == 0)
            {
                lv_preview.Items.Clear();
            }
        }

        private void tb_numofquesingrid_Validating(object sender, CancelEventArgs e)
        {
            validate_noofques();
        }

        private bool validate_noofques()
        {
            bool check;

            if (string.IsNullOrEmpty(tb_numofquesingrid.Text))
            {
                error_noofques.SetError(tb_numofquesingrid, null);
                check = true;                
            }
            else if (!Regex.IsMatch(tb_numofquesingrid.Text, @"^[0-9]*$"))
            {
                error_noofques.SetError(tb_numofquesingrid, "Kindly input a numeric value.");
                check = false;               
            }
            else
            {
                error_noofques.SetError(tb_numofquesingrid, null);
                check = true;                
            }
            return check;
        }

        private void CreateAnswerSheet_Click(object sender, EventArgs e)
        {            
            XmlDocument xmldoc = new XmlDocument();
            XmlNode rootnode = xmldoc.CreateElement("OMRAnswers");
            xmldoc.AppendChild(rootnode);

            XmlNode totalquesnode = xmldoc.CreateElement("TotalQuestions");
            totalquesnode.InnerText = dgv_ansgrid.Rows.Count.ToString();
            rootnode.AppendChild(totalquesnode);

            XmlNode markperattemptnode = xmldoc.CreateElement("MarkPerAttempted");
            markperattemptnode.InnerText = num_attemptedans.Value.ToString();
            rootnode.AppendChild(markperattemptnode);

            XmlNode markperunattemptnode = xmldoc.CreateElement("MarkPerUnAttempted");
            markperunattemptnode.InnerText = num_unattemptedans.Value.ToString();
            rootnode.AppendChild(markperunattemptnode);

            XmlNode negmarkingnode = xmldoc.CreateElement("NegativeMarking");
            negmarkingnode.InnerText = num_wrongans.Value.ToString();
            rootnode.AppendChild(negmarkingnode);

            XmlNode answersnode = xmldoc.CreateElement("Answers");            
            rootnode.AppendChild(answersnode);

            foreach (DataGridViewRow dgrow in dgv_ansgrid.Rows)
            {
                XmlNode questionnode = xmldoc.CreateElement("Question");

                XmlAttribute quesnumbattr = xmldoc.CreateAttribute("number");
                quesnumbattr.Value = dgrow.Cells[0].Value.ToString();
                questionnode.Attributes.Append(quesnumbattr);

                XmlAttribute ansattr = xmldoc.CreateAttribute("answer");
                ansattr.Value = dgrow.Cells[1].Value.ToString();
                questionnode.Attributes.Append(ansattr);

                answersnode.AppendChild(questionnode);
            }

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Answer Sheet";
                dlg.Filter = "OMR Answer Sheet file (.xml) | *.xml";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    xmldoc.Save(dlg.FileName);
                    this.returnval_ansdest = dlg.FileName;
                    this.Close();
                }
            }
        }
    }
}
