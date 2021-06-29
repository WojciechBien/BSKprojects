using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bsk_zadania
{
    public partial class ResultForm : Form
    {
        public ResultForm(string time,string iterations,string average)
        {
            InitializeComponent();
            timeLabel.Text = time;
            iterationsLabel.Text = iterations;
            averageLabel.Text = average;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
