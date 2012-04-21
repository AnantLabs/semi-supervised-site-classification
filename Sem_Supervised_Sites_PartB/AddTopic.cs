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
    public partial class AddTopic : Form
    {
        public static AddTopic AddTopicStaticVar;
        string Added_Topic;

        public AddTopic()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.FirStaticVar.AddtoListBox1(Added_Topic);
            Form1.FirStaticVar.Enabled = true;
            this.Hide();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.Added_Topic = textBox1.Text;
          
        }

        public void ClearTextBox1()
        {
            textBox1.Clear();
            //textBox1.Select(0, 0);
        }

        private void AddTopic_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.FirStaticVar.Enabled = true;
            this.Hide();
        }



        public string thisAdded_Topic { get; set; }
    }
}
