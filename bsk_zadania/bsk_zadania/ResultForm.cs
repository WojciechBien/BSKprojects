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
        public ResultForm(string time,string iterations,string average,string hashLenghtt)
        {
            InitializeComponent();
            timeLabel.Text = time;
            iterationsLabel.Text = iterations;
            averageLabel.Text = average;
            hashLenght.Text = hashLenghtt;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
