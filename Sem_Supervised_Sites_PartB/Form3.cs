using System;
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
        BagofWords Bag_Gui3;
        

        public static DateTime date1;

        public static Form3 ThirStaticVar;
        static int num_of_words = 10;
        string[] dictionary = new string[] { "sport", "computer", "fashion", "team", "designer", "basketball", "football", "platform", "Sony", "Kinect" };
        
        List<string> _items = new List<string>();
        List<string> _items2 = new List<string>();
        List<string> _items3 = new List<string>();
        List<string> _items4 = new List<string>();
        List<string> _items5 = new List<string>();
        List<string> _items6 = new List<string>();
        List<string> _items7 = new List<string>();
        public Form3()
        {
            
            //this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

         
            _items.Add("www.nba.com");
            _items.Add("www.espn.com");
            
            listBox1.DataSource = _items;
            listBox1.ClearSelected();

            _items2.Add("www.gamespot.com");
            _items2.Add("www.1up.com");

            listBox2.DataSource = _items2;
            listBox2.ClearSelected();

            _items3.Add("www.ftv.com");
            _items3.Add("www.victoriasecret.com");

            listBox3.DataSource = _items3;
            listBox3.ClearSelected();

            _items4.Add("www.ftv.com");
            _items4.Add("www.nba.com");

            listBox4.DataSource = _items4;
            listBox4.ClearSelected();

            _items5.Add("www.victoriasecret.com");
            _items5.Add("www.espn.com");

            listBox5.DataSource = _items5;
            listBox5.ClearSelected();

            _items6.Add("www.gamespot.com");
            _items6.Add("www.ftv.com");

            listBox6.DataSource = _items6;
            listBox6.ClearSelected();

            _items7.Add("www.1up.com");
            _items7.Add("www.nba.com");

            listBox7.DataSource = _items7;
            listBox7.ClearSelected();

            

            // Dictionary Table

            this.num_of_words_in_dictionary = Tools.num_of_words_in_dictionary;
            this.words_in_dictionary = Tools.words_in_dictionary;

            for (int i = 0; i < this.num_of_words_in_dictionary; i++)
            {
                DataGridViewRow dataGridRow = new DataGridViewRow();
                DataGridViewTextBoxCell txt1A_i1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell txt1A = new DataGridViewTextBoxCell();

                txt1A_i1.Value = i+1;
                txt1A.Value = words_in_dictionary[i];

                dataGridRow.Cells.Add(txt1A_i1);
                txt1A_i1.ReadOnly = true;
                dataGridRow.Cells.Add(txt1A);
                txt1A.ReadOnly = true;

                dataGridRow.Height = 25;



                dataGridView1.Rows.Add(dataGridRow);

            }

           
//---------------------------------------------------------------------------------------

            //-----------------1st line-------------------------

            DataGridViewRow dataGridRow2_1 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt1A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt1A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt1A_3 = new DataGridViewTextBoxCell();

          //  txt1A_1.Value = "Sports";

          //  dataGridRow2_1.Cells.Add(txt1A_1);
          //  txt1A_1.ReadOnly = true;
           

            txt1A_2.Value = "www.espn.com";
            dataGridRow2_1.Cells.Add(txt1A_2);

           // txt1A_3.Value = "{10,0,20,1,0,0,0,0,0,0}";
            txt1A_3.Value = "{50,10,4}";
            dataGridRow2_1.Cells.Add(txt1A_3);

            dataGridRow2_1.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_1);

         //-----------------2nd line-------------------------


            DataGridViewRow dataGridRow2_2 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt2A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt2A_3 = new DataGridViewTextBoxCell();

         //   txt2A_1.Value = " ";
         //   dataGridRow2_2.Cells.Add(txt2A_1);
         //   txt2A_1.ReadOnly = true;


            txt2A_2.Value = "www.nba.com";
            dataGridRow2_2.Cells.Add(txt2A_2);
            txt2A_2.ReadOnly = true;

            //txt2A_3.Value = "{20,1,21,1,0,0,0,0,0,0}";
            txt2A_3.Value = "{40,20,13}";
            dataGridRow2_2.Cells.Add(txt2A_3);
            txt2A_3.ReadOnly = true;

            dataGridRow2_2.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_2);

         //-----------------3rd line-------------------------


            DataGridViewRow dataGridRow2_3 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt3A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt3A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt3A_3 = new DataGridViewTextBoxCell();

         //   txt3A_1.Value = "Computers";
         //   dataGridRow2_3.Cells.Add(txt3A_1);
         //   txt3A_1.ReadOnly = true;


            txt3A_2.Value = "www.gamespot.com";
            dataGridRow2_3.Cells.Add(txt3A_2);
            txt3A_2.ReadOnly = true;

            //txt3A_3.Value = "{5,30,10,1,20,0,0,0,0,0}";
            txt3A_3.Value = "{30,70,6}";
            dataGridRow2_3.Cells.Add(txt3A_3);
            txt3A_3.ReadOnly = true;

            dataGridRow2_3.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_3);

        //-----------------4th line-------------------------

            DataGridViewRow dataGridRow2_4 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt4A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt4A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt4A_3 = new DataGridViewTextBoxCell();

        //    txt4A_1.Value = "Fashion";
        //    dataGridRow2_4.Cells.Add(txt4A_1);
        //    txt4A_1.ReadOnly = true;


            txt4A_2.Value = "www.victoriasecret.com";
            dataGridRow2_4.Cells.Add(txt4A_2);
            txt4A_2.ReadOnly = true;

            //txt4A_3.Value = "{15,3,6,0,4,0,0,0,0,0}";
            txt4A_3.Value = "{40,3,120}";
            dataGridRow2_4.Cells.Add(txt4A_3);
            txt4A_3.ReadOnly = true;

            dataGridRow2_4.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_4);


            //-----------------5th line-------------------------

            DataGridViewRow dataGridRow2_5 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt5A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt5A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt5A_3 = new DataGridViewTextBoxCell();

          //  txt5A_1.Value = "Fashion";
          //  dataGridRow2_5.Cells.Add(txt5A_1);
          //  txt5A_1.ReadOnly = true;


            txt5A_2.Value = "www.ftv.com";
            dataGridRow2_5.Cells.Add(txt5A_2);
            txt5A_2.ReadOnly = true;

            //txt5A_3.Value = "{15,3,6,100,70,0,0,0,0,0}";
            txt5A_3.Value = "{40,3,200}";
            dataGridRow2_5.Cells.Add(txt5A_3);
            txt5A_3.ReadOnly = true;

            dataGridRow2_5.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_5);

            //-----------------6th line-------------------------

            DataGridViewRow dataGridRow2_6 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt6A_1 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt6A_2 = new DataGridViewTextBoxCell();
            DataGridViewTextBoxCell txt6A_3 = new DataGridViewTextBoxCell();

           // txt6A_1.Value = "No Topic";
           // dataGridRow2_6.Cells.Add(txt6A_1);
           // txt6A_1.ReadOnly = true;


            txt6A_2.Value = "www.1up.com";
            dataGridRow2_6.Cells.Add(txt6A_2);
            txt6A_2.ReadOnly = true;

            txt6A_3.Value = "{30,200,3}";
            dataGridRow2_6.Cells.Add(txt6A_3);
            txt6A_3.ReadOnly = true;

            dataGridRow2_6.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_6);

            
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

            /*double[,] matr = new double[7, 3] { { 5, 2, 1 }, { 6, 3, 1 }, { 4, 6, 1 }, { 3, 6, 2 }, { 1, 1, 5 }, { 1, 1, 4 }, { 4,7,2 } };
            double[,] matr1 = new double[2, 3] { { 5, 2, 1 }, { 6, 3, 1 } };
            double[,] matr2 = new double[2, 3] { { 4, 6, 1 }, { 3, 6, 2 } };
            double[,] matr3 = new double[2, 3] { { 1, 1, 5 }, { 1, 1, 4 } };

            LinkedList<double[,]> ll = new LinkedList<double[,]>();
            ll.AddLast(matr1);
            ll.AddLast(matr2);
            ll.AddLast(matr3);*/

            //Activate MPCK after all info is collected
            MPCKMeans k = new MPCKMeans(Form1.FirStaticVar.getNumOfTopics(), Tools.pca_result, Tools.mLC, Form1.FirStaticVar.getW(), Form1.FirStaticVar.getW_Roof());
            //MPCKMeans k = new MPCKMeans(3, matr, ll, 10, 10);
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
            /*Form1.FirStaticVar.panel2.siteList_items.Clear();
            Form1.FirStaticVar.panel2.dataGridView1.Rows.Clear();
            Form1.FirStaticVar.panel2.dataGridView2.Rows.Clear();*/
            Form1.StatPanel3.Hide();
            Form1.StatPanel2.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
