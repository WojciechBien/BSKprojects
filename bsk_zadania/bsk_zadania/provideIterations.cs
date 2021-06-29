using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bsk_zadania
{
    public partial class provideIterations : Form
    {
        public int Iterations;
        public provideIterations()
        {
            InitializeComponent();
            Iterations = Int32.Parse(numericUpDown1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
