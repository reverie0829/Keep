using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class StretchingInVertical
    {
        TabControl tabControl1 = new TabControl();

        public StretchingInVertical(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image, int number)
        {
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            TabPage tabPage = new TabPage("StretchingInVertical");
            tabPage.Controls.Add(pictureBox);
            this.tabControl1.TabPages.Add(tabPage);

            answer.Add("StretchingInVertical", stretchinginhvertical(image, number));
            pictureBox.Image = answer["StretchingInVertical"];
            return answer;
        }

        private Bitmap stretchinginhvertical(Image image, int number)
        {
            Bitmap bimage = new Bitmap(image);
            //原圖的寬和高
            int w = bimage.Width;
            int h = bimage.Height;

            if (number == 0) number = 1;
            if (number > 10) number = 10;
            Bitmap dsImage = new Bitmap(w , h * number);
            Graphics g = Graphics.FromImage(dsImage);//取出畫布
            g.DrawImage(image, new Point[] { new Point(0, 0), new Point(w , 0), new Point(0, h * number) });

            return dsImage;
        }
    }
}
