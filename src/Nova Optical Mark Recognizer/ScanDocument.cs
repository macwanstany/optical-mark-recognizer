using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TwainDotNet;
using TwainDotNet.WinFroms;
using TwainDotNet.TwainNative;
using System.Drawing.Imaging;

namespace Nova_Optical_Mark_Recognizer
{    
    public partial class ScanDocument : Form
    {      
        private static AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);

        Twain _twain;
        ScanSettings _settings;

        public static Image scannedimage;        

        public ScanDocument()
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
        }

        private void btn_scanimage_Click(object sender, EventArgs e)
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

        private void btn_proceed_Click(object sender, EventArgs e)
        {
            scannedimage = picbox_displayomrsheet.Image;
            ProcessOMR procOMR = new ProcessOMR();            
            procOMR.Show();
            this.Hide();             
        }

        private void btn_saveimage_Click(object sender, EventArgs e)
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

        private void btn_rotateleft_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
            picbox_displayomrsheet.Refresh();
        }

        private void btn_rotateright_Click(object sender, EventArgs e)
        {
            picbox_displayomrsheet.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picbox_displayomrsheet.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Open OMR Sheet Document";
                dlg.Filter = "Image files | *.jpg; *.jpeg; *.png";
                if (dlg.ShowDialog() == DialogResult.OK)
                {                    
                    picbox_displayomrsheet.Image = System.Drawing.Image.FromFile(dlg.FileName);                    
                }
            }
        }       
    }
}
