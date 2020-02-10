using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Controls;
using System.Drawing.Imaging;
using System.IO;
using TwainDotNet;
using TwainDotNet.WinFroms;
using TwainDotNet.TwainNative;
using System.Xml;
using System.Drawing.Printing;

namespace Nova_Optical_Mark_Recognizer
{
    public partial class ProcessOMR : Form
    {
        TimeSpan ts = new TimeSpan(DateTime.Now.Ticks);
        Image actualomrimage;
        
        List<Tuple<int, int>> resultlist = new List<Tuple<int, int>>();
        List<Tuple<int, Point>> list_queslocation = new List<Tuple<int, Point>>();

        int anssheet_totalques;
        double anssheet_markperattempt, anssheet_markperunattempt, anssheet_negmark;
        List<Tuple<int, string>> anssheet_answers = new List<Tuple<int, string>>();
        List<IGrouping<int, int>> resultlist_dupmerge = new List<IGrouping<int, int>>();

        private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;

        public ProcessOMR()
        {
            InitializeComponent();

            _twain = new Twain(new WinFormsWindowMessageHook(this));
            _twain.TransferImage += delegate(Object sender, TransferImageEventArgs args)
            {
                if (args.Image != null)
                {
                    picbox_displayomrsheet.Image = args.Image;
                }
            };
            _twain.ScanningComplete += delegate
            {
                Enabled = true;
            };

            if (!File.Exists("SheetSpecifications.xml"))
            {
                //generate the sheet specification file
                OMRSheetSpecsWriter.GenerateDefaultSheetSpecsFile("SheetSpecifications.xml");
            }            
        }       

        private void Form1_Load(object sender, EventArgs e)
        {
            if (ScanDocument.scannedimage != null)
            {
                picbox_displayomrsheet.Image = ScanDocument.scannedimage;
                this.actualomrimage = ScanDocument.scannedimage;
            }
            ((DataGridViewImageColumn)this.dgv_omrresult.Columns["result"]).DefaultCellStyle.NullValue = null;   
        }
           
        private void showTimeStamp(string str)
        {           
            textBox1.AppendText(str + ": " + (new TimeSpan(DateTime.Now.Ticks) - ts).TotalSeconds + " sec" + "\r\n");
            ts = new TimeSpan(DateTime.Now.Ticks);         
        }

        private void logOutputTerminal(string str)
        {
            textBox1.AppendText(str + "\r\n");
        }

        private void btn_processomrsheet_Click(object sender, EventArgs e)
        {
            //empty the resultlist if it is not empty
            if (resultlist.Count > 0)
            {
                resultlist.Clear();                
                dgv_omrresult.Rows.Clear();
            }

            if (this.resultlist_dupmerge.Count > 0)
            {
                resultlist_dupmerge.Clear();
            }

            if (list_queslocation.Count > 0)
            {
                list_queslocation.Clear();
            }

            // Scale the image to lower proportions while maintaining aspect ratio (for performance enhancements)                
            Bitmap scaledsheet = ImageProcessor.ScaleImage(picbox_displayomrsheet.Image, 900, 1300);
            picbox_displayomrsheet.Image = scaledsheet;
            ts = new TimeSpan(DateTime.Now.Ticks);
            logOutputTerminal("Initializing OMR Extraction Process");

            // Applying Image Pre-Processing
            logOutputTerminal("Applying Pre-Processing on the image");
            Bitmap omrpreprocess_bmp = ImageProcessor.ApplyPreProcessing((Bitmap)scaledsheet, 160);
            picbox_displayomrsheet.Image = omrpreprocess_bmp;
            showTimeStamp("Image Pre-Processing Completed");

            //Apply OMR Extraction            
            showTimeStamp("Image Flattening Started");
            Bitmap extractedsheet = OmrProcessor.ExtractPaperFromFlattened(omrpreprocess_bmp, scaledsheet, 5, 5, true);
            picbox_displayomrsheet.Image = extractedsheet;
            showTimeStamp("Image Flattening Completed");

            if (extractedsheet == null)
            {
                MessageBox.Show("Quadrilateral transformation on the preprocess omr sheet failed due to incorrect blob detection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Apply Resizing on the quadrilateral transformed image and bring it to a fixed proportion and resolution
            Bitmap resizedsheet = ImageProcessor.ResizeImage(extractedsheet, 600, 849, 72);
            picbox_displayomrsheet.Image = resizedsheet;

            //Apply preprocessing once again on the extracted sheet
            showTimeStamp("Second PreProcessing Started");
            Bitmap extracted_preprocess = ImageProcessor.ApplyPreProcessing(resizedsheet, 190);
            showTimeStamp("Second PreProcessing Completed");
            List<AForge.Imaging.Blob> bubblesblobs = OmrProcessor.ExtractBubbleCorrespondingBlobs(extracted_preprocess, 8, 8);
            showTimeStamp("Bubble Detection Completed");

            try
            {
                //Generate XML file containing blobs(i.e filled bubbles) information(position, area, center of gravity, fullness etc)                
                XMLReaderWriter.WriteNewBubblesDataXML(bubblesblobs, "BubbleData.xml");
            }
            catch (System.Xml.XmlException)
            {
                MessageBox.Show("Failed to Generate a Bubble XML file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            showTimeStamp("Bubble XML Generation Completed");

            if (picbox_displayomrsheet.SizeMode == PictureBoxSizeMode.AutoSize)
            {
                picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            /***debug purpose only***/
            //Bitmap bm = new Bitmap(resizedsheet.Size.Width, resizedsheet.Size.Height);
            //Graphics g = Graphics.FromImage(bm);
            //g.DrawImage(resizedsheet, new Rectangle(0, 0, resizedsheet.Width, resizedsheet.Height));
            //Pen redpen = new Pen(Color.Red, 2);

            //foreach (AForge.Imaging.Blob blob in bubblesblobs)
            //{
            //    g.DrawEllipse(redpen, blob.Rectangle.X, blob.Rectangle.Y, blob.Image.Width, blob.Image.Height);
            //}
            //picbox_displayomrsheet.Image = bm;
            /***debug purpose only***/

            Rectangle[] Blocks = new Rectangle[]
            {
                new Rectangle(52, 384, 99, 454),
                new Rectangle(196, 384, 99, 454),
                new Rectangle(340, 384, 99, 454),
                new Rectangle(484, 384, 99, 454)
            };

            //answer fetching algorithm starts                

            List<AForge.Imaging.Blob>[] blocksfitbubbles = new List<AForge.Imaging.Blob>[Blocks.Length];
            for (int i = 0; i < Blocks.Length; i++)
            {
                blocksfitbubbles[i] = new List<AForge.Imaging.Blob>();
            }
            //Re-position each blob in corresponding answerblocks array
            foreach (AForge.Imaging.Blob blob in bubblesblobs)
            {
                bool flag = false;
                for (int i = 0; i < Blocks.Length; i++)
                {
                    if (Blocks[i].Contains(new Point((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y)))
                    {
                        blocksfitbubbles[i].Add(blob);
                        flag = true;
                        break;
                    }
                }
                if (flag) continue;
            }

            //identify each bubble in block correspond to which question no and which option(a,b,c,d)
            //for identification of question no, slice the blocks and identify each bubbles falls in which slice
            //for identification of option(a,b,c,d) measure the difference in horizontal length from blob.rectangle.X to its corresponding block rectangle.X

            List<Rectangle>[] blockslices = new List<Rectangle>[Blocks.Length];
            int totalquestionsperblock = 25;
            for (int i = 0; i < Blocks.Length; i++)
            {
                blockslices[i] = new List<Rectangle>();
            }

            for (int i = 0; i < Blocks.Length; i++)
            {
                Rectangle[] singleblockslices = SliceBlock(Blocks[i], totalquestionsperblock);
                foreach (Rectangle slice in singleblockslices)
                {
                    blockslices[i].Add(slice);
                }
            }

            for (int i = 0; i < Blocks.Length; i++)
            {
                foreach (AForge.Imaging.Blob blob in blocksfitbubbles[i])
                {
                    bool flag = false;
                    foreach (Rectangle slice in blockslices[i])
                    {
                        if (slice.Contains(new Point((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y)))
                        {
                            //bubble is present in this slice
                            //get corresponding question no and its checked option                              
                            int questionno = ((int)(slice.Bottom - Blocks[i].Top) / slice.Height) + (i * totalquestionsperblock);
                            //MessageBox.Show("Ques: " + questionno + Environment.NewLine + "Bubble X: " + blob.CenterOfGravity.X.ToString() + Environment.NewLine + "Bubble Y: " + blob.CenterOfGravity.Y.ToString());

                            int parallelans_hordiff = (int)(slice.Width / 4);
                            int ans_hordist_fromslice = blob.Rectangle.Left - slice.Left;

                            int answerchecked = 0;
                            for (int k = 1; k <= 4; k++)
                            {
                                if ((ans_hordist_fromslice > (parallelans_hordiff * (k - 1))) && (ans_hordist_fromslice < (parallelans_hordiff * (k))))
                                {
                                    answerchecked = k;
                                }
                            }
                            resultlist.Add(Tuple.Create(questionno, answerchecked));
                            list_queslocation.Add(Tuple.Create(questionno, slice.Location));
                            //MessageBox.Show("Ques: " + questionno + Environment.NewLine + "Answer: " + answerchecked);
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;
                }
            }
            //answer fetching algorithm ends            

            this.resultlist_dupmerge = resultlist.GroupBy(x => x.Item1, x => x.Item2).ToList();           
            
            foreach (var group in resultlist_dupmerge)
            {
                DataGridViewRow dgrow = new DataGridViewRow();
                dgrow.CreateCells(dgv_omrresult);
                dgrow.Cells[0].Value = group.Key.ToString();
                
                string ans = "";
                foreach (var element in group)
                {
                    if (element == 1)
                    {
                        ans += "A";
                    }
                    else if (element == 2)
                    {
                        if (ans.Length >= 1)
                        {
                            ans += " ";
                        }
                        ans += "B";
                    }
                    else if (element == 3)
                    {
                        if (ans.Length >= 1)
                        {
                            ans += " ";
                        }
                        ans += "C";
                    }
                    else if (element == 4)
                    {
                        if (ans.Length >= 1)
                        {
                            ans += " ";
                        }
                        ans += "D";
                    }
                    else
                    {
                        ans = "-";
                    }
                }                
                dgrow.Cells[1].Value = ans;
                dgv_omrresult.Rows.Add(dgrow);
            }                                           
        }

        private Rectangle[] SliceBlock(Rectangle blockrectangle, int slices)
        {
            List<Rectangle> cropRects = new List<Rectangle>();
            Bitmap[] bmps = new Bitmap[slices];
            for (int i = 0; i < slices; i++)
            {
                Rectangle slice = new Rectangle(blockrectangle.X, blockrectangle.Y + (blockrectangle.Height / slices) * i, blockrectangle.Width, blockrectangle.Height / slices);                
                cropRects.Add(slice);
            }
            
            return cropRects.ToArray();
            throw new Exception("Couldn't slice");
        }
       

        
        private void btn_autosize_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.AutoSize;
        }               

        private void btn_clearterminal_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void btn_getimageproperties_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Height: " + picbox_displayomrsheet.Image.Height.ToString() + Environment.NewLine + "Width: " + picbox_displayomrsheet.Image.Width.ToString() + Environment.NewLine + "Horizontal Resolution: " + picbox_displayomrsheet.Image.HorizontalResolution.ToString() + Environment.NewLine + "Vertical Resolution: " + picbox_displayomrsheet.Image.VerticalResolution.ToString());
        }

        private void btn_scansheet_Click(object sender, EventArgs e)
        {
            try
            {
                _twain.SelectSource();
                _settings = new ScanSettings();
                _settings.ShowProgressIndicatorUI = true;
                _settings.Resolution = ResolutionSettings.ColourPhotocopier;
                _twain.StartScanning(_settings);
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
                Enabled = true;
            }
        }

        private void btn_browsefolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {                
                if (dlg.ShowDialog() == DialogResult.OK)
                {                    
                    ts = new TimeSpan(DateTime.Now.Ticks);
                    tb_folderpath.Text = dlg.SelectedPath;   
                    
                    string[] extensions = { ".jpg", ".bmp", ".png", ".jpeg" };
                    string[] imagefilesarray = Directory.GetFiles(dlg.SelectedPath, "*.*")
                                    .Where(f => extensions.Contains(System.IO.Path.GetExtension(f).ToLower())).ToArray();
                   
                    foreach(string file_fbd in imagefilesarray)
                    {                       
                        string fileName = Path.GetFileName(file_fbd);
                        ListViewItem item = new ListViewItem(fileName);
                        item.Tag = file_fbd;
                        lv_displayfiles.Items.Add(item);
                    }
                }
            }            
        }        

        private void loadASheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open OMR Sheet Document";
                dlg.Filter = "Image files | *.bmp; *.jpg; *.jpeg; *.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ts = new TimeSpan(DateTime.Now.Ticks);                  
                    this.actualomrimage = System.Drawing.Image.FromFile(dlg.FileName);
                    picbox_displayomrsheet.Image = actualomrimage;
                    showTimeStamp("OMR sheet Loaded");
                }
            }
        }

        private void readSingleSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_processomrsheet_Click(sender, e);
        }

        private void autoSizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picbox_displayomrsheet.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Bitmap Image (.bmp)|*.bmp|Jpg Image (.jpg)|*.jpg";
                sfd.DefaultExt = "bmp";
                ImageFormat format = ImageFormat.Png;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    switch (sfd.FilterIndex)
                    {
                        case 1:
                            format = ImageFormat.Jpeg;
                            break;
                        case 2:
                            format = ImageFormat.Bmp;
                            break;
                    }
                    picbox_displayomrsheet.Image.Save(sfd.FileName, format);
                }
            }
        }

        private void scanSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _twain.SelectSource();
                _settings = new ScanSettings();
                _settings.ShowProgressIndicatorUI = true;
                _settings.Resolution = ResolutionSettings.ColourPhotocopier;
                _twain.StartScanning(_settings);
            }
            catch (TwainException ex)
            {
                MessageBox.Show(ex.Message);
                Enabled = true;
            }
        }

        private void btn_createanssheet_Click(object sender, EventArgs e)
        {
            using(AnswerMaker ansmaker = new AnswerMaker())            
            {
                var result = ansmaker.ShowDialog();
                if(result == System.Windows.Forms.DialogResult.OK)
                {
                    tb_linkanssheet.Text = ansmaker.returnval_ansdest;
                }
            }
        }

        private void btn_browseanssheet_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open OMR Answer Sheet";
                dlg.Filter = "OMR Answer Sheet Files | *.xml";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    tb_linkanssheet.Text = dlg.FileName;
                }
            }
        }
        
        private void LoadAnswerSheetFile(string filedest)
        {
            if (anssheet_answers.Count > 0)
            {
                anssheet_answers.Clear();
            }
            XmlReader xmlreader = XmlReader.Create(filedest);

            if (xmlreader.IsStartElement("OMRAnswers"))
            {
                while (xmlreader.Read())
                {
                    if (xmlreader.IsStartElement())
                    {
                        switch (xmlreader.Name)
                        {
                            case "TotalQuestions":
                                this.anssheet_totalques = Convert.ToInt32(xmlreader.ReadString());
                                break;

                            case "MarkPerAttempted":
                                this.anssheet_markperattempt = Convert.ToDouble(xmlreader.ReadString());
                                break;

                            case "MarkPerUnAttempted":
                                this.anssheet_markperunattempt = Convert.ToDouble(xmlreader.ReadString());
                                break;

                            case "NegativeMarking":
                                this.anssheet_negmark = Convert.ToDouble(xmlreader.ReadString());
                                break;

                            case "Answers":
                                while (xmlreader.Read())
                                {
                                    if (xmlreader.IsStartElement() && (xmlreader.NodeType == XmlNodeType.Element) && (xmlreader.Name == "Question"))
                                    {
                                        anssheet_answers.Add(Tuple.Create(Convert.ToInt32(xmlreader.GetAttribute("number")), xmlreader.GetAttribute("answer").ToString()));
                                    }
                                }
                                break;
                        }
                    }
                }
                lbl_attemptedans.Text = "(+ve) " + this.anssheet_markperattempt.ToString();
                lbl_unattemptedans.Text = "(-ve) " + this.anssheet_markperunattempt.ToString();
                lbl_wrongans.Text = "(-ve) "+this.anssheet_negmark.ToString();
            }
            else
            {
                MessageBox.Show("Invalid AnswerSheet File.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        

        private void btn_checkanswers_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_linkanssheet.Text))
            {
                MessageBox.Show("Please Link an Answer Sheet File", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(resultlist_dupmerge.Count == 0)
            {
                MessageBox.Show("Please acquire the candidate's checked answer first, by clicking the 'Read OMR Sheet' button", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                LoadAnswerSheetFile(tb_linkanssheet.Text);
                PerformAnswerChecking();
            }
        }

        private void PerformAnswerChecking()
        {
            int maxques_tickedresult = resultlist_dupmerge.Max(i => i.Key);

            if (anssheet_totalques >= maxques_tickedresult)
            {
                int correctcount = 0, incorrectcount = 0; 

                if (dgv_omrresult.Rows.Count > 0)
                    dgv_omrresult.Rows.Clear();

                foreach (var group in resultlist_dupmerge)
                {
                    DataGridViewRow dgrow = new DataGridViewRow();
                    dgrow.CreateCells(dgv_omrresult);
                    dgrow.Cells[0].Value = group.Key.ToString();
                    string ans = "";

                    foreach (var element in group)
                    {
                        if (element == 1)
                        {
                            ans += "A";
                        }
                        else if (element == 2)
                        {
                            if (ans.Length >= 1)
                            {
                                ans += " ";
                            }
                            ans += "B";
                        }
                        else if (element == 3)
                        {
                            if (ans.Length >= 1)
                            {
                                ans += " ";
                            }
                            ans += "C";
                        }
                        else if (element == 4)
                        {
                            if (ans.Length >= 1)
                            {
                                ans += " ";
                            }
                            ans += "D";
                        }
                        else
                        {
                            ans = "-";
                        }
                    }
                    dgrow.Cells[1].Value = ans;
                    dgrow.Cells[2].Value = anssheet_answers[(group.Key - 1)].Item2.ToString();

                    if ((anssheet_answers[(group.Key - 1)].Item2.ToString()) == ans)
                    {
                        dgrow.Cells[3].Value = imagelist_ansresult.Images[0]; //correct answer
                        correctcount++;
                    }
                    else
                    {
                        dgrow.Cells[3].Value = imagelist_ansresult.Images[1]; //incorrect answer
                        incorrectcount++;
                    }                                        
                    dgv_omrresult.Rows.Add(dgrow);                  
                }

                tb_totalques.Text = anssheet_totalques.ToString();
                tb_totalattemptques.Text = resultlist_dupmerge.Count.ToString();
                tb_totalunattemptques.Text = (anssheet_totalques - resultlist_dupmerge.Count).ToString();
                tb_totalcorrectans.Text = correctcount.ToString();
                tb_totalincorrectans.Text = incorrectcount.ToString();

                //marks = (TotalCorrectAnswers * MarkPerAttemptedAns) - (TotalUnattempted * MarkPerUnattemptedAns) - (TotalIncorrect * MarkPerWrongAns)
                double markscalculated = (correctcount * anssheet_markperattempt) - ((anssheet_totalques - resultlist_dupmerge.Count) * anssheet_markperunattempt) - (incorrectcount * anssheet_negmark);
                tb_markobtain.Text = markscalculated.ToString();
                tb_marktotal.Text = ((double)(anssheet_totalques * anssheet_markperattempt)).ToString();
            }
            else
            {
                MessageBox.Show("Cannot check the answers due to a mismatch detected between the answersheet file and the candidate ticked answers. Please Link an appropriate answersheet file.", "Error Checking", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }            
        }

        private void btn_openomrdocument_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open OMR Sheet Document";
                dlg.Filter = "Image files | *.bmp; *.jpg; *.jpeg; *.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ts = new TimeSpan(DateTime.Now.Ticks);
                    tb_showomrfilepath.Text = dlg.FileName;
                    this.actualomrimage = System.Drawing.Image.FromFile(dlg.FileName);
                    picbox_displayomrsheet.Image = actualomrimage;
                    showTimeStamp("OMR sheet Loaded");
                }
            }
        }               

        private void lv_displayfiles_DoubleClick(object sender, EventArgs e)
        {
            picbox_displayomrsheet.ImageLocation = lv_displayfiles.SelectedItems[0].Tag.ToString();
        }

        private void tsmenuitm_autosize_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void tsmenuitm_center_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btn_printcorrectedsheet_Click(object sender, EventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pdlg.Document = pd;

            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.PrintPage += (_, e_print) =>
                {
                    Bitmap bm = new Bitmap(600,840);                    
                    Graphics g = Graphics.FromImage(bm);
                    foreach (DataGridViewRow dgrow in dgv_omrresult.Rows)
                    {
                        Point quesloc = (from pair in list_queslocation
                                         where pair.Item1 == Convert.ToInt32(dgrow.Cells[0].Value)
                                         select pair.Item2).First();

                        if (dgrow.Cells[1].Value.ToString() == dgrow.Cells[2].Value.ToString())
                        {
                            g.DrawImage(imagelist_ansresult.Images[0], new Rectangle(quesloc.X + 100, quesloc.Y, 24, 24));
                        }
                        else
                        {                            
                            g.DrawImage(imagelist_ansresult.Images[1], new Rectangle(quesloc.X + 100, quesloc.Y, 24, 24));
                        }
                    }

                    // This uses a 50 pixel margin - adjust as needed
                    e_print.Graphics.DrawImage(bm, new Point(5,5));                    
                };

                try
                {                    
                    pd.Print();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }
            }
        }       

        public Bitmap Superimpose(Bitmap firstbmp, Bitmap secondbmp)
        {
            Graphics g = Graphics.FromImage(firstbmp);
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
            secondbmp.MakeTransparent();
            int margin = 5;
            int x = firstbmp.Width - secondbmp.Width - margin;
            int y = firstbmp.Height - secondbmp.Height - margin;
            g.DrawImage(secondbmp, new Point(x,y));
            return firstbmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            PrintDialog pdlg = new PrintDialog();
            PrintDocument pd = new System.Drawing.Printing.PrintDocument();
            pdlg.Document = pd;                        
            if (pdlg.ShowDialog() == DialogResult.OK)
            {
                pd.PrintPage += (_, e_print) =>
                {
                    float yPos = 0f;                    
                    float leftMargin = e_print.MarginBounds.Left;
                    float topMargin = e_print.MarginBounds.Top;
                    PaperSize papersize = new PaperSize();
                    papersize.RawKind = (int)PaperKind.A4;
                    pd.DefaultPageSettings.PaperSize = papersize;
                    pd.PrinterSettings.DefaultPageSettings.PaperSize = papersize;                    
                    Bitmap bm = new Bitmap(picbox_displayomrsheet.Image.Width, picbox_displayomrsheet.Image.Height);
                    Graphics g = Graphics.FromImage(bm);
                    foreach (DataGridViewRow dgrow in dgv_omrresult.Rows)
                    {
                        Point quesloc = (from pair in list_queslocation
                                         where pair.Item1 == Convert.ToInt32(dgrow.Cells[0].Value)
                                         select pair.Item2).First();

                        if (dgrow.Cells[1].Value.ToString() == dgrow.Cells[2].Value.ToString())
                        {                            
                            g.DrawImage(imagelist_ansresult.Images[0], new Rectangle(quesloc.X + 90, quesloc.Y+30, 25, 25));
                        }
                        else
                        {
                            g.DrawImage(imagelist_ansresult.Images[1], new Rectangle(quesloc.X + 90, quesloc.Y+30, 25, 25));
                        }
                    }
                    //picbox_displayomrsheet.Image = bm;
                    // This uses a 50 pixel margin - adjust as needed
                    e_print.Graphics.DrawImage(bm, 10f+leftMargin, yPos+topMargin+10f);
                };

                try
                {
                    pd.Print();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex);
                }
            }


            
        }
    }
}
