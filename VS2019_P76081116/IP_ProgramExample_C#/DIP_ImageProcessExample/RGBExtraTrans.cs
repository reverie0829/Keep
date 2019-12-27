using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class RGBExtraTrans
    {
        TabControl tabControl1 = new TabControl();        

        public RGBExtraTrans(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image)
        {
            List<string> problem = new List<string> { "R channel", "G channel", "B channel", "gray scale" };
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            foreach (string item in problem)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom; //隨著放大縮小

                TabPage tabPage = new TabPage(item);
                tabPage.Controls.Add(pictureBox);
                this.tabControl1.TabPages.Add(tabPage);

                if (item.Contains("R channel")) answer.Add(item, ColorExtractionR(image));
                if (item.Contains("G channel")) answer.Add(item, ColorExtractionG(image));
                if (item.Contains("B channel")) answer.Add(item, ColorExtractionB(image));
                if (item.Contains("gray scale")) answer.Add(item, ColorTransToGray(image));

                pictureBox.Image = answer[item];                
            }
            return answer;
        }

        private Bitmap ColorExtractionR(Image image)
        {
            Bitmap bimage = new Bitmap(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);                    
                    bimage.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));                    
                }
            }            
            return bimage;
        }

        private Bitmap ColorExtractionG(Image image)
        {
            Bitmap bimage = new Bitmap(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);                    
                    bimage.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));
                }
            }
            return bimage;
        }

        private Bitmap ColorExtractionB(Image image)
        {
            Bitmap bimage = new Bitmap(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);                    
                    bimage.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));
                }
            }
            return bimage;
        }

        private Bitmap ColorTransToGray(Image image)
        {
            Bitmap bimage = new Bitmap(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);
                    int invAvg = (RGB.R + RGB.G + RGB.B) / 3;
                    bimage.SetPixel(x, y, Color.FromArgb(invAvg, invAvg, invAvg));
                }
            }
            return bimage;
        }

        public int[,,] ColorExtractionRGB(Bitmap image)
        {
            int[,,] rgbImg = new int[image.Height, image.Width, 3];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = image.GetPixel(x, y);
                    rgbImg[y, x, 0] = RGB.R;
                    rgbImg[y, x, 1] = RGB.G;
                    rgbImg[y, x, 2] = RGB.B;
                }
            }
            return rgbImg;
        }
    }
}