using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sem_Supervised_Sites_PartB;

//aaaaa
namespace Sem_Supervised_Sites_PartB
{
    public partial class Form4 : Form
    {

        private DateTime date2;
        private TimeSpan duration;

        //remove that crap after
        public struct vectorNode
        {
            public double[] vector;
            public string name;
        }

        public struct tmpClust
        {
            public LinkedList<vectorNode> relatedPoints;
            public string clusterName;
        }


        private tmpClust[] tmpCluster;
        //remove that crap after

        private int clustNum;

        public static Form4 FourStaticVar;
        private Dictionary<string, LinkedList<double[]>> dic;
        public Form4(Dictionary<string, LinkedList<double[]>> d, int num)
        {
            this.dic = d;
            this.clustNum = num;

            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            string k;


            //remove that crap after
            double[] vec0 = new double[2] { -4, 3 };
            double[] vec1 = new double[2] { -3, 4 };
            double[] vec2 = new double[2] { 2, -1 };
            double[] vec3 = new double[2] { -3, 1 };
            double[] vec4 = new double[2] { 4, -2 };


            tmpCluster = new tmpClust[2];

            tmpCluster[0].clusterName = "Cluster 0";
            tmpCluster[0].relatedPoints = new LinkedList<vectorNode>();
            tmpCluster[1].clusterName = "Cluster 1";
            tmpCluster[1].relatedPoints = new LinkedList<vectorNode>();
            vectorNode tmpVector0 = new vectorNode();
            tmpVector0.name = "site 0";
            tmpVector0.vector = vec0;

            vectorNode tmpVector1 = new vectorNode();
            tmpVector1.name = "site 1";
            tmpVector1.vector = vec1;

            vectorNode tmpVector3 = new vectorNode();
            tmpVector3.name = "site 3";
            tmpVector3.vector = vec3;

            tmpCluster[0].relatedPoints.AddFirst(tmpVector0);
            tmpCluster[0].relatedPoints.AddFirst(tmpVector1);
            
            //tmpCluster[0].relatedPoints.AddFirst(tmpVector3);

            vectorNode tmpVector2 = new vectorNode();
            tmpVector2.name = "site 2";
            tmpVector2.vector = vec2;
            vectorNode tmpVector4 = new vectorNode();
            tmpVector4.name = "site 4";
            tmpVector4.vector = vec4;
            tmpCluster[1].relatedPoints.AddFirst(tmpVector2);
            tmpCluster[1].relatedPoints.AddFirst(tmpVector4);
            
            tmpCluster[1].relatedPoints.AddFirst(tmpVector3);



            //remove that crap after    
            //Create chart
            foreach (string key in dic.Keys.ToList())
            {
                k = "Cluster " + key;
                chart1.Series.Add(k).ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                foreach (double[] temp in dic[key])
                {
                    chart1.Series[k].Points.AddXY(temp[0], temp[1]);
                }
            }

            DataGridViewRow dataGridRow;
            DataGridViewTextBoxCell userAsign;
            DataGridViewTextBoxCell clustRes;
            DataGridViewTextBoxCell siteName;

            double greenCount = 0;
            double redCount = 0;
            double percent = 0;

            //key = 0..clust_num-1
            foreach (string key in dic.Keys.ToList())
            {
                foreach (double[] temp in dic[key])
                {

                    dataGridRow = new DataGridViewRow();

                    userAsign = new DataGridViewTextBoxCell();
                    clustRes = new DataGridViewTextBoxCell();
                    siteName = new DataGridViewTextBoxCell();


                    //find temp[] in tmpCluster[p].relatedPoints. Print p and name
                    for (int p = 0; p < clustNum; p++)
                    {
                        foreach (vectorNode tempVectorNode in tmpCluster[p].relatedPoints)
                            if (Tools.Equals(temp, tempVectorNode.vector))
                            {
                                userAsign.Value = tmpCluster[p].clusterName;
                                siteName.Value = tempVectorNode.name;
                            }
                    }
                    clustRes.Value = tmpCluster[Convert.ToInt32(key)].clusterName;


                    dataGridRow.Cells.Add(userAsign);
                    dataGridRow.Cells.Add(clustRes);
                    dataGridRow.Cells.Add(siteName);

                    userAsign.ReadOnly = true;
                    clustRes.ReadOnly = true;
                    siteName.ReadOnly = true;

                    if (String.Equals(clustRes.Value, userAsign.Value))
                    {
                        dataGridRow.Cells[0].Style.BackColor = Color.LightGreen;
                        dataGridRow.Cells[1].Style.BackColor = Color.LightGreen;
                        dataGridRow.Cells[2].Style.BackColor = Color.LightGreen;
                        greenCount++;
                    }
                    else
                    {
                        dataGridRow.Cells[0].Style.BackColor = Color.Red;
                        dataGridRow.Cells[1].Style.BackColor = Color.Red;
                        dataGridRow.Cells[2].Style.BackColor = Color.Red;
                        redCount++;
                    }

                    dataGridRow.Resizable = DataGridViewTriState.False;
                    dataGridView2.Rows.Add(dataGridRow);
                }
            }
            percent = (greenCount / (redCount+greenCount)) * 100;
            this.label3.Text = "Precision Rate  : "+percent+"%";
            date2 = DateTime.Now;
            duration = date2 - Form3.date1;
            double timeInSec = ((double)(duration.Milliseconds))/1000;
            this.label2.Text = "Algorithm Run-Time : " + timeInSec + " Seconds";
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //this.Close();
            //Form3.ThirStaticVar.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.FirStaticVar.Close();
        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
       
        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
