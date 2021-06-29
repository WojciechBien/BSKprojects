using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bsk_zadania
{
    public partial class Form1 : Form
    {
        private projekt_2 _projekt2;
        private projekt_3 _projekt3;
        public Form1()
        {
            InitializeComponent();
            _projekt2 = new projekt_2();
            radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControlWithoutHeader1.SelectedTab = tabControlWithoutHeader1.TabPages[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControlWithoutHeader1.SelectedTab = tabControlWithoutHeader1.TabPages[1];
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            tabControlWithoutHeader1.SelectedTab = tabControlWithoutHeader1.TabPages[2];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                tableLayoutPanel5.Visible = true;
                tableLayoutPanel6.Visible = false;
                button6.Visible = true;
                button7.Visible = false;
                label4.Text = "Enter a name for a encryption key that will be generated";
            }
            else
            {
                tableLayoutPanel5.Visible = false;
                button6.Visible = false;
                button7.Visible = true;
                tableLayoutPanel6.Visible = true;
                label4.Text = "Select a file with encryption key that was used to encrypt the file";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                button6.Visible = false;
                button7.Visible = true;
                textBox3.Text = string.Empty;
                button8.Visible = true;
                textBox3.Enabled = false;
                tableLayoutPanel5.Visible = false;
                tableLayoutPanel6.Visible = true;
                label4.Text = "Enter a name for a encryption key that was used to encrypt the file"; 
            }
            else
            {
                textBox3.Text = string.Empty;
                button8.Visible = false;
                textBox3.Enabled = true;
                tableLayoutPanel5.Visible = true;
                button6.Visible = false;
                button7.Visible = true;
                tableLayoutPanel6.Visible = false;
                label4.Text = "Enter a name for a encryption key that will be generated";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var filename = ShowDialog("Enter a new name for encrypted file");
            MessageBox.Show(_projekt2.EncryptFile(textBox1.Text, filename, textBox3.Text));
        }
        public static  string ShowDialog(string message)
        {
            var prompt = new dialogKey(message);
            prompt.ShowDialog();
            return prompt.fileName;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                button7.Enabled = true;
            }
            else
            {
                button7.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox1.Text))
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var filename = ShowDialog("Enter a new name for decrypted file");
            if (MessageBox.Show(_projekt2.DecryptFile(textBox2.Text, textBox3.Text, ref filename)) == DialogResult.OK)
            {
                MessageBox.Show(_projekt2.CompareFiles(_projekt2.originalFilePath, filename));
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                button7.Enabled = true;
            }
            else
            {
                button7.Enabled = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog2.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openFileDialog3.FileName;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog4.FileName;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                button9.Enabled = true;
            }
            else
            {
                button9.Enabled = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _projekt3 = new projekt_3();
            var rsaDialog = new RSAdialog(_projekt3.Encrypt(textBox5.Text),_projekt3);
            rsaDialog.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            provideIterations form = new provideIterations();
            if (form.ShowDialog()==DialogResult.OK)
            {
                projekt_4 pr4 = new projekt_4();
                pr4.process(form.Iterations);
                ResultForm res = new ResultForm(pr4.time,pr4.Iterations.ToString(),pr4.Average.ToString()+"%",pr4.IsHashSizeConstMsg);
                res.Show();
            }
        }
    }
}
