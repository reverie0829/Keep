using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class StretchingInHorizontal
    {
        TabControl tabControl1 = new TabControl();

        public StretchingInHorizontal(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image, int number)
        {
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            TabPage tabPage = new TabPage("StretchingInHorizontal");
            tabPage.Controls.Add(pictureBox);
            this.tabControl1.TabPages.Add(tabPage);

            answer.Add("StretchingInHorizontal", stretchinginhorizontal(image, number));
            pictureBox.Image = answer["StretchingInHorizontal"];
            return answer;
        }

        private Bitmap stretchinginhorizontal(Image image, int number)
        {
            Bitmap bimage = new Bitmap(image);
            //原圖的寬和高
            int w = bimage.Width;
            int h = bimage.Height;

            if (number == 0) number = 1;
            if (number > 10) number = 10;
            Bitmap dsImage = new Bitmap(w * number, h);
            Graphics g = Graphics.FromImage(dsImage);//取出畫布
            g.DrawImage(image, new Point[] { new Point(0, 0), new Point(w * number, 0), new Point(0, h) });
            
            return dsImage;
        }
    }
}
