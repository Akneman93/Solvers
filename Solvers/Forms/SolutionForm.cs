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
using Solvers.NPuzzleBench;

namespace Solvers.Forms
{
    public partial class SolutionForm : Form
    {

        List<IState> stateList = new List<IState>();
        int N = 4;

        public SolutionForm(ISolver A, IEnvironment env, INode start, INode goal)
        {            

            InitializeComponent();
            
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Operators";
            col.SortMode = DataGridViewColumnSortMode.NotSortable;
            operatorsView.Columns.Add(col);
            operatorsView.RowHeadersWidth = 50;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            IState currentState = start.State;                     
            IPolicy policy = A.getPolicy();
            ISearchInfo info = A.GetSearchInfo();

            if (policy == null)
                MessageBox.Show("No solution found");


            //use policy to find solution     

            int i = 0;
            int maxi = 3000;
            double SumQ = 0;
            DataGridViewRow row;
            while (policy != null && !currentState.Equals(goal.State) && i < maxi)
            {
                row = new DataGridViewRow();

                row.HeaderCell.Value = String.Format("{0}", i + 1);               

                IOperator op = policy.action(currentState);

                operatorsView.Rows.Add(row);

                operatorsView.Rows[i].Cells[0].Value = op.Name;                

                IOutcome outcome = env.act(currentState, policy.action(currentState));

                currentState = outcome.State;
                stateList.Add(outcome.State);

                SumQ += outcome.QValue;

                i += 1;
            }

            if (i >= maxi)
            {
                MessageBox.Show("No solution found");
                stateList.Clear();
            }            
            operatorsView.Update();

            // print solver parameters + results
            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Properties";
            ResultView.Columns.Add(col);
            ResultView.RowHeadersWidth = 50;

            col = new DataGridViewTextBoxColumn();
            col.HeaderText = "Values";
            ResultView.Columns.Add(col);
            ResultView.RowHeadersWidth = 50;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            i = 0;
            foreach (KeyValuePair<string, string> pair in info.SearchResults.Union(info.SearchParameters))
            {                
                row = new DataGridViewRow();

                row.HeaderCell.Value = String.Format("{0}", i + 1);

                ResultView.Rows.Add(row);

                ResultView.Rows[i].Cells[0].Value = pair.Key;
                ResultView.Rows[i].Cells[1].Value = pair.Value;
                i += 1;
            }


            //print total reward
            row = new DataGridViewRow();
            row.HeaderCell.Value = String.Format("{0}", i + 1);
            ResultView.Rows.Add(row);
            ResultView.Rows[i].Cells[0].Value = "Cummulative reward";
            ResultView.Rows[i].Cells[1].Value = SumQ.ToString();

            ResultView.Width = col.Width + ResultView.RowHeadersWidth + 20;
            ResultView.Update();

            ConfigureDGV(N);
            NPuzzleState npState = (NPuzzleState)start.State;
            FillDGV(npState.Tiles, N);


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SolutionForm_Load(object sender, EventArgs e)
        {

        }

        private void ResultView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void puzzleView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        public bool FillDGV(byte[] tiles, int N)
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    puzzleView.Rows[i].Cells[j].Value = tiles[N * i + j].ToString();
                    if (tiles[N * i + j] == 0)
                        puzzleView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    else
                        puzzleView.Rows[i].Cells[j].Style.BackColor = Color.White;

                }
            puzzleView.Update();
            return true;
        }


        private void ConfigureDGV(int N)
        {
            int size = 40;
            for (int i = 0; i < N; i++)
            {
                DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
                col.Width = size;
                puzzleView.Columns.Add(col);
            }


            for (int i = 0; i < N; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = size;
                puzzleView.Rows.Add(row);
            }
            puzzleView.Height = N * size + 3;
            puzzleView.Width = N * size + 3;

            puzzleView.DefaultCellStyle.SelectionBackColor = puzzleView.DefaultCellStyle.BackColor;
            puzzleView.DefaultCellStyle.SelectionForeColor = puzzleView.DefaultCellStyle.ForeColor;

        }
      

        private void operatorsView_SelectionChanged(object sender, EventArgs e)
        {
            if (operatorsView.SelectedCells.Count == 1 && stateList.Count != 0)
            {               
                int index = operatorsView.SelectedCells[0].RowIndex;
                if (index < stateList.Count)
                {
                    NPuzzleState npState = (NPuzzleState)stateList[index];
                    FillDGV(npState.Tiles, N);
                }
            }
        }
    }
}
