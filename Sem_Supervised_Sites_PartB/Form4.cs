using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sem_Supervised_Sites_PartB;


namespace Sem_Supervised_Sites_PartB
{
    public partial class Form4 : Form
    {

        private DateTime date2;
        private TimeSpan duration;

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

        private int clustNum;

        public static Form4 FourStaticVar;
        private Dictionary<string, LinkedList<double[]>> dic;
        public Form4(Dictionary<string, LinkedList<double[]>> d, int num)
        {
            this.dic = d;
            this.clustNum = num;

            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();

            this.dataGridView2.Columns[0].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.Columns[1].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.Columns[2].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowsDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowsDefaultCellStyle.ForeColor = Color.Black;




            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.ClearSelection();
            string k;
            int index;

            //Create chart
            foreach (string key in dic.Keys.ToList())
            {
                Int32.TryParse(key,out index);
                k = Form1.FirStaticVar.tmpCluster[index].clusterName;
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
                        foreach (Sem_Supervised_Sites_PartB.Form1.vectorNode tempVectorNode in Form1.FirStaticVar.tmpCluster[p].relatedPoints)
                            if (Tools.Equals(temp, tempVectorNode.vector))
                            {
                                userAsign.Value = Form1.FirStaticVar.tmpCluster[p].clusterName;
                                siteName.Value = tempVectorNode.name;
                            }
                    }
                    clustRes.Value = Form1.FirStaticVar.tmpCluster[Convert.ToInt32(key)].clusterName;

                    userAsign.Selected = false;
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
                    dataGridView2.ClearSelection();
                    

                }
            }
            percent = (greenCount / (redCount+greenCount)) * 100;
            this.label3.Text = "Precision Rate  : "+percent+"%";
            date2 = DateTime.Now;
            duration = (date2 - Form3.date1)+Form2.duration2;
            this.label2.Text = "Algorithm Run-Time : " + duration.Seconds + "." + duration.Milliseconds + " Seconds";
            dataGridView2.ClearSelection();
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1.StatPanel3.Show();
            Form1.StatPanel3.BringToFront();
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

        private void button2_Click(object sender, EventArgs e)
        {
            hist histgr = new hist(dic, clustNum);
            histgr.Show();
        }

        protected override void OnShown(EventArgs e)
        {
            
                for (int i = 0; i < this.dataGridView2.SelectedCells.Count; i++)
                    this.dataGridView2.SelectedCells[i].Selected = false;
            
        } 



    }
}
