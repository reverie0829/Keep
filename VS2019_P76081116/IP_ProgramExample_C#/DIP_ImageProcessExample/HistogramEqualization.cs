using System;
using ZedGraph;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DIP_ImageProcessExample
{
    class HistogramEqualization
    {
        TabControl tabControl1 = new TabControl();

        public HistogramEqualization(TabControl tabControl)
        {
            this.tabControl1 = tabControl;
        }

        public Dictionary<string, Image> Answer(Image image)
        {
            List<string> problem = new List<string> { "before-histogram", "equalization", "after-histogram" };
            Dictionary<string, Image> answer = new Dictionary<string, Image>();

            foreach (string item in problem)
            {                
                PictureBox pictureBox = new PictureBox();
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                TabPage tabPage = new TabPage(item);
                tabPage.Controls.Add(pictureBox);
                this.tabControl1.TabPages.Add(tabPage);
                
                if (item.Contains("before-histogram")) Histogram(image, pictureBox);
                if (item.Contains("equalization")) answer.Add(item, Equalization(image));
                if (item.Contains("after-histogram")) Histogram(answer["equalization"], pictureBox);

                if (answer.ContainsKey(item)) pictureBox.Image = answer[item];
            }
            return answer;
        }        

        private void Histogram(Image image, PictureBox pictureBox)
        {
            Dictionary<int, double> pixel = new Dictionary<int, double>();
            pixel = PDF(image);

            PrintHistogram(pixel, pictureBox);
        }

        private Dictionary<int, double> PDF(Image image)
        {
            Bitmap bimage = new Bitmap(image);
            Dictionary<int, double> pixel = new Dictionary<int, double>();
            Dictionary<int, double> sortPixel = new Dictionary<int, double>();

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);
                    if (!pixel.ContainsKey(RGB.R)) pixel.Add(RGB.R, 0);
                    pixel[RGB.R] += 1;
                }
            }
            sortPixel = pixel.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            return sortPixel;
        }

        private void PrintHistogram(Dictionary<int, double> pixel, PictureBox pictureBox)
        {
            ZedGraphControl zedGraphControl = new ZedGraphControl();            
            zedGraphControl.Dock = DockStyle.Fill;
            zedGraphControl.IsSynchronizeXAxes = true;
            zedGraphControl.GraphPane.Title.IsVisible = false;            
            zedGraphControl.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl.GraphPane.XAxis.MajorGrid.IsVisible = true;
            zedGraphControl.GraphPane.YAxis.MajorGrid.IsVisible = true;

            PointPairList pointPairList = new PointPairList();
            foreach (int item in pixel.Keys) pointPairList.Add(item, pixel[item]);

            GraphPane graphPane = zedGraphControl.GraphPane;
            BarItem barItem = graphPane.AddBar("--", pointPairList, Color.Blue);
            barItem.Label.IsVisible = false;

            zedGraphControl.RestoreScale(graphPane);
            pictureBox.Controls.Add(zedGraphControl);
        }       
        
        private Bitmap Equalization(Image image)
        {
            Bitmap bimage = new Bitmap(image);
            Dictionary<int, double> pdfPixel = new Dictionary<int, double>();
            pdfPixel = PDF(image);

            Dictionary<int, double> cdfPixel = new Dictionary<int, double>();
            cdfPixel = CDF(pdfPixel);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = bimage.GetPixel(x, y);
                    int invAvg = Convert.ToInt32(cdfPixel[RGB.R]);
                    bimage.SetPixel(x, y, Color.FromArgb(invAvg, invAvg, invAvg));
                }
            }
            return bimage;
        }

        private Dictionary<int, double> CDF(Dictionary<int, double> pdfPixel)
        {
            Dictionary<int, double> cdfPixel = new Dictionary<int, double>();
            foreach (int item in pdfPixel.Keys)
            {
                if (cdfPixel.Count == 0) cdfPixel.Add(pdfPixel.Keys.First(), pdfPixel.Values.First());
                else cdfPixel.Add(item, pdfPixel[item] + cdfPixel.Values.Last());
            }

            Dictionary<int, double> equPixel = new Dictionary<int, double>();
            foreach (int item in cdfPixel.Keys)
            {
                if (equPixel.Count == 0) { equPixel.Add(item, 0); continue; }
                double temp = (cdfPixel[item] - cdfPixel.Values.Min()) / (cdfPixel.Values.Max() - cdfPixel.Values.Min());
                equPixel.Add(item, Math.Round(temp * 255));
            }
            return equPixel;
        }
    }
}
