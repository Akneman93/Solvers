using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solvers.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            this.Name = "MainForm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            GridWorldForm gwf;
            NPuzzleForm npf;
            if (radioButton2.Checked)
            {
                gwf = new GridWorldForm();
                gwf.ShowDialog();
                this.Show();
            }
            else
            {
                npf = new NPuzzleForm();
                npf.ShowDialog();
                this.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
