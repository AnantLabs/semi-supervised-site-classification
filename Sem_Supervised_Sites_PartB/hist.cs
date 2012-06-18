using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Sem_Supervised_Sites_PartB
{
	public partial class hist : Form
	{
        private int clustNum;
        private Dictionary<string, LinkedList<double[]>> dic;
        private string[] labels;
        private double[] y;
        private int[] RGB;
        private int RGBDiv;
        private int RGBPhase;
        
        int[,] brightPastelArray = { { 65, 140, 240 }, { 252, 180, 65 }, { 224, 64, 10 }, { 5, 100, 146 }, { 191, 191, 191 }, { 26, 59, 105 }, { 255, 227, 130 }, { 18, 156, 221 }, { 202, 107, 75 }, { 0, 92, 219 }, { 243, 210, 136 }, { 80, 99, 129 }, { 241, 185, 168 }, { 224, 131, 10 }, { 120, 147, 190 } };

        public hist(Dictionary<string, LinkedList<double[]>> d, int n)
		{
            this.dic = d;
            this.clustNum = n;
            this.RGBDiv = 1;
            RGBPhase = 1;
            RGB = new int[3];
            RGB[0] = 0;
            RGB[1] = 0;
            RGB[2] = 255;
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e )
		{
			CreateGraph( zg1 );
			SetSize();
		}

		private void CreateGraph( ZedGraphControl zgc )
		{
            // get a reference to the GraphPane
            GraphPane myPane = zg1.GraphPane;

            // Set the Titles
            myPane.Title.Text = "Histogram";
            myPane.XAxis.Title.Text = "Cluster assignment";
            myPane.YAxis.Title.Text = "Vector number";

            y = new double[clustNum];
            labels = new string[clustNum];

            for (int p = 0; p < clustNum; p++)
            {
                labels[p] = Form1.FirStaticVar.tmpCluster[p].clusterName;
                foreach (Sem_Supervised_Sites_PartB.Form1.vectorNode tempVectorNode in Form1.FirStaticVar.tmpCluster[p].relatedPoints)
                    foreach (string key in dic.Keys)
                        foreach (double[] temp in dic[key])
                            if (Tools.Equals(temp, tempVectorNode.vector))
                                y[Convert.ToInt32(key)]++;
                BarItem myBar = myPane.AddBar(labels[p], null, y,
                    Color.FromArgb(brightPastelArray[p % 15, 0], brightPastelArray[p % 15, 1], brightPastelArray[p % 15, 2]));
                myBar.Bar.Fill = new Fill(Color.FromArgb(brightPastelArray[p % 15, 0], brightPastelArray[p % 15, 1], brightPastelArray[p % 15, 2]), Color.White,
                    Color.FromArgb(brightPastelArray[p % 15, 0], brightPastelArray[p % 15, 1], brightPastelArray[p % 15, 2]));
                y = new double[clustNum];
                
            }
            
            myPane.XAxis.MajorTic.IsBetweenLabels = true;

            // Set the XAxis labels
            myPane.XAxis.Scale.TextLabels = labels;
            // Set the XAxis to Text type
            myPane.XAxis.Type = AxisType.Text;

            // Fill the Axis and Pane backgrounds
            myPane.Chart.Fill = new Fill(Color.White,
                  Color.FromArgb(255, 255, 166), 90F);
            myPane.Fill = new Fill(Color.FromArgb(250, 250, 255));

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zg1.AxisChange();

		}

        private void generateRGB()
        {
            int temp;
            if (RGBPhase < 3)
            {
                temp = RGB[0] / RGBDiv;
                RGB[0] = RGB[1] / RGBDiv;
                RGB[1] = RGB[2] / RGBDiv;
                RGB[2] = temp;
                RGBPhase++;
            }
            if (RGBPhase == 3)
            {
                RGBDiv++;
                RGBPhase = 1;
            }
        }

		private void Form1_Resize( object sender, EventArgs e )
		{
			SetSize();
		}

		private void SetSize()
		{
			zg1.Location = new Point( 10, 10 );
			// Leave a small margin around the outside of the control
			zg1.Size = new Size( this.ClientRectangle.Width - 20, this.ClientRectangle.Height - 20 );
		}
	}
}