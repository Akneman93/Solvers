using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Solvers.Algorithms;
using Solvers.Interfaces;
using Solvers.Algorithms.Exceptions;
using Solvers.GridWorldBench;

namespace Solvers.Forms
{
    public partial class GridWorldForm2 : Form
    {

        double SumQ = 0;

        public GridWorldForm2(GridWorldMap gw1, int blockSize, ISolver A, INode start, INode goal)
        {           

            //set image
            InitializeComponent();

            GridWorldMap gwMap = DrawResults(gw1, blockSize, A.getPolicy(), start, goal);




            pictureBox1.Width = gwMap.BitMap.Width;
            pictureBox1.Height = gwMap.BitMap.Height;
            pictureBox1.Image = gwMap.BitMap;
            pictureBox1.Update();            


            // print solver parameters to dataview
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Search parameters";
            ResultView.Columns.Add(col);
            ResultView.RowHeadersWidth = 50;

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Values";
            ResultView.Columns.Add(col);
            ResultView.RowHeadersWidth = 50;

            DataGridViewRow row;

            int i = 0;
            foreach (KeyValuePair<string, string> pair in A.GetSearchInfo().SearchResults.Union(A.GetSearchInfo().SearchParameters))
            {

                row = new DataGridViewRow();

                row.HeaderCell.Value = String.Format("{0}", i + 1);

                ResultView.Rows.Add(row);

                ResultView.Rows[i].Cells[0].Value = pair.Key;
                ResultView.Rows[i].Cells[1].Value = pair.Value;
                i += 1;
            }


            //add total reward
            row = new DataGridViewRow();
            row.HeaderCell.Value = String.Format("{0}", i + 1);
            ResultView.Rows.Add(row);
            ResultView.Rows[i].Cells[0].Value = "Cummulative reward";
            ResultView.Rows[i].Cells[1].Value = SumQ.ToString();

            ResultView.Width = col.Width + ResultView.RowHeadersWidth;
            ResultView.Update();





        }        


        private void GridWorldForm2_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ResultView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public GridWorldMap DrawResults(GridWorldMap gw1, int blockSize, IPolicy policy, INode start, INode goal)
        {            

            GridWorldMap gw2 = new GridWorldMap(gw1);

           
            GridWorldState currentState = (GridWorldState)start.State;
            IEnvironment env = new GridWorld(gw2, blockSize);

            gw2.DrawBlock(currentState.X, currentState.Y, Color.Red, blockSize);

            int i = 0;
            int maxi = 3000;


            if (policy == null)
                MessageBox.Show("Path not found");

            while (policy != null && !currentState.Equals(goal.State) && i < maxi)
            {
                i++;
                IOperator op = policy.action(currentState);

                IOutcome outcome = env.act(currentState, policy.action(currentState));

                SumQ += outcome.Reward;

                currentState = (GridWorldState)outcome.State;

                gw2.DrawBlock(currentState.X, currentState.Y, Color.Red, blockSize);
            }

            if (i >= maxi)
                MessageBox.Show("Path not found"); ;

            return gw2;
        }



    }
}
