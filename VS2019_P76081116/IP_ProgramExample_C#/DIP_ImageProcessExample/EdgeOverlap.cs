using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class EdgeOverlap
    {
        TabControl tabControl1 = new TabControl();

        public EdgeOverlap(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image, Image oriImg, int number)
        {            
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            TabPage tabPage = new TabPage("threshold");
            tabPage.Controls.Add(pictureBox);
            this.tabControl1.TabPages.Add(tabPage);

            answer.Add("threshold", Threshold(image, oriImg, number));
            pictureBox.Image = answer["threshold"];
            return answer;
        }

        private Bitmap Threshold(Image image, Image oriImg, int number)
        {
            Bitmap bimage = new Bitmap(image);
            Bitmap borimg = new Bitmap(oriImg);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color imgRGB = bimage.GetPixel(x, y);
                    Color oriRGB = borimg.GetPixel(x, y);

                    int temp = (oriRGB.R + oriRGB.G + oriRGB.B) / 3;
                    if (temp >= number && imgRGB.R == 255) borimg.SetPixel(x, y, Color.Lime);                    
                }
            }
            return borimg;
        }
    }
}
