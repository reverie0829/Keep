using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    public partial class Form1 : Form
    {
        bool isUndo = false;
        string processed = "Load Image";        
        Dictionary<string, Image> ansTmp = new Dictionary<string, Image>();
        Dictionary<string, Image> ansImg = new Dictionary<string, Image>();

        string ovrTmp;//上上一步的步驟名稱
        Image ovrImg;//上上一步的的圖

        RGBExtraTrans rgbObj;
        SmoothFilter filObj;
        HistogramEqualization hieObj;
        DefinedThres thrObj;
        EdgeDetect dctObj;
        EdgeOverlap ovrObj;
        Rotate rotObj;
        StretchingInHorizontal sihObj;
        StretchingInVertical sivObj;

        public Form1()
        {
            InitializeComponent();
            this.rgbObj = new RGBExtraTrans(this.tabControl1);
            this.filObj = new SmoothFilter(this.tabControl1);
            this.hieObj = new HistogramEqualization(this.tabControl1);
            this.thrObj = new DefinedThres(this.tabControl1);
            this.dctObj = new EdgeDetect(this.tabControl1);
            this.ovrObj = new EdgeOverlap(this.tabControl1);
            this.rotObj = new Rotate(this.tabControl1);
            this.sihObj = new StretchingInHorizontal(this.tabControl1);
            this.sivObj = new StretchingInVertical(this.tabControl1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Initial variables and controls*/
            this.isUndo = false;
            this.ansImg.Clear();
            this.ansTmp.Clear();
            this.processed = "Load Image";

            this.button2.Enabled = false;
            this.button3.Enabled = false;
            this.tabControl1.TabPages.Clear();

            /*Initial Directory is C:*/
            openFileDialog1.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp|Jpeg File(.jpg)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ansImg.Add(this.processed, Image.FromFile(openFileDialog1.FileName));
                this.pictureBox1.Image = this.ansImg[this.processed];
                this.comboBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveImg = new SaveFileDialog();            
            saveImg.Filter = "Jpeg File(.jpg)|*.jpg|Bitmap Files (.bmp)|*.bmp";
            if (saveImg.ShowDialog() == DialogResult.OK)
            {
                this.ansImg.Values.Last().Save(saveImg.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.ansImg.Count < 3) this.button2.Enabled = false;
            if (this.ansImg.Count < 2) this.button3.Enabled = false;
            this.tabControl1.TabPages.Clear();
            this.trackBar1.Enabled = false;            
            this.ansTmp.Clear();
            this.isUndo = true;

            if (ansImg.Count == 1) this.processed = "Load Image";
            else
            {
                string temp = this.ansImg.Keys.Last();
                this.ansImg.Remove(this.ansImg.Keys.Last());

                this.processed = this.ansImg.Keys.Last();
                this.pictureBox1.Image = this.ansImg[this.processed];
                this.comboBox1.Text = temp;
            }
            this.isUndo = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.isUndo == false) UpdateControl();
            if (this.comboBox1.Text.Contains("RGB Extraction & Transformation")) this.ansTmp = this.rgbObj.Answer(this.ansImg[this.processed]);
            if (this.comboBox1.Text.Contains("Smooth Filter")) this.ansTmp = this.filObj.Answer(this.ansImg[this.processed]);
            if (this.comboBox1.Text.Contains("Histogram Equalization")) this.ansTmp = this.hieObj.Answer(this.ansImg[this.processed]);
            if (this.comboBox1.Text.Contains("A User-defined Thresholding")) { this.ansTmp = this.thrObj.Answer(this.ansImg[this.processed], 0); this.trackBar1.Enabled = true; }
            if (this.comboBox1.Text.Contains("Sobel Edge Detection")) this.ansTmp = this.dctObj.Answer(this.ansImg[this.processed]);
            if (this.comboBox1.Text.Contains("Edge Overlapping")) { this.ansTmp = this.ovrObj.Answer(this.ansImg[this.processed], this.ansImg[this.ovrTmp], 0); this.trackBar1.Enabled = true; this.ovrImg = this.ansImg[this.ovrTmp]; }
            if (this.comboBox1.Text.Contains("Image Rotation")) { this.ansTmp = this.rotObj.Answer(this.ansImg[this.processed], 0); this.trackBar1.Enabled = true; }
            if (this.comboBox1.Text.Contains("stretching in horizontal")) { this.ansTmp = this.sihObj.Answer(this.ansImg[this.processed], 1); this.trackBar1.Enabled = true; }
            if (this.comboBox1.Text.Contains("stretching in vertical")) { this.ansTmp = this.sivObj.Answer(this.ansImg[this.processed], 1); this.trackBar1.Enabled = true; }
            this.ovrTmp = this.processed;
            this.processed = this.comboBox1.Text;
        }       

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Clear();
            if (this.processed.Contains("A User-defined Thresholding")) this.ansTmp = this.thrObj.Answer(this.ansImg.Values.Last(), this.trackBar1.Value);
            if (this.processed.Contains("Edge Overlapping")) this.ansTmp = this.ovrObj.Answer(this.ansImg.Values.Last(), this.ovrImg, this.trackBar1.Value);
            if (this.comboBox1.Text.Contains("Image Rotation")) this.ansTmp = this.rotObj.Answer(this.ansImg.Values.Last(), this.trackBar1.Value);
            if (this.comboBox1.Text.Contains("stretching in horizontal")) this.ansTmp = this.sihObj.Answer(this.ansImg.Values.Last(), this.trackBar1.Value);
            if (this.comboBox1.Text.Contains("stretching in vertical")) this.ansTmp = this.sivObj.Answer(this.ansImg.Values.Last(), this.trackBar1.Value);
        }

        private void UpdateControl()
        {
            /*Update processed and Initial Control*/
            if (this.ansTmp.Count != 0) this.ansImg.Add(this.processed, this.ansTmp[this.tabControl1.SelectedTab.Text]);
            if (this.ansImg.Count > 1) this.button2.Enabled = true;
            if (this.ansImg.Count > 0) this.button3.Enabled = true;
            this.pictureBox1.Image = this.ansImg[this.processed];
            this.tabControl1.TabPages.Clear();
            this.trackBar1.Enabled = false;
        }
    }
}
