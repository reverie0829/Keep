using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class DefinedThres
    {
        TabControl tabControl1 = new TabControl();

        public DefinedThres(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image, int number)
        {            
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            PictureBox pictureBox = new PictureBox();            
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            TabPage tabPage = new TabPage("threshold");
            tabPage.Controls.Add(pictureBox);
            this.tabControl1.TabPages.Add(tabPage);

            answer.Add("threshold", Threshold(image, number));
            pictureBox.Image = answer["threshold"];
            return answer;
        }

        private Bitmap Threshold(Image image, int number)
        {
            Bitmap bimage = new Bitmap(image);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);
                    if (RGB.R >= number) bimage.SetPixel(x, y, Color.White);
                    else bimage.SetPixel(x, y, Color.Black);
                }
            }
            return bimage;
        }
    }
}
