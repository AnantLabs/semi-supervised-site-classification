using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sem_Supervised_Sites_PartB
{
    public partial class Form1 : Form
    {
        List<string> _items = new List<string>();
        public string[] Topics;
        double W;
        double W_roof;
        //public static Form1 Form1StaticVar;
        public static Form1 FirStaticVar;
   //     public static Form1 InstanceForm1 { get; private set; } 


        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            FirStaticVar = this;
            panel2.Hide();
            panel3.Hide();
            StatPanel2 = panel2;
            StatPanel3 = panel3;
            StatPanel4 = panel4;
        }

/*      private OpenFileDialog openFileDialog = new OpenFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Type File";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
            }
        } */


        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            if (AddTopic.AddTopicStaticVar == null)
                AddTopic.AddTopicStaticVar = new AddTopic();
            AddTopic.AddTopicStaticVar.ClearTextBox1();
            AddTopic.AddTopicStaticVar.Show();
        }

        public void AddtoListBox1( string str )
        {
            listBox1.Items.Add(str);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Topics= new string[listBox1.Items.Count];

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                object s = listBox1.Items[i];
                Topics[i] = s.ToString();
            }

            StatPanel2.Show();
            StatPanel2.BringToFront();

      
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            W = double.Parse(textBox1.Text);
        }

        private void rectangleShape2_Click(object sender, EventArgs e)
        {

        }

        private void rectangleShape1_Click(object sender, EventArgs e)
        {

        }

        private void lineShape4_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            W_roof = double.Parse(textBox2.Text);
        }

        public double getW()
        {
            return W;
        }

        public double getW_Roof()
        {
            return W_roof;
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rectangleShape3_Click(object sender, EventArgs e)
        {

        }
    }
}
