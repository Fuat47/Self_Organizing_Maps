using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSS_SOM
{
    public partial class DataDisplayForm : Form
    {
        private StringBuilder output;
        private int x;
        private int y;

        public DataDisplayForm(StringBuilder output, int x, int y)
        {
            InitializeComponent();

            this.output = output;
            this.x = x;
            this.y = y;
            this.Text = $"Instances in {x}, {y}:";
            ShowData();
            
        }

        private void ShowData()
        {
            // Clear the ListBox before displaying new data
            listBoxOutput.Items.Clear();

            // Split the output by newline character and add each line to the ListBox
            string[] lines = output.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                listBoxOutput.Items.Add(line);
            }
        }
    }
}
