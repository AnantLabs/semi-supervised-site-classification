using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Project_Phase_B___Engine_Stopwords;
using Project_Phase_B___Engine_BagofWords;
using PCA_Logic;

namespace Sem_Supervised_Sites_PartB
{
    public partial class Form2 : Panel

    {

        public static DateTime date3;
        public static DateTime date4;
        public static TimeSpan duration2;
        public struct siteClusterNode
        {
            public string name;
            public string clusterName;
        }
        private siteClusterNode[] siteClusterNodeArray;

        public static Form2 SecStaticVar;
        public List<string> siteList_items = new List<string>();
        List<string> siteList_items_temp = new List<string>();
        List<string> supervised_siteList_items = new List<string>();
        List<string> supervised_siteList_items_temp = new List<string>();
        List<string> SitesFileNamesList_StopWords = new List<string>();
        string[] sitesFileNames;

     
        Sem_Supervised_Sites_PartB.Form1.vectorNode vectorNodetmp;

        Stopwords Stop;
        public BagofWords Bag;
        PCA Pca;

        double[,] pca_result;
        LinkedList<double[,]> mLC = new LinkedList<double[,]>();
        
        List<double[,]> mLC_temp;

        string filename;
        string filenametmp;
        bool is_site_added = false;
        int dimensions;
        int pca_result_size;
        int num_of_topics;
        int numOfAdded;

        int[] Supervised_Matrix_rows_per_Cluster;

        public Form2()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            textBox1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView2.AllowUserToAddRows = false;

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

            label5.Enabled = false;
            textBox2.Enabled = false;
            numOfAdded = 0;
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
                    numOfAdded++;
                    string filename = Path.GetFileName(filenamepath);

                    if (!siteList_items.Contains(filename) && !supervised_siteList_items.Contains(filename))
                    {
                        StreamReader streamReader = new StreamReader(filenamepath);
                        string text = streamReader.ReadToEnd();
                        streamReader.Close();

                        string[] tokens = filenamepath.Split('.');
                        string savefile_new = tokens[0] + "_new.txt";
                        try
                        {
                            File.SetAttributes(savefile_new, FileAttributes.Normal);
                        }
                        catch
                        {
                        }
                        using (StreamWriter outfile = new StreamWriter(savefile_new))
                        {
                            outfile.Write(Stop.CleanSearchedWords(text));
                        }

                        siteList_items.Add(filename);
                        siteList_items_temp.Add(filename);
                        is_site_added = true;

                        SitesFileNamesList_StopWords.Add(savefile_new);

                        File.SetAttributes(savefile_new, FileAttributes.Hidden);
                    }
                }
                if (numOfAdded >= 2)
                {
                    this.button7.Enabled = true;
                    this.button8.Enabled = true;
                }
                if (is_site_added)
                {
                    
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
                        
                        
                        txt2_Array[i].Value = siteList_items_temp[i];
                        dataGridRow2_Array[i].Cells.Add(txt2_Array[i]);
                        txt2_Array[i].ReadOnly = true;
                        dataGridRow2_Array[i].Cells.Add(cbo2_Array[i]);
                        dataGridRow2_Array[i].Height = 25;
                        dataGridView2.Rows.Add(dataGridRow2_Array[i]);                    }

                }

                is_site_added = false;
                
                siteList_items_temp.Clear();

            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.button5.Enabled = true;
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


        private void setSiteClusterNodeArray()
        {
            int index = 0;
            bool found_in_datagridview2 = false;

            siteClusterNode[] siteClusterNodeArray_temp=new siteClusterNode[SitesFileNamesList_StopWords.Count()];
            siteClusterNodeArray = siteClusterNodeArray_temp;
          
            foreach (string s in SitesFileNamesList_StopWords)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    string sitename = row.Cells[0].Value.ToString();

                    string[] tokens = sitename.Split('.');
                    
                    
                    if (s.Contains(tokens[0]))
                    {
                        siteClusterNodeArray[index].name = row.Cells[0].Value.ToString();
                        siteClusterNodeArray[index].clusterName = row.Cells[1].Value.ToString();
                        found_in_datagridview2 = true;
                    }

                }

                if (!found_in_datagridview2)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        string sitename = row.Cells[0].Value.ToString();

                        string[] tokens = sitename.Split('.');

                        
                        if (s.Contains(tokens[0]))
                        {
                            siteClusterNodeArray[index].name = row.Cells[0].Value.ToString();
                            siteClusterNodeArray[index].clusterName = row.Cells[1].Value.ToString();
                        }

                    }

                }
                index++;
                found_in_datagridview2 = false;
            }
        }

        public void setSitesFileNames()
        {
            string [] sitesFileNames_temp=  new string[SitesFileNamesList_StopWords.Count()];

            sitesFileNames = sitesFileNames_temp;

            int index_s = 0;
            foreach (string s in SitesFileNamesList_StopWords)
            {
                sitesFileNames[index_s++] = s;
            }

        }

        private void runBOW_PCA()
        {
            
            
            
            BagofWords Bag_temp = new BagofWords(sitesFileNames);

            Bag = Bag_temp;

            Bag.setSitesFreqVector();

            if (checkBox1.Checked == true) // if the enable PCA was checked
            {
                
                
                PCA Pca_temp = new PCA(Bag.getSitesFreqVector(), dimensions);
                Pca = Pca_temp;
                Pca.compute();
                pca_result = Pca.Result;
                pca_result_size = dimensions;
            }
            else
            {
                pca_result = Bag.getSitesFreqVector();
                pca_result_size = Bag.getNumOfWordsInDictionary();
            }

        }

        private void setRelatedPointsInEachCluster()
        {
            int numOfSites = SitesFileNamesList_StopWords.Count();
            this.num_of_topics = Form1.FirStaticVar.getNumOfTopics();  

            for (int i = 0; i < this.num_of_topics; i++)
               Form1.FirStaticVar.tmpCluster[i].relatedPoints.Clear();  

            for (int i = 0; i < this.num_of_topics; i++)    
            {
                for (int j = 0; j < numOfSites; j++)
                {
                    if (Form1.FirStaticVar.tmpCluster[i].clusterName.Equals(siteClusterNodeArray[j].clusterName))
                    {
                        vectorNodetmp = new Sem_Supervised_Sites_PartB.Form1.vectorNode();
                        vectorNodetmp.name = siteClusterNodeArray[j].name;
                        vectorNodetmp.vector = new double[pca_result_size];

                        for (int k = 0; k < pca_result_size; k++)
                        {
                            vectorNodetmp.vector[k] = pca_result[j, k];
                        }

                        Form1.FirStaticVar.tmpCluster[i].relatedPoints.AddFirst(vectorNodetmp); 
                    }

                }
            }

        }


        private void set_Supervised_Matrix_rows_per_Cluster()
        {
           
            int [] Supervised_Matrix_rows_per_Cluster_temp = new int[num_of_topics];
            Supervised_Matrix_rows_per_Cluster = Supervised_Matrix_rows_per_Cluster_temp;

            for (int i = 0; i < num_of_topics; i++)
                Supervised_Matrix_rows_per_Cluster[i] = 0;

        }

        private void count_Supervised_Sites_For_Each_Cluster()
        {
            // to count how many supervised site are in each cluster
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int index_for_compatible_cluster;

                string cluster_name = row.Cells[1].Value.ToString();
                index_for_compatible_cluster = Form1.FirStaticVar.dictionary_ClusterNameToValue_KeyToVal(cluster_name);

                Sem_Supervised_Sites_PartB.Form1.tmpClust tmpClust_Temp;
                tmpClust_Temp = Form1.FirStaticVar.tmpCluster[index_for_compatible_cluster];
                tmpClust_Temp.SupervisedCounter++;
                Form1.FirStaticVar.tmpCluster[index_for_compatible_cluster] = tmpClust_Temp;

                

            }


        }


        // sets the matrix of supervised sites x Words ( dictionary or after PCA ) for each compatible cluster
        private void set_mLC_temp()
        {
            List<double[,]> mLC_temp_temp = new List<double[,]>();
            mLC_temp = mLC_temp_temp;

            for (int i = 0; i < num_of_topics; i++)
            {
                int matrix_rows = Form1.FirStaticVar.tmpCluster[i].SupervisedCounter; 
                int matrix_cols = pca_result_size;

                double[,] matrix = new double[matrix_rows, matrix_cols];

                for (int j = 0; j < matrix_rows; j++)
                {
                    for (int k = 0; k < matrix_cols; k++)
                    {
                        matrix[j, k] = 0;
                    }

                }
            
                mLC_temp.Add(matrix);
                

            }


        }

        // to set the vector matrix for each cluster to the mlc ( Must-Link Constraint )
        private void set_Vector_Matrix_For_Each_Cluster_To_MLC()
        {
            mLC.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                int index_for_compatible_cluster;

                string site_name = row.Cells[0].Value.ToString();
                
                string cluster_name = row.Cells[1].Value.ToString();

                index_for_compatible_cluster = Form1.FirStaticVar.dictionary_ClusterNameToValue_KeyToVal(cluster_name);

                LinkedList<Sem_Supervised_Sites_PartB.Form1.vectorNode> cluster_related_points = Form1.FirStaticVar.tmpCluster[index_for_compatible_cluster].relatedPoints;

                foreach (Sem_Supervised_Sites_PartB.Form1.vectorNode vectorNodetmp_for_supervised in cluster_related_points)
                {
                    if (vectorNodetmp_for_supervised.name.Equals(site_name))
                    {
                        double[,] matrix_tmp = mLC_temp[index_for_compatible_cluster];

                        int i = Supervised_Matrix_rows_per_Cluster[index_for_compatible_cluster];

                        for (int j = 0; j < pca_result_size; j++)
                        {
                            matrix_tmp[i, j] = vectorNodetmp_for_supervised.vector[j];
                        }
                        Supervised_Matrix_rows_per_Cluster[index_for_compatible_cluster]++;
                        mLC_temp[index_for_compatible_cluster] = matrix_tmp;
                    }

                }
                
            }

            for (int i = 0; i < num_of_topics; i++)
                mLC.AddLast(mLC_temp[i]);

        }



        private void button5_Click(object sender, EventArgs e)
        {
            date3 = DateTime.Now;
            int numOfSites = SitesFileNamesList_StopWords.Count();
            siteClusterNodeArray = new siteClusterNode[numOfSites];

            for (int i = 0; i < Form1.FirStaticVar.tmpCluster.Count(); i++)
            {
                Sem_Supervised_Sites_PartB.Form1.tmpClust tmpClust_Temp;
                tmpClust_Temp = Form1.FirStaticVar.tmpCluster[i];
                tmpClust_Temp.SupervisedCounter=0;
                Form1.FirStaticVar.tmpCluster[i] = tmpClust_Temp;
            }

            setSiteClusterNodeArray();
            setSitesFileNames();
            runBOW_PCA();
            setRelatedPointsInEachCluster();

            set_Supervised_Matrix_rows_per_Cluster();

            count_Supervised_Sites_For_Each_Cluster();

            set_mLC_temp();

            set_Vector_Matrix_For_Each_Cluster_To_MLC();



            Tools.num_of_words_in_dictionary = Bag.getNumOfWordsInDictionary();
            Tools.words_in_dictionary = Bag.getWordInDictionary();

            Tools.mLC = this.mLC;
            Tools.pca_result = this.pca_result;

            date4 = DateTime.Now;
            duration2 = date4 - date3;

          

            Form1.FirStaticVar.panel3.Show_Dictionary();
            Form1.FirStaticVar.panel3.Show_Datagridview2_Sites_Vectors();
            Form1.FirStaticVar.panel3.Show_Datagridview3_Topics_Sites();

            Form1.StatPanel3 = Form1.FirStaticVar.panel3;
            Form1.FirStaticVar.panel3.Show();
            Form1.FirStaticVar.panel3.Datagridview_No_Selection();
            Form1.FirStaticVar.panel3.BringToFront();
          

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
      
            dataGridView1.Refresh();
            dataGridView2.Refresh();
            this.Hide();
            Form1.FirStaticVar.Show();
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
                this.button2.Enabled = true;
                this.button3.Enabled = true;
                this.button4.Enabled = true;
                filename = openFileDialog.FileName;
                Stop = new Stopwords(filename);

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

                DataGridViewRow dataGrid2NewRow = new DataGridViewRow();
                dataGrid2NewRow = dataGridView2.Rows[cell.RowIndex];
                string dataGrid2NewRow_string_value = dataGrid2NewRow.Cells[0].Value.ToString();

                for (int i = 0; i < SitesFileNamesList_StopWords.Count(); i++)
                {
                    string[] tokens = dataGrid2NewRow_string_value.Split('.');

                    if (SitesFileNamesList_StopWords[i].Contains(tokens[0]))
                    {
                        SitesFileNamesList_StopWords.RemoveAt(i);
                        break;
                    }
                }

                
                siteList_items.RemoveAt(cell.RowIndex);
                
                dataGridView2.Rows.Remove(dataGridView2.Rows[cell.RowIndex]);



            }

        

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (string s in siteList_items)
            {
                bool finish = false;
                for (int i = 0; i < SitesFileNamesList_StopWords.Count() && !finish; i++)
                {
                    string[] tokens = s.Split('.');
                    
                    if (SitesFileNamesList_StopWords[i].Contains(tokens[0]))
                    {
                        SitesFileNamesList_StopWords.RemoveAt(i);
                        finish = true;
                    }
                }
            }


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
            try
            {
                dimensions = int.Parse(textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Please enter only numbers into the text box!", "Input error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int getDimensions()
        {
            return dimensions;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public FormStartPosition StartPosition { get; set; }

        public BagofWords getBOWinstance()
        {
            return Bag;
        }

        public void UpdateGUI2()
        {
            // Update Data Here

            int num_of_rows_datagridview1;
            int num_of_rows_datagridview2;

            
            dataGridView1.Refresh();
            num_of_rows_datagridview1 = dataGridView1.Rows.Count;

            dataGridView2.Refresh();
            num_of_rows_datagridview2 = dataGridView2.Rows.Count;

            
   

            if (num_of_rows_datagridview2 > 0)
            {
                DataGridViewRow[] updated_row_dataGridView2_Array = new DataGridViewRow[num_of_rows_datagridview2];
                int updated_row_dataGridView2_index = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    DataGridViewComboBoxCell cbo_updated2 = new DataGridViewComboBoxCell();
                    for (int j = 0; j < Form1.FirStaticVar.Topics.Count(); j++)
                    {
                        cbo_updated2.Items.Add(Form1.FirStaticVar.Topics[j]);
                    }


                    DataGridViewRow updated_row_dataGridView2 = new DataGridViewRow();
                    DataGridViewTextBoxCell txt2_TextBoxCell = new DataGridViewTextBoxCell();
                    object cbo_val;


                    txt2_TextBoxCell.Value = row.Cells[0].Value.ToString(); 
                    updated_row_dataGridView2.Cells.Add(txt2_TextBoxCell);
                    txt2_TextBoxCell.ReadOnly = true;
                    cbo_val = row.Cells[1].Value;

                    if (Form1.FirStaticVar.Deleted_Topics != null)
                    {
                        if (Form1.FirStaticVar.Deleted_Topics.Count() > 0)
                        {
                            if (Form1.FirStaticVar.Deleted_Topics.Any(item => item == cbo_val.ToString()))
                            {
                                cbo_val = Form1.FirStaticVar.Topics[0];
                            }
                        }
                    }

                    updated_row_dataGridView2.Cells.Add(cbo_updated2);
                    updated_row_dataGridView2.Cells[1].Value = cbo_val;
                    updated_row_dataGridView2.Height = 25;

                    updated_row_dataGridView2_Array[updated_row_dataGridView2_index] = new DataGridViewRow();
                    updated_row_dataGridView2_Array[updated_row_dataGridView2_index] = updated_row_dataGridView2;

     
                    updated_row_dataGridView2_index++;
                                
                }

                dataGridView2.Rows.Clear();

                for (int i = 0; i < num_of_rows_datagridview2; i++)
                {
                    DataGridViewRow row_updated=new DataGridViewRow();
                    row_updated = updated_row_dataGridView2_Array[i];
                    dataGridView2.Rows.Add(row_updated);

                }

                 
            }

            if (num_of_rows_datagridview1 > 0)
            {
                DataGridViewRow[] updated_row_dataGridView1_Array = new DataGridViewRow[num_of_rows_datagridview1];
                int updated_row_dataGridView1_index = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataGridViewComboBoxCell cbo_updated1 = new DataGridViewComboBoxCell();
                    for (int j = 0; j < Form1.FirStaticVar.Topics.Count(); j++)
                    {
                        cbo_updated1.Items.Add(Form1.FirStaticVar.Topics[j]);
                    }


                    DataGridViewRow updated_row_dataGridView1 = new DataGridViewRow();
                    DataGridViewTextBoxCell txt1_TextBoxCell = new DataGridViewTextBoxCell();
                    object cbo_val;


                    txt1_TextBoxCell.Value = row.Cells[0].Value.ToString(); 
                    updated_row_dataGridView1.Cells.Add(txt1_TextBoxCell);
                    txt1_TextBoxCell.ReadOnly = true;
                    cbo_val = row.Cells[1].Value;

                    if (Form1.FirStaticVar.Deleted_Topics != null)
                    {
                        if (Form1.FirStaticVar.Deleted_Topics.Count() > 0)
                        {
                            if (Form1.FirStaticVar.Deleted_Topics.Any(item => item == cbo_val.ToString()))
                            {
                                cbo_val = Form1.FirStaticVar.Topics[0];
                            }
                        }
                    }

                    updated_row_dataGridView1.Cells.Add(cbo_updated1);
                    updated_row_dataGridView1.Cells[1].Value = cbo_val;
                    updated_row_dataGridView1.Height = 25;

                    updated_row_dataGridView1_Array[updated_row_dataGridView1_index] = new DataGridViewRow();
                    updated_row_dataGridView1_Array[updated_row_dataGridView1_index] = updated_row_dataGridView1;


                    updated_row_dataGridView1_index++;


                }
                
              
                dataGridView1.Rows.Clear();

                for (int i = 0; i < num_of_rows_datagridview1; i++)
                {
                    DataGridViewRow row_updated = new DataGridViewRow();
                    row_updated = updated_row_dataGridView1_Array[i];
                    dataGridView1.Rows.Add(row_updated);

                }
            }
            // cleaning list in case that deleted topic will be re-entered again.
            if (Form1.FirStaticVar.Deleted_Topics !=null )
              Form1.FirStaticVar.Deleted_Topics.Clear();


        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

      

      
    }
}
