using System.IO;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DSS_SOM
{
    public partial class MainForm : Form
    {
        private List<Button> buttons = new List<Button>();
        private string path;
        private static List<int>[][] dataWithCoordinate;
        private static string labels;
        private static List<string> dataset;
        public MainForm()
        {
            InitializeComponent();
        }
        public class Map
        {
            private Neuron[,] outputs;  // Collection of weights.
            private int iteration;      // Current iteration.
            private int length;        // Side length of output grid.
            private int dimensions;    // Number of input dimensions.
            private Random rnd = new Random();

            private List<string> labels = new List<string>();
            private List<double[]> patterns = new List<double[]>();

            private List<string> patterns_with_target_attr = new List<string>();

            public Map(int dimensions, int length, string file)
            {
                this.length = length;
                this.dimensions = dimensions;
                Initialise();
                LoadData(file);
                NormalisePatterns();
                Train(0.0000001);
                DumpCoordinates();

                double sse = 0;
                for (int i = 0; i < outputs.GetLength(0); i++)
                {
                    for (int j = 0; j < outputs.GetLength(1); j++)
                    {
                        sse += outputs[i, j].sse1();
                    }
                }

                Console.WriteLine(sse);
                Console.ReadLine();
            }

            private void Initialise()
            {
                outputs = new Neuron[length, length];
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        outputs[i, j] = new Neuron(i, j, length);
                        outputs[i, j].Weights = new double[dimensions];
                        outputs[i, j].total_attr_values = new double[dimensions];
                        for (int k = 0; k < dimensions; k++)
                        {
                            outputs[i, j].Weights[k] = rnd.NextDouble();
                        }
                    }
                }
            }

            private void LoadData(string file)
            {
                StreamReader reader = File.OpenText(file);
                MainForm.labels = reader.ReadLine()!;
                MainForm.dataset = new List<string>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine()!;
                    MainForm.dataset.Add(line);
                    string[] splitLine = line.Split(',');
                    labels.Add(splitLine[0]);
                    double[] inputs = new double[dimensions];
                    for (int i = 0; i < dimensions; i++)
                    {
                        inputs[i] = double.Parse(splitLine[i]);

                    }
                    patterns.Add(inputs);
                    patterns_with_target_attr.Add(splitLine[dimensions]);
                }
                reader.Close();
            }

            private void NormalisePatterns()
            {
                for (int j = 0; j < dimensions; j++)
                {
                    //double sum = 0;
                    double max = 0;

                    for (int i = 0; i < patterns.Count; i++)
                    {
                        if (patterns[i][j] > max) max = patterns[i][j];
                        //sum += patterns[i][j];
                    }
                    //double average = sum / patterns.Count;
                    for (int i = 0; i < patterns.Count; i++)
                    {
                        //patterns[i][j] = patterns[i][j] / average;
                        patterns[i][j] = patterns[i][j] / max;
                    }
                }
            }

            private void Train(double maxError)
            {
                double currentError = double.MaxValue;
                while (currentError > maxError)
                {
                    currentError = 0;
                    List<double[]> TrainingSet = new List<double[]>();
                    foreach (double[] pattern in patterns)
                    {
                        TrainingSet.Add(pattern);
                    }
                    for (int i = 0; i < patterns.Count; i++)
                    {
                        double[] pattern = TrainingSet[rnd.Next(patterns.Count - i)];
                        currentError += TrainPattern(pattern);
                        TrainingSet.Remove(pattern);
                    }
                    Console.WriteLine(currentError.ToString("0.0000000"));
                }
            }

            private double TrainPattern(double[] pattern)
            {
                double error = 0;
                Neuron winner = Winner(pattern);
                for (int i = 0; i < length; i++)
                {
                    for (int j = 0; j < length; j++)
                    {
                        error += outputs[i, j].UpdateWeights(pattern, winner, iteration);
                    }
                }
                iteration++;
                return Math.Abs(error / (length * length));
            }

            private void DumpCoordinates()
            {
                dataWithCoordinate = new List<int>[length][];
                for (int i = 0; i < length; i++)
                {
                    dataWithCoordinate[i] = new List<int>[length];
                    for (int j = 0; j < length; j++)
                    {
                        dataWithCoordinate[i][j] = new List<int>();
                    }
                }
                for (int i = 0; i < patterns.Count; i++)
                {
                    Neuron n = Winner(patterns[i]);
                    n.data.Add(patterns[i]);
                    n.tr.Add(patterns_with_target_attr[i]);

                    for (int j = 0; j < patterns[i].Length; j++)
                        n.total_attr_values[j] += patterns[i][j];

                    dataWithCoordinate[n.X][n.Y].Add(i);
                }
            }

            private Neuron Winner(double[] pattern)
            {
                Neuron winner = null;
                double min = double.MaxValue;
                for (int i = 0; i < length; i++)
                    for (int j = 0; j < length; j++)
                    {
                        double d = Distance(pattern, outputs[i, j].Weights);
                        if (d < min)
                        {
                            min = d;
                            winner = outputs[i, j];
                        }
                    }
                return winner!;
            }

            private double Distance(double[] vector1, double[] vector2)
            {
                double value = 0;
                for (int i = 0; i < vector1.Length; i++)
                {
                    value += Math.Pow((vector1[i] - vector2[i]), 2);
                }
                return Math.Sqrt(value);
            }
        }

        public class Neuron(int x, int y, int length)
        {
            public double[] Weights;
            public int X = x;
            public int Y = y;
            private int length = length;
            private double nf = 1000 / Math.Log(length);
            public List<double[]> data = new List<double[]>();
            public List<string> tr = new List<string>();

            public double sse1()
            {
                double retV = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    retV += Distance(Weights, data[i]);
                }
                return retV;
            }

            private double Distance(double[] vector1, double[] vector2)
            {
                double value = 0;
                for (int i = 0; i < vector1.Length; i++)
                {
                    value += Math.Pow((vector1[i] - vector2[i]), 2);
                }
                return value;
            }


            public double[] total_attr_values;

            private double Gauss(Neuron win, int it)
            {
                double distance = Math.Sqrt(Math.Pow(win.X - X, 2) + Math.Pow(win.Y - Y, 2));
                return Math.Exp(-Math.Pow(distance, 2) / (Math.Pow(Strength(it), 2)));
            }

            private double LearningRate(int it)
            {
                return Math.Exp(-it / 1000) * 0.1;
            }

            private double Strength(int it)
            {
                return Math.Exp(-it / nf) * length;
            }

            public double UpdateWeights(double[] pattern, Neuron winner, int it)
            {
                double sum = 0;
                for (int i = 0; i < Weights.Length; i++)
                {
                    double delta = LearningRate(it) * Gauss(winner, it) * (pattern[i] - Weights[i]);
                    Weights[i] += delta;
                    sum += delta;
                }
                return sum / Weights.Length;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Text File |*.txt"
            };

            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
                string filename = Path.GetFileName(path);
                string ext = filename.Split('.').Last();
                if (ext.Equals("txt"))
                {
                    txtDataset.Text = filename;
                }
                else
                {
                    MessageBox.Show("The format must be .txt");
                }
            }
        }

        private void btnSOM_Click(object sender, EventArgs e)
        {
            int dimension;
            if (txtDataset.Text == "" || txtDimension.Text == "")
            {
                MessageBox.Show("Please fill in the required fields");
                return;
            }
            else if (!int.TryParse(txtDimension.Text, out dimension))
            {
                MessageBox.Show("Dimension value must be an integer.");
                return;
            }
            else if (dimension == 0)
            {
                MessageBox.Show("Dimension value must be greater than 0.");
                return;
            }
            int buttonSize = 0;
            if (dimension < 11)
            {
                buttonSize = 400 / dimension;
            }
            else
            {
                buttonSize = 40;
            }
            this.Size = new Size(500, 620);

            foreach (Button btn in buttons)
            {
                this.Controls.Remove(btn);
                btn.Dispose();
            }
            buttons.Clear();

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Button button = new Button();
                    button.Text = $"{i},{j}";
                    button.Size = new Size(buttonSize - 10, buttonSize - 10);
                    button.Location = new Point(50 + i * buttonSize, 170 + j * buttonSize);
                    button.BackColor = Color.DarkGray;
                    button.Click += Button_Click!;
                    this.Controls.Add(button);
                    buttons.Add(button);
                }
            }
            StreamReader sr = new StreamReader(path);
            int lng = sr.ReadLine()!.Split(',').Length;
            new Map(lng - 1, dimension, path);

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string[] coordinates = clickedButton.Text.Split(',');
            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            List<int> dataAtCoordinates = dataWithCoordinate[x][y];

            StringBuilder output = new StringBuilder();
            output.AppendLine(labels);
            foreach (int index in dataAtCoordinates)
            {
                output.AppendLine(dataset[index]);
            }
            if (dataAtCoordinates.Count == 0)
            {
                MessageBox.Show("No data at this coordinates.", $"Instances in {x}, {y}:");
            }
            else
            {
                DataDisplayForm dataDisplayForm = new DataDisplayForm(output, x, y);
                dataDisplayForm.Show();
            }

        }

        private void txtDimension_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSOM.PerformClick();
                e.SuppressKeyPress = true; 
            }
        }
    }
}
