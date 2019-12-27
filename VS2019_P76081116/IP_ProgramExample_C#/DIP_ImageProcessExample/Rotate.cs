using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Drawing.Drawing2D;

namespace DIP_ImageProcessExample
{
    class Rotate
    {
        TabControl tabControl1 = new TabControl();

        public Rotate(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image, int angle)
        {
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            TabPage tabPage = new TabPage("rotate");
            tabPage.Controls.Add(pictureBox);
            this.tabControl1.TabPages.Add(tabPage);

            answer.Add("rotate", Rotating(image, angle));
            pictureBox.Image = answer["rotate"];
            return answer;
        }

        private Bitmap Rotating(Image image, int angle)
        {
            Bitmap bimage = new Bitmap(image);
            angle = angle % 360;

            //弧度轉換
            double radian = angle * Math.PI / 180.0;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            //原圖的寬和高
            int w = bimage.Width;
            int h = bimage.Height;
            int W = (int)(Math.Max(Math.Abs(w * cos - h * sin), Math.Abs(w * cos + h * sin)));
            int H = (int)(Math.Max(Math.Abs(w * sin - h * cos), Math.Abs(w * sin + h * cos)));

            //目標點陣圖
            Bitmap dsImage = new Bitmap(W, H);
            Graphics g = Graphics.FromImage(dsImage);//取出畫布

            g.InterpolationMode = InterpolationMode.Bilinear;

            g.SmoothingMode = SmoothingMode.HighQuality;

            //計算偏移量
            Point Offset = new Point((W - w) / 2, (H - h) / 2);

            //構造影象顯示區域：讓影象的中心與視窗的中心點一致
            Rectangle rect = new Rectangle(Offset.X, Offset.Y, w, h);
            Point center = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            g.TranslateTransform(center.X, center.Y);
            g.RotateTransform(360 - angle);

            //恢復影象在水平和垂直方向的平移
            g.TranslateTransform(-center.X, -center.Y);
            g.DrawImage(bimage, rect);

            //重至繪圖的所有變換
            g.ResetTransform();

            g.Save();
            g.Dispose();
            return dsImage;
        }
    }
}
