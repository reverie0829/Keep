using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class SmoothFilter
    {
        int filterSize = 3;
        TabControl tabControl1 = new TabControl();

        public SmoothFilter(TabControl tabControl)
        {
            this.tabControl1 = tabControl;            
        }
        
        public Dictionary<string, Image> Answer(Image image)
        {
            List<string> problem = new List<string> { "mean", "median" };
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            foreach (string item in problem)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                TabPage tabPage = new TabPage(item);
                tabPage.Controls.Add(pictureBox);
                this.tabControl1.TabPages.Add(tabPage);

                if (item.Contains("mean")) answer.Add(item, Mean(image));
                if (item.Contains("median")) answer.Add(item, Median(image));

                pictureBox.Image = answer[item];
            }
            return answer;
        }
        
        private Bitmap Mean(Image image)
        {
            Bitmap bimage = new Bitmap(image);            
            int[,,] rgbImg = new int[image.Height, image.Width, 3];

            RGBExtraTrans rgbObj = new RGBExtraTrans(this.tabControl1);
            rgbImg = rgbObj.ColorExtractionRGB(bimage);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    int invR = 0, invG = 0, invB = 0;
                    foreach (int item in Neighbor(rgbImg, x, y, 0)) invR += item / (this.filterSize * this.filterSize);
                    foreach (int item in Neighbor(rgbImg, x, y, 1)) invG += item / (this.filterSize * this.filterSize);
                    foreach (int item in Neighbor(rgbImg, x, y, 2)) invB += item / (this.filterSize * this.filterSize);
                    bimage.SetPixel(x, y, Color.FromArgb(invR, invG, invB));
                }
            }
            return bimage;
        }

        private Bitmap Median(Image image)
        {
            Bitmap bimage = new Bitmap(image);            
            int[,,] rgbImg = new int[image.Height, image.Width, 3];
            int[] pixel = new int[this.filterSize * this.filterSize];

            RGBExtraTrans rgbObj = new RGBExtraTrans(this.tabControl1);
            rgbImg = rgbObj.ColorExtractionRGB(bimage);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    pixel = Neighbor(rgbImg, x, y, 0);
                    int invR = pixel[4];
                    pixel = Neighbor(rgbImg, x, y, 1);
                    int invG = pixel[4];
                    pixel = Neighbor(rgbImg, x, y, 2);
                    int invB = pixel[4];
                    bimage.SetPixel(x, y, Color.FromArgb(invR, invG, invB));
                }
            }
            return bimage;
        }

        private int[] Neighbor(int[,,] rgbImg, int x, int y, int z)
        {
            int index = 0;
            int[] pixel = new int[this.filterSize * this.filterSize];

            for (int i = 0; i < this.filterSize; i++)
            {                
                for (int j = 0; j < this.filterSize; j++)
                {                    
                    pixel[index ++] = rgbImg[y + Convert.ToInt32(Math.Pow(-1, i)), x + Convert.ToInt32(Math.Pow(-1, j)), z];
                }
            }
            Array.Sort(pixel);
            return pixel;
        }
    }
}
