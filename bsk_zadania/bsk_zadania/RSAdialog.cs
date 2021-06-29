using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bsk_zadania
{
    public partial class RSAdialog : Form
    {
        private projekt_3 _projekt3;
        public RSAdialog(string encryptedText, projekt_3 projekt3)
        {
            InitializeComponent();
            richTextBox1.Text = encryptedText;
            _projekt3 = projekt3;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox2.Text))
            {
                richTextBox2.Text = _projekt3.Decrypt();
            }
        }
    }
}
