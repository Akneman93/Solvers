using Solvers.Interfaces;
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
    public partial class ParametersForm : Form
    {
        Dictionary<Label, TextBox> dic = new Dictionary<Label, TextBox>();
        ISearchInfo info;

        public ParametersForm(ISearchInfo info)
        {
            InitializeComponent();
            int i = 0;
            TextBox textBox;
            Label label;
            this.info = info;
            foreach (KeyValuePair<string, string> pair in info.SearchParameters)
            {
                label = new Label();
                label.Text = pair.Key;
                tablePanel.Controls.Add(label, 0, i);
                

                textBox = new TextBox();
                textBox.Text = pair.Value;
                tablePanel.Controls.Add(textBox, 1, i);

                dic.Add(label, textBox);

                i++;


            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            foreach(Label label in dic.Keys)
            {
                info.SearchParameters[label.Text] = dic[label].Text.ToString();
            }

            DialogResult = DialogResult.OK;
            this.Close();

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
