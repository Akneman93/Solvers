using Solvers.Algorithms;
using Solvers.Algorithms.Astar;
using Solvers.Algorithms.MCTS;
using Solvers.Interfaces;
using Solvers.NPuzzleBench;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;





namespace Solvers.Forms
{
    public partial class NPuzzleForm : Form
    {
        Thread exeThread;
        ISolver A;       
      
        NPuzzleManDistCal hcal;
        ANodeSucGen sucgen;
        UCTNodeSucGen uctSucGen;

        INode start;
        INode goal;

        Chart chart;
        int N;


        byte[] startTiles = { 2, 10, 0, 3, 14, 15, 1, 7, 6, 8, 4, 13, 12, 9, 5, 11};
        byte[] goalTiles = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
        

        List<Form> listOfForms;

 
        public NPuzzleForm()
        {
            InitializeComponent();
            InitState();

            N = 4;         
            
           
            hcal = new NPuzzleManDistCal(N);
			sucgen = new ANodeSucGen(new Solvers.NPuzzleBench.NPuzzle(N));
            uctSucGen = new UCTNodeSucGen(new Solvers.NPuzzleBench.NPuzzle(N));

            ConfigureDGV(N);
            FillDGV(startTiles, N);                    
            chart = new Chart();           
            radioButton1.Checked = true;
            listOfForms = new List<Form>();
           
        }

        private void ConfigureDGV(int N)
        {
            int size = 40;
            for (int i = 0; i < N; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Width = size;
                dataGridView1.Columns.Add(col);
            }


            for (int i = 0; i < N; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = size;
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.Height = N * size + 3;
            dataGridView1.Width = N * size + 3;

            dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            dataGridView1.DefaultCellStyle.SelectionForeColor = dataGridView1.DefaultCellStyle.ForeColor;

        }

        public IState GetStartState()
        {
            int size = dataGridView1.Rows.Count;
            byte[] tiles = new byte[size * size];
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    tiles[size * i + j] = Convert.ToByte(dataGridView1.Rows[i].Cells[j].Value.ToString());		
            return new NPuzzleState(tiles);
        }

        public bool FillDGV(byte[] tiles, int N)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    dataGridView1.Rows[i].Cells[j].Value = tiles[N * i + j].ToString();
            dataGridView1.Update();
            return true;
        }


      

        private void BeginBtn_Click(object sender, EventArgs e)
        {
            InSearchState();




            if (radioButton1.Checked)
            {
                A = new AWA(sucgen, hcal, 1);
                start = new ANode();
                goal = new ANode();
            }
            else
            if (radioButton2.Checked)
            {
                A = new Astar(sucgen, hcal);
                start = new ANode();
                goal = new ANode();
            }
            else
            if (radioButton3.Checked)
            {
                A = new AWinA(sucgen, hcal);
                start = new ANode();
                goal = new ANode();
            }
            else
            {
                A = new UCT(uctSucGen, 2, 30);
                start = new UCTNode();
                goal = new UCTNode();
            }


            if (!SetParameters(A))
            {
                InitState();
                return;
            }


            start.Parent = null;
            start.State = GetStartState();
            start.UsedOperator = new DefaultOperator("init");

            goal.Parent = null;
            goal.State = new NPuzzleState(goalTiles);
            goal.UsedOperator = new DefaultOperator("init");

            
            
            A.InitStates(start, goal);

            




            exeThread = new Thread(() => A.Execute());

            exeThread.Start();

            
            while (exeThread.IsAlive)
            {
                Application.DoEvents();                
            }                      
            
            StoppedState();       

            IPolicy policy = A.getPolicy();            
          
            SolutionForm sf = new SolutionForm(A, new Solvers.NPuzzleBench.NPuzzle(N), start, goal);

            sf.Show();
                     
      
                
            

        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
           
            if (A == null)
                return;
            SolutionForm sf = new SolutionForm(A, new Solvers.NPuzzleBench.NPuzzle(N),start,goal);
            sf.Show();

                        
        }
              



        private void InSearchState()
        {
          
            BeginBtn.Enabled = false;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = true;

        }

        private void StoppedState()
        {
         
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = true;
            StopBtn.Enabled = false;

        }

        private void InitState()
        {          
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = false;
        }


        private void EndState()
        {        
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = true;
            StopBtn.Enabled = false;
        }     


        private void GeneratingState()
        {
           
            BeginBtn.Enabled = false;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = false;
        }

      
        


        private void Graph_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm(A.GetSearchInfo());
            listOfForms.Add(cf);
            cf.ShowDialog();
        }       
       

       

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            listOfForms.Add(ab);
            ab.ShowDialog();
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NPuzzleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            for (int i = 0; i < listOfForms.Count; i++)
                listOfForms[i].Close();
        }
        

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void GraphBtn_Click(object sender, EventArgs e)
        {
            if (A == null)
                return;
            ChartForm cf = new ChartForm(A.GetSearchInfo());
            cf.Show();
        }


        public bool SetParameters(ISolver solver)
        {
            try
            {
                ISearchInfo info = solver.GetSearchInfo();
                ParametersForm pf = new ParametersForm(info);
                pf.ShowDialog();

                if (pf.DialogResult != DialogResult.OK)
                    return false;

                solver.setParameters(info.SearchParameters);

            }
            catch (Exception ex)
            {
                MessageBox.Show("error in parameters");
                InitState();
                return false;
            }

            return true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (A != null && exeThread != null && exeThread.IsAlive)
            {
                A.Stop();
            }
        }
    }
}




