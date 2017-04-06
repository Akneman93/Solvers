using System;
using System.Windows.Forms;

namespace Solvers.Forms
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = String.Format("Название продукта: {0}", AssemblyProduct);
            this.labelVersion.Text = String.Format("Версия: {0}", AssemblyVersion);
            this.labelCopyright.Text = String.Format("Copyright: {0}", AssemblyCopyright);            
            
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                return "Solvers";
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return "1.0";
            }
        }

        public string AssemblyDescription
        {
            get
            {
                return "";
            }
        }

        public string AssemblyProduct
        {
            get
            {
                return "Solvers";
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                return "Петров Алексей";
            }
        }

        
        #endregion

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
