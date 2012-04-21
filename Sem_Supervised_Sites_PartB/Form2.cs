using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Sem_Supervised_Sites_PartB
{
    public partial class Form2 : Panel
    {
        public static Form2 SecStaticVar;
        List<string> siteList_items = new List<string>();
        List<string> siteList_items_temp = new List<string>();
        List<string> supervised_siteList_items = new List<string>();
        List<string> supervised_siteList_items_temp = new List<string>();
        string filename;
        string filenametmp;
        bool is_site_added = false;
        int dimensions;
        public Form2()
        {
            //this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            textBox1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;
            label5.Enabled = false;
            textBox2.Enabled = false;

            
            //siteList_items.Add("www.gamespot.com.txt");
            //siteList_items.Add("www.nba.com.txt");
            //siteList_items.Add("www.victoriassecret.com.txt");
            //siteList_items.Add("www.espn.com.txt");
            //siteList_items.Add("www.ftv.com.txt");
            //siteList_items.Add("www.1up.com.txt");
           // siteList_items.Add("www.vogue.com.txt");
            //siteList_items.Add("www.sportsillustrated.co.za.txt");
            //siteList_items.Add("www.ea.com.txt");
            //siteList_items.Add("www.fifa.com.txt");

            //listBox1.DataSource = siteList_items;


            // **** DataGridViewRow2  ****

/*            //-----------------1st line-------------------------
            DataGridViewRow dataGridRow2_1 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2_1A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo2_1 = new DataGridViewComboBoxCell();

            cbo2_1.Items.Add("Computers");
            cbo2_1.Items.Add("Sports");
            cbo2_1.Items.Add("Fashion");


            txt2_1A.Value = "www.vogue.com.txt";

            dataGridRow2_1.Cells.Add(txt2_1A);
            txt2_1A.ReadOnly = true;
            dataGridRow2_1.Cells.Add(cbo2_1);
            dataGridRow2_1.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_1);


            //-----------------2nd line-------------------------
            DataGridViewRow dataGridRow2_2 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2_2A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo2_2 = new DataGridViewComboBoxCell();

            cbo2_2.Items.Add("Computers");
            cbo2_2.Items.Add("Sports");
            cbo2_2.Items.Add("Fashion");


            txt2_2A.Value = "www.fashion.net.txt";

            dataGridRow2_2.Cells.Add(txt2_2A);
            txt2_2A.ReadOnly = true;
            dataGridRow2_2.Cells.Add(cbo2_2);
            dataGridRow2_2.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_2);


            //-----------------3rd line-------------------------
            DataGridViewRow dataGridRow2_3 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2_3A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo2_3 = new DataGridViewComboBoxCell();

            cbo2_3.Items.Add("Computers");
            cbo2_3.Items.Add("Sports");
            cbo2_3.Items.Add("Fashion");


            txt2_3A.Value = "www.ea.com.txt";

            dataGridRow2_3.Cells.Add(txt2_3A);
            txt2_3A.ReadOnly = true;
            dataGridRow2_3.Cells.Add(cbo2_3);
            dataGridRow2_3.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_3);

            //-----------------4th line-------------------------
            DataGridViewRow dataGridRow2_4 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2_4A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo2_4 = new DataGridViewComboBoxCell();

            cbo2_4.Items.Add("Computers");
            cbo2_4.Items.Add("Sports");
            cbo2_4.Items.Add("Fashion");


            txt2_4A.Value = "www.fifa.com.txt";

            dataGridRow2_4.Cells.Add(txt2_4A);
            txt2_4A.ReadOnly = true;
            dataGridRow2_4.Cells.Add(cbo2_4);
            dataGridRow2_4.Height = 25;

            dataGridView2.Rows.Add(dataGridRow2_4);





            // **** DataGridViewRow1  ****


            //-----------------1st line-------------------------

            DataGridViewRow dataGridRow = new DataGridViewRow();
          //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt1A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo1 = new DataGridViewComboBoxCell();

            cbo1.Items.Add("Computers");
            cbo1.Items.Add("Sports");
            cbo1.Items.Add("Fashion");
            

            txt1A.Value = "www.gamespot.com.txt";
           
            dataGridRow.Cells.Add(txt1A);
            txt1A.ReadOnly = true;
            dataGridRow.Cells.Add(cbo1);
            dataGridRow.Height = 25;
          
            dataGridView1.Rows.Add(dataGridRow);

            //-----------------2nd line-------------------------


            DataGridViewRow dataGridRow2 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt2A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo2 = new DataGridViewComboBoxCell();

            cbo2.Items.Add("Computers");
            cbo2.Items.Add("Sports");
            cbo2.Items.Add("Fashion");


            txt2A.Value = "www.nba.com.txt";

            dataGridRow2.Cells.Add(txt2A);
            txt2A.ReadOnly = true;
            dataGridRow2.Cells.Add(cbo2);
            dataGridRow2.Height = 25;

            dataGridView1.Rows.Add(dataGridRow2);
        //    dataGridView1.Rows.RemoveAt(3);

         /*   //-----------------3rd line-------------------------


            DataGridViewRow dataGridRow3 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt3A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo3 = new DataGridViewComboBoxCell();

            cbo3.Items.Add("Computers");
            cbo3.Items.Add("Sports");
            cbo3.Items.Add("Fashion");


            txt3A.Value = "www.espn.com.txt";

            dataGridRow3.Cells.Add(txt3A);
            txt3A.ReadOnly = true;
            dataGridRow3.Cells.Add(cbo3);
            dataGridRow3.Height = 25;

            dataGridView1.Rows.Add(dataGridRow3);
            */


          //-----------------4th line-------------------------

            /*
            DataGridViewRow dataGridRow4 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt4A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo4 = new DataGridViewComboBoxCell();

            cbo4.Items.Add("Computers");
            cbo4.Items.Add("Sports");
            cbo4.Items.Add("Fashion");


            txt4A.Value = "www.victoriasecret.com.txt";

            dataGridRow4.Cells.Add(txt4A);
            txt4A.ReadOnly = true;
            dataGridRow4.Cells.Add(cbo4);
            dataGridRow4.Height = 25;

            dataGridView1.Rows.Add(dataGridRow4);

            
          //-----------------5th line-------------------------


            DataGridViewRow dataGridRow5 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt5A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo5 = new DataGridViewComboBoxCell();

            cbo5.Items.Add("Computers");
            cbo5.Items.Add("Sports");
            cbo5.Items.Add("Fashion");


            txt5A.Value = "www.ftv.com.txt";

            dataGridRow5.Cells.Add(txt5A);
            txt5A.ReadOnly = true;
            dataGridRow5.Cells.Add(cbo5);
            dataGridRow5.Height = 25;

            dataGridView1.Rows.Add(dataGridRow5);


            //-----------------6th line-------------------------


            DataGridViewRow dataGridRow6 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt6A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo6 = new DataGridViewComboBoxCell();

            cbo6.Items.Add("Computers");
            cbo6.Items.Add("Sports");
            cbo6.Items.Add("Fashion");


            txt6A.Value = "www.1up.com.txt";

            dataGridRow6.Cells.Add(txt6A);
            txt6A.ReadOnly = true;
            dataGridRow6.Cells.Add(cbo6);
            dataGridRow6.Height = 25;

            dataGridView1.Rows.Add(dataGridRow6);


            //-----------------7th line-------------------------


            DataGridViewRow dataGridRow7 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt7A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo7 = new DataGridViewComboBoxCell();

            cbo7.Items.Add("Computers");
            cbo7.Items.Add("Sports");
            cbo7.Items.Add("Fashion");


            txt7A.Value = "www.espn.com.txt";

            dataGridRow7.Cells.Add(txt7A);
            txt7A.ReadOnly = true;
            dataGridRow7.Cells.Add(cbo7);
            dataGridRow7.Height = 25;

            dataGridView1.Rows.Add(dataGridRow7);

            //-----------------8th line-------------------------


            DataGridViewRow dataGridRow8 = new DataGridViewRow();
            //  DataGridViewCell[] cells = new DataGridViewCell[2];
            DataGridViewTextBoxCell txt8A = new DataGridViewTextBoxCell();
            DataGridViewComboBoxCell cbo8 = new DataGridViewComboBoxCell();

            cbo8.Items.Add("Computers");
            cbo8.Items.Add("Sports");
            cbo8.Items.Add("Fashion");


            txt8A.Value = "www.victoriasecret.com.txt";

            dataGridRow8.Cells.Add(txt8A);
            txt8A.ReadOnly = true;
            dataGridRow8.Cells.Add(cbo8);
            dataGridRow8.Height = 25;

            dataGridView1.Rows.Add(dataGridRow8);

            */


        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Type File";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                foreach (string filenamepath in openFileDialog.FileNames)
                {
                    string filename = Path.GetFileName(filenamepath);
 
                    if (!siteList_items.Contains(filename))
                    {
                        siteList_items.Add(filename);
                        siteList_items_temp.Add(filename);
                        is_site_added = true;
                        
                    }
                }

                if (is_site_added)
                {
                    //int NumOfItems = siteList_items.Count();
                    int NumOfItems = siteList_items_temp.Count();
                    DataGridViewRow[] dataGridRow2_Array;
                    dataGridRow2_Array = new DataGridViewRow[NumOfItems];

                    DataGridViewTextBoxCell[] txt2_Array;
                    txt2_Array = new DataGridViewTextBoxCell[NumOfItems];

                    DataGridViewComboBoxCell[] cbo2_Array;
                    cbo2_Array = new DataGridViewComboBoxCell[NumOfItems];

                    for (int i = 0; i < NumOfItems; i++)
                    {
                        dataGridRow2_Array[i] = new DataGridViewRow();
                        txt2_Array[i] = new DataGridViewTextBoxCell(); 
                        cbo2_Array[i] = new DataGridViewComboBoxCell();
                        
                        for (int j = 0; j < Form1.FirStaticVar.Topics.Count(); j++)
                        {
                            cbo2_Array[i].Items.Add(Form1.FirStaticVar.Topics[j]);
                        }
                        //txt2_Array[i].Value = siteList_items[i]; 
                        txt2_Array[i].Value = siteList_items_temp[i]; 
                        dataGridRow2_Array[i].Cells.Add(txt2_Array[i]);
                        txt2_Array[i].ReadOnly = true;
                        dataGridRow2_Array[i].Cells.Add(cbo2_Array[i]);
                        dataGridRow2_Array[i].Height = 25;
                        dataGridView2.Rows.Add(dataGridRow2_Array[i]);
                    }

                }

                is_site_added = false;
                //siteList_items.Clear();
                siteList_items_temp.Clear();

               /* DataGridViewRow dataGridRow2_1 = new DataGridViewRow();
                    //  DataGridViewCell[] cells = new DataGridViewCell[2];
                    DataGridViewTextBoxCell txt2_1A = new DataGridViewTextBoxCell();
                    DataGridViewComboBoxCell cbo2_1 = new DataGridViewComboBoxCell();

                    cbo2_1.Items.Add("Computers");
                    cbo2_1.Items.Add("Sports");
                    cbo2_1.Items.Add("Fashion");


                    txt2_1A.Value = "www.vogue.com.txt";

                    dataGridRow2_1.Cells.Add(txt2_1A);
                    txt2_1A.ReadOnly = true;
                    dataGridRow2_1.Cells.Add(cbo2_1);
                    dataGridRow2_1.Height = 25;

                    dataGridView2.Rows.Add(dataGridRow2_1);
                */
 
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
            {
                siteList_items.RemoveAt(cell.RowIndex);
                DataGridViewRow dataGrid1NewRow = new DataGridViewRow();
                dataGrid1NewRow = dataGridView2.Rows[cell.RowIndex];
                dataGridView2.Rows.Remove(dataGridView2.Rows[cell.RowIndex]);
                dataGridView1.Rows.Add(dataGrid1NewRow);
                string filename = dataGrid1NewRow.Cells[0].Value.ToString();
                supervised_siteList_items.Add(filename);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                supervised_siteList_items.RemoveAt(cell.RowIndex);
                DataGridViewRow dataGrid2NewRow = new DataGridViewRow();
                dataGrid2NewRow = dataGridView1.Rows[cell.RowIndex];
                dataGridView1.Rows.Remove(dataGridView1.Rows[cell.RowIndex]);
                dataGridView2.Rows.Add(dataGrid2NewRow);
                string filename = dataGrid2NewRow.Cells[0].Value.ToString();
                siteList_items.Add(filename);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.StatPanel3.Show();
            Form1.StatPanel3.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void lineShape2_Click(object sender, EventArgs e)
        {

        }

        private void lineShape4_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.StatPanel2.Hide();
        }


        private void Form2_Load_1(object sender, EventArgs e)
        {

        }
        
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Type File";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                filename = openFileDialog.FileName;
                filenametmp = filename;
                filenametmp.Replace(@"\\", @"\");
                textBox1.Text = filenametmp;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
            {
                //var row = dataGridView1.Rows[cell.RowNumber];
                siteList_items.RemoveAt(cell.RowIndex);
                dataGridView2.Rows.Remove(dataGridView2.Rows[cell.RowIndex]);
                

            }

//            foreach (DataGridViewRow dr in dataGridView2.SelectedRows)
//            {

//                dataGridView2.Rows.Remove(dr);

//            }

            

        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            siteList_items.Clear();
            dataGridView2.Rows.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label5.Enabled = true;
                textBox2.Enabled = true;
                return;
            }
            label5.Enabled = false;
            textBox2.Enabled = false;
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dimensions = int.Parse(textBox2.Text);
        }

        public int getDimensions()
        {
            return dimensions;
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
