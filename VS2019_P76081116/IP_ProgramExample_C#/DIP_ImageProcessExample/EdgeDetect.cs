using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class EdgeDetect
    {
        TabControl tabControl1 = new TabControl();

        public EdgeDetect(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image)
        {
            List<string> problem = new List<string> { "vertical", "horizontal", "combined" };
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            foreach (string item in problem)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                TabPage tabPage = new TabPage(item);
                tabPage.Controls.Add(pictureBox);
                this.tabControl1.TabPages.Add(tabPage);

                if (item.Contains("vertical")) answer.Add(item, Vertical(image));
                if (item.Contains("horizontal")) answer.Add(item, Horizontal(image));
                if (item.Contains("combined")) answer.Add(item, Combined(image));

                pictureBox.Image = answer[item];
            }
            return answer;
        }

        private Bitmap Vertical(Image image)
        {
            Bitmap bimage = new Bitmap(image);
            int[,,] rgbImg = new int[image.Height, image.Width, 3];
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            RGBExtraTrans rgbObj = new RGBExtraTrans(this.tabControl1);
            rgbImg = rgbObj.ColorExtractionRGB(bimage);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        int G = 0;
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                G += gy[i + 1, j + 1] * rgbImg[y + j, x + i, z];
                            }
                        }
                        if (G > 128) bimage.SetPixel(x, y, Color.White);
                    }
                }
            }
            return bimage;
        }

        private Bitmap Horizontal(Image image)
        {
            Bitmap bimage = new Bitmap(image);
            int[,,] rgbImg = new int[image.Height, image.Width, 3];
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };

            RGBExtraTrans rgbObj = new RGBExtraTrans(this.tabControl1);
            rgbImg = rgbObj.ColorExtractionRGB(bimage);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        int G = 0;
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                G += gx[i + 1, j + 1] * rgbImg[y + j, x + i, z];
                            }
                        }
                        if (G > 128) bimage.SetPixel(x, y, Color.White);
                    }
                }
            }
            return bimage;            
        }

        private Bitmap Combined(Image image)
        {
            Bitmap bimage = new Bitmap(image);
            int[,,] rgbImg = new int[image.Height, image.Width, 3];
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            RGBExtraTrans rgbObj = new RGBExtraTrans(this.tabControl1);
            rgbImg = rgbObj.ColorExtractionRGB(bimage);

            for (int y = 1; y < image.Height - 1; y++)
            {
                for (int x = 1; x < image.Width - 1; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        int Gx = 0, Gy = 0;
                        for (int i = -1; i < 2; i++)
                        {
                            for (int j = -1; j < 2; j++)
                            {
                                Gx += gx[i + 1, j + 1] * rgbImg[y + j, x + i, z];
                                Gy += gy[i + 1, j + 1] * rgbImg[y + j, x + i, z];
                            }
                        }
                        if ((Math.Pow(Gx, 2) + Math.Pow(Gy, 2)) > Math.Pow(128, 2)) bimage.SetPixel(x, y, Color.White);
                    }
                }
            }
            return bimage;
        }        
    }
}
