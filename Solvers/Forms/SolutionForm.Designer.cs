namespace Solvers.Forms
{
    partial class SolutionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.operatorsView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResultView = new System.Windows.Forms.DataGridView();
            this.puzzleView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.operatorsView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.puzzleView)).BeginInit();
            this.SuspendLayout();
            // 
            // operatorsView
            // 
            this.operatorsView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.operatorsView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.operatorsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.operatorsView.Location = new System.Drawing.Point(12, 13);
            this.operatorsView.Name = "operatorsView";
            this.operatorsView.Size = new System.Drawing.Size(206, 151);
            this.operatorsView.TabIndex = 0;
            this.operatorsView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.operatorsView.SelectionChanged += new System.EventHandler(this.operatorsView_SelectionChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResultView);
            this.groupBox1.Location = new System.Drawing.Point(246, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 223);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // ResultView
            // 
            this.ResultView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultView.Location = new System.Drawing.Point(3, 16);
            this.ResultView.Name = "ResultView";
            this.ResultView.Size = new System.Drawing.Size(312, 204);
            this.ResultView.TabIndex = 0;
            this.ResultView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ResultView_CellContentClick);
            // 
            // puzzleView
            // 
            this.puzzleView.AllowUserToAddRows = false;
            this.puzzleView.AllowUserToDeleteRows = false;
            this.puzzleView.AllowUserToResizeColumns = false;
            this.puzzleView.AllowUserToResizeRows = false;
            this.puzzleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.puzzleView.ColumnHeadersVisible = false;
            this.puzzleView.Location = new System.Drawing.Point(12, 204);
            this.puzzleView.Name = "puzzleView";
            this.puzzleView.RowHeadersVisible = false;
            this.puzzleView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.puzzleView.Size = new System.Drawing.Size(226, 154);
            this.puzzleView.TabIndex = 6;
            // 
            // SolutionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 406);
            this.Controls.Add(this.puzzleView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.operatorsView);
            this.Name = "SolutionForm";
            this.Text = "Solution";
            this.Load += new System.EventHandler(this.SolutionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.operatorsView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ResultView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.puzzleView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView operatorsView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView ResultView;
        private System.Windows.Forms.DataGridView puzzleView;
    }
}