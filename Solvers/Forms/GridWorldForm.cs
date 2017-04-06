using Solvers.Algorithms;
using Solvers.Algorithms.Astar;
using Solvers.Algorithms.Exceptions;
using Solvers.Algorithms.MCTS;
using Solvers.Algorithms.TD;
using Solvers.GridWorldBench;
using Solvers.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;





namespace Solvers.Forms
{
    public partial class GridWorldForm : Form
    {
        Thread exeThread;
        GridWorldMap gwMap;
        ISolver A;
              
        GWHCal hcal;
        ANodeSucGen sucgen;
        UCTNodeSucGen uctSucGen;
       //INode start;
       // INode goal;

        int blockSize;
        int StartX;
        int StartY;
        int GoalX;
        int GoalY;

        INode start = null;
        INode goal = null;

        List<Form> listOfForms;        



        public GridWorldForm()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            ReadyState();
            blockSizeList.SelectedItem = "Size 1";
            Bitmap bitmap = new Bitmap(400, 400);
            for (int i = 0; i < bitmap.Height; i++)
                for (int j = 0; j < bitmap.Width; j++)
                    bitmap.SetPixel(j, i, Color.White);
            gwMap = new GridWorldMap(bitmap);
            pictureBox1.Width = bitmap.Width;
            pictureBox1.Height = bitmap.Height;
            pictureBox1.Image = bitmap;
            pictureBox1.Update();                   
            blockSize = 1;
            listOfForms = new List<Form>();
          


            StartX = 0;
            StartY = 0;
            GoalX = (gwMap.BitMap.Height / blockSize) * blockSize - blockSize;
            GoalY = (gwMap.BitMap.Height / blockSize) * blockSize - blockSize;


            hcal = new GWHCal(blockSize);
            sucgen = new ANodeSucGen(new GridWorld(gwMap, blockSize));
            uctSucGen = new UCTNodeSucGen(new GridWorld(gwMap, blockSize));           
                  
                      
            radioButton1.Checked = true;
            blockSizeList.Text = blockSizeList.Items[0].ToString();

            InitialState();

        }




        private void generateBtn_Click(object sender, EventArgs e)
        {
            GeneratingState();
            GenerateBtn.Enabled = false;
            blockSize = 1;
            if (blockSizeList.SelectedItem == blockSizeList.Items[0])
                blockSize = 1;
            else
                if (blockSizeList.SelectedItem == blockSizeList.Items[1])
                blockSize = 4;
            else
                    if (blockSizeList.SelectedItem == blockSizeList.Items[2])
                blockSize = 10;
            else
                        if (blockSizeList.SelectedItem == blockSizeList.Items[3])
                blockSize = 40;
            else
                            if (blockSizeList.SelectedItem == blockSizeList.Items[4])
                blockSize = 80;
            StartX = 0;
            StartY = 0;
            GoalX = (gwMap.BitMap.Height / blockSize) * blockSize - blockSize;
            GoalY = (gwMap.BitMap.Height / blockSize) * blockSize - blockSize;



            Random random = new Random();
            gwMap = new GridWorldMap(400, 400);
            gwMap.CreateBlocks(blockSize);
            gwMap.DrawBlock(StartX, StartY, Color.Red, blockSize);
            gwMap.DrawBlock(GoalX, GoalY, Color.Green, blockSize);
            Bitmap bitmap = new Bitmap(gwMap.BitMap);
            pictureBox1.Width = bitmap.Width;
            pictureBox1.Height = bitmap.Height;
            pictureBox1.Image = bitmap;
            pictureBox1.Update();            
            ReadyState();

        }

        private void beginBtn_Click(object sender, EventArgs e)
        {

            if (exeThread != null && exeThread.IsAlive)
                return;
            InSearchState();             
            
            hcal.BlockSize = blockSize;
            

            if (radioButton1.Checked)
            {
                A = new AWA(new ANodeSucGen(new GridWorld(gwMap, blockSize)), hcal, 1);
                start = new ANode();               
                goal = new ANode();
                
            }
            else
                if (radioButton3.Checked)

            {
                A = new AWinA(new ANodeSucGen(new GridWorld(gwMap, blockSize)), hcal);
                start = new ANode();
                goal = new ANode();
            }
            else
                    if (radioButton2.Checked)
            {
                A = new Astar(new ANodeSucGen(new GridWorld(gwMap, blockSize)), hcal);
                start = new ANode();
                goal = new ANode();
            }
            else
                        if (radioButton5.Checked)
            {
                A = new QLearning(new GridWorld(gwMap, blockSize), 0.5, 0.99, 0.2);
                start = new QNode();
                goal = new QNode();
            }
            else
             if (radioButton4.Checked)
            {
                A = new UCT(new UCTNodeSucGen(new GridWorld(gwMap, blockSize)), 2, 30);
                start = new UCTNode();
                goal = new UCTNode();
            }
           
            start.State = new GridWorldState(StartX, StartY);
            start.UsedOperator = new DefaultOperator("Init");
            start.Parent = null;
            
            goal.State = new GridWorldState(GoalX, GoalY);
            goal.UsedOperator = new DefaultOperator("Init");
            goal.Parent = null;





            if (!SetParameters(A))
            {
                ReadyState();
                return;
            }                    


            A.InitStates(start, goal);

            exeThread = new Thread(() => A.Execute());

            exeThread.Start();


            while (exeThread.IsAlive)
            {
                Application.DoEvents();
            }

            StoppedState();             
            
           
            GridWorldForm2 sf = new GridWorldForm2(gwMap, blockSize, A, start, goal);
            sf.Show();
            


            
        }

        private void ShowBtn_Click(object sender, EventArgs e)
        {
            if (A == null)
                return;
            
            GridWorldForm2 sf = new GridWorldForm2(gwMap, blockSize, A, start, goal);
            sf.Show();
           

           
        }



        private void button4_Click(object sender, EventArgs e)
        {
            StoppedState();


        }
     



        public GridWorldMap DrawResults(GridWorldMap gw1)
        {
            if (gw1 == null || A == null)
                return null;

            GridWorldMap gw2 = new GridWorldMap(gw1);


            IPolicy policy = A.getPolicy();
            GridWorldState currentState = (GridWorldState) start.State;
            GridWorldState goalState = (GridWorldState)goal.State;
            IEnvironment env = new GridWorld(gwMap, blockSize);

            gw2.DrawBlock(currentState.X, currentState.Y, Color.Red, blockSize);
            gw2.DrawBlock(goalState.X, goalState.Y, Color.Green, blockSize);


            int i = 0;
            int maxi = 3000;

            while (policy != null && !currentState.Equals(goal.State) && i < maxi)
            {
                i++;
                IOperator op = policy.action(currentState);             

                IOutcome outcome = env.act(currentState, policy.action(currentState));

                currentState = (GridWorldState)outcome.State;

                gw2.DrawBlock(currentState.X, currentState.Y, Color.Red, blockSize);
            }

            if (i >= maxi)
                MessageBox.Show("Path not found"); ;

            return gw2;
        }



        private void InitialState()
        {
            GenerateBtn.Enabled = true;
            BeginBtn.Enabled = false;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = false;
        }




        private void InSearchState()
        {
            GenerateBtn.Enabled = false;
            BeginBtn.Enabled = false;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = true;            
        }

        private void StoppedState()
        {
            GenerateBtn.Enabled = true;
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = true;
            StopBtn.Enabled = false;          
           
        }

        private void ReadyState()
        {
            GenerateBtn.Enabled = true;
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = false;          
        }


        private void EndState()
        {
            GenerateBtn.Enabled = true;
            BeginBtn.Enabled = true;
            ShowBtn.Enabled = true;
            StopBtn.Enabled = false;           
        }


        private void GridWorldForm_FormClosed(object sender, FormClosedEventArgs e)
        {
                     
        }


        private void GeneratingState()
        {
            GenerateBtn.Enabled = false;
            BeginBtn.Enabled = false;
            ShowBtn.Enabled = false;
            StopBtn.Enabled = false;          
           
        }
      
                

       

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void puzzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NPuzzleForm nf = new NPuzzleForm();
            nf.Show();
            this.Close();
        }

        private void gridWorldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridWorldForm gwf = new GridWorldForm();
            gwf.Show();
            this.Close();
        }
        
       

       

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            listOfForms.Add(ab);
            ab.ShowDialog();
        }

        private void GridWorldForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            for (int i = 0; i < listOfForms.Count; i++)
                listOfForms[i].Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        private void TimeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
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
                ParametersFrom pf = new ParametersFrom(info);
                pf.ShowDialog();

                if (pf.DialogResult != DialogResult.OK)
                    return false;

                solver.setParameters(info.SearchParameters);


            }
            catch (Exception ex)
            {
                MessageBox.Show("error in parameters");
                ReadyState();
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
