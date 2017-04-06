using Solvers.Algorithms;
using Solvers.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Solvers.Forms
{
    public partial class ChartForm : Form
    {
        private ISearchInfo result;
        public ChartForm(ISearchInfo result)
        {
            InitializeComponent();           
            this.result = result;


            foreach(Chart chart in result.ChartList)
            {
                flowPanel.Controls.Add(configChart(chart));
            }

            flowPanel.Update();



        }

        private Chart configChart(Chart chart)
        {           

            chart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chart.Series[0].Color = Color.Blue;
            chart.Series[0].BorderWidth = 3;
            chart.Series[0].BorderDashStyle = ChartDashStyle.Dash;

            chart.Update();


            return chart;

        }

        private void ChartForm_Load(object sender, EventArgs e)
        {

        }

        private void flowPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

     
}
