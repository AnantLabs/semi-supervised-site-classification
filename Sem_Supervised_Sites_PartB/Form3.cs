﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sem_Supervised_Sites_PartB;
using Project_Phase_B___Engine_Stopwords;
using Project_Phase_B___Engine_BagofWords;
using PCA_Logic;

namespace Sem_Supervised_Sites_PartB
{
    public partial class Form3 : Panel
    {

        int num_of_words_in_dictionary;
        string[] words_in_dictionary;
 
        

        public static DateTime date1;

        public static Form3 ThirStaticVar;
        
        List<string> _items = new List<string>();
        List<string> _items2 = new List<string>();
        List<string> _items3 = new List<string>();
        List<string> _items4 = new List<string>();
        List<string> _items5 = new List<string>();
        List<string> _items6 = new List<string>();
        List<string> _items7 = new List<string>();


        int[,] brightPastelArray = { { 65, 140, 240 } , {252, 180, 65}, {224, 64, 10}, {5, 100, 146}, {191, 191, 191}, {26, 59, 105}, {255, 227, 130}, {18, 156, 221}, {202, 107, 75}, {0, 92, 219}, {243, 210, 136}, {80, 99, 129}, {241, 185, 168}, {224, 131, 10}, {120, 147, 190} };


        public Form3()
        {
            
            
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView3.AllowUserToAddRows = false;


            this.dataGridView1.Columns[0].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.Columns[1].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.RowsDefaultCellStyle.ForeColor = Color.Black;

            this.dataGridView2.Columns[0].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.Columns[1].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowsDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView2.RowsDefaultCellStyle.ForeColor = Color.Black;

            this.dataGridView3.Columns[0].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.Columns[1].HeaderCell.Style.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.RowsDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.RowsDefaultCellStyle.ForeColor = Color.Black;
        } 

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
       
        private void button5_Click(object sender, EventArgs e)
        {
            date1 = DateTime.Now;


            //Activate MPCK after all info is collected
            MPCKMeans k = new MPCKMeans(Form1.FirStaticVar.getNumOfTopics(), Tools.pca_result, Tools.mLC, Form1.FirStaticVar.getW(), Form1.FirStaticVar.getW_Roof());
            MPCKMeans.cluster[] clust = k.getPoints();
            int num = k.getClustNum();
            
            Dictionary<string, LinkedList<double[]>> dic= new Dictionary<string, LinkedList<double[]>>();

            for (int i = 0; i < num; i++)
            {
                dic.Add(i.ToString(),clust[i].relatedPoints);
            }

            //Create every time new result

            Form4.FourStaticVar = new Form4(dic, num);
            Form4.FourStaticVar.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lineShape2_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape5_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Optionally delete all site lists in GUI2
  
            Form1.StatPanel3.Hide();
            Form1.StatPanel2.Show();
        }

       

        public void Show_Dictionary()
        {

            // Dictionary Table

            this.num_of_words_in_dictionary = Tools.num_of_words_in_dictionary;
            this.words_in_dictionary = Tools.words_in_dictionary;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < this.num_of_words_in_dictionary; i++)
            {
                DataGridViewRow dataGridRow = new DataGridViewRow();
                DataGridViewTextBoxCell txt1A_i1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell txt1A = new DataGridViewTextBoxCell();

                txt1A_i1.Value = i + 1;
                txt1A.Value = words_in_dictionary[i];

                dataGridRow.Cells.Add(txt1A_i1);
                txt1A_i1.ReadOnly = true;
                dataGridRow.Cells.Add(txt1A);
                txt1A.ReadOnly = true;

                dataGridRow.Height = 25;



                dataGridView1.Rows.Add(dataGridRow);

            }

        }

        public void Show_Datagridview2_Sites_Vectors()
        {
            int num_of_topics = Form1.FirStaticVar.getNumOfTopics();
            LinkedList<Sem_Supervised_Sites_PartB.Form1.vectorNode> relatedPoints_temp = new LinkedList<Sem_Supervised_Sites_PartB.Form1.vectorNode>();
            

            dataGridView2.Rows.Clear();
            relatedPoints_temp.Clear();

            for (int i = 0; i < num_of_topics; i++)
            {
                relatedPoints_temp = Form1.FirStaticVar.tmpCluster[i].relatedPoints;

                foreach (Sem_Supervised_Sites_PartB.Form1.vectorNode vectorNodeTmp in relatedPoints_temp)
                {
                    DataGridViewRow dataGridRow = new DataGridViewRow();
                    DataGridViewTextBoxCell txt2A_i2 = new DataGridViewTextBoxCell();

                    txt2A_i2.Value = vectorNodeTmp.name;

                    DataGridViewTextBoxCell txt2A = new DataGridViewTextBoxCell();

                    string vector_representation = "{";

                    for (int j = 0; j < vectorNodeTmp.vector.Count(); j++)
                    {
                        vector_representation += vectorNodeTmp.vector[j].ToString();
                        if ( j < vectorNodeTmp.vector.Count() -1 )
                          vector_representation += ",";

                    }

                    
                    vector_representation+= "}";

                    txt2A.Value = vector_representation;

                    dataGridRow.Cells.Add(txt2A_i2);
                    txt2A_i2.ReadOnly = true;
                    dataGridRow.Cells.Add(txt2A);
                    txt2A.ReadOnly = true;

                    dataGridRow.Height = 25;

                    
                    dataGridView2.Rows.Add(dataGridRow);


                }


            }

        }


        public void Show_Datagridview3_Topics_Sites()
        {
            int num_of_topics = Form1.FirStaticVar.getNumOfTopics();
            LinkedList<Sem_Supervised_Sites_PartB.Form1.vectorNode> relatedPoints_temp = new LinkedList<Sem_Supervised_Sites_PartB.Form1.vectorNode>();
            

            dataGridView3.Rows.Clear();
            relatedPoints_temp.Clear();

            for (int i = 0; i < num_of_topics; i++)
            {
                relatedPoints_temp = Form1.FirStaticVar.tmpCluster[i].relatedPoints;

                foreach (Sem_Supervised_Sites_PartB.Form1.vectorNode vectorNodeTmp in relatedPoints_temp)
                {
                    DataGridViewRow dataGridRow = new DataGridViewRow();
                    DataGridViewTextBoxCell txt3A_i3 = new DataGridViewTextBoxCell();

                    txt3A_i3.Value = Form1.FirStaticVar.tmpCluster[i].clusterName;

                    DataGridViewTextBoxCell txt3A = new DataGridViewTextBoxCell();

                    txt3A.Value = vectorNodeTmp.name;

                    dataGridRow.Cells.Add(txt3A_i3);
                    txt3A_i3.ReadOnly = true;
                    dataGridRow.Cells.Add(txt3A);
                    txt3A.ReadOnly = true;

                    dataGridRow.Height = 25;

                    //brightPastelArray

                    dataGridRow.Cells[0].Style.BackColor = Color.FromArgb(brightPastelArray[i % 15, 0], brightPastelArray[i % 15, 1], brightPastelArray[i % 15, 2]);
                    dataGridRow.Cells[1].Style.BackColor = Color.FromArgb(brightPastelArray[i % 15, 0], brightPastelArray[i % 15, 1], brightPastelArray[i % 15, 2]);


                    dataGridView3.Rows.Add(dataGridRow);


                }


            }

        }

        public void Datagridview_No_Selection()
        {


            for (int i = 0; i < this.dataGridView1.SelectedCells.Count; i++)
                this.dataGridView1.SelectedCells[i].Selected = false;

            for (int i = 0; i < this.dataGridView2.SelectedCells.Count; i++)
                this.dataGridView2.SelectedCells[i].Selected = false;

            for (int i = 0; i < this.dataGridView3.SelectedCells.Count; i++)
                this.dataGridView3.SelectedCells[i].Selected = false;
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        



    }
}
