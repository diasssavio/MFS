using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monthly_Forecast_Sales
{
    public partial class Form1 : Form
    {
        private FuzzyInferenceSystem fuzzySystem;
        private Dictionary<string, double[]> month;
        private Dictionary<string, double> real;

        public Form1()
        {
            InitializeComponent();

            month = new Dictionary<string, double[]>();
            real = new Dictionary<string, double>();

            // 2011 Entries
            month.Add("Jan/2011", new double[] { -20.96, -5.54, 10.74, -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71 });
            real.Add("Jan/2011", -20.49);
            month.Add("Fev/2011", new double[] { -5.54, 10.74, -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49 });
            real.Add("Fev/2011", -6.35);
            month.Add("Mar/2011", new double[] { 10.74, -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35 });
            real.Add("Mar/2011", 10.14);
            month.Add("Abr/2011", new double[] { -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14 });
            real.Add("Abr/2011", 8);
            month.Add("Mai/2011", new double[] { 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8 });
            real.Add("Mai/2011", -10.92);
            month.Add("Jun/2011", new double[] { -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92 });
            real.Add("Jun/2011", -2.49);
            month.Add("Jul/2011", new double[] { 4.21, -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49 });
            real.Add("Jul/2011", 6.41);
            month.Add("Ago/2011", new double[] { -1.37, -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49, 6.41 });
            real.Add("Ago/2011", -1.84);
            month.Add("Set/2011", new double[] { -0.11, 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49, 6.41, -1.84 });
            real.Add("Set/2011", -0.22);
            month.Add("Out/2011", new double[] { 7.82, -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49, 6.41, -1.84, -0.22 });
            real.Add("Out/2011", 5.16);
            month.Add("Nov/2011", new double[] { -4.43, 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16 });
            real.Add("Nov/2011", -1.84);
            month.Add("Dez/2011", new double[] { 34.71, -20.49, -6.35, 10.14, 8, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84 });
            real.Add("Dez/2011", 30.66);

            // 2012 Entries
            month.Add("Jan/2012", new double[] { -20.49, -6.35, 10.14, 8.0, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66 });
            real.Add("Jan/2012", -18.91);
            month.Add("Fev/2012", new double[] { -6.35, 10.14, 8.0, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91 });
            real.Add("Fev/2012", 0.27);
            month.Add("Mar/2012", new double[] { 10.14, 8.0, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27 });
            real.Add("Mar/2012", 7.54);
            month.Add("Abr/2012", new double[] { 8.0, -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54 });
            real.Add("Abr/2012", -1.37);
            month.Add("Mai/2012", new double[] { -10.92, -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37 });
            real.Add("Mai/2012", -2.37);
            month.Add("Jun/2012", new double[] { -2.49, 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37 });
            real.Add("Jun/2012", -5.39);
            month.Add("Jul/2012", new double[] { 6.41, -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39 });
            real.Add("Jul/2012", -0.08);
            month.Add("Ago/2012", new double[] { -1.84, -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39, -0.08 });
            real.Add("Ago/2012", 2.12);
            month.Add("Set/2012", new double[] { -0.22, 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39, -0.08, 2.12 });
            real.Add("Set/2012", 0.79);
            month.Add("Out/2012", new double[] { 5.16, -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39, -0.08, 2.12, 0.79 });
            real.Add("Out/2012", 2.80);
            month.Add("Nov/2012", new double[] { -1.84, 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39, -0.08, 2.12, 0.79, 2.8 });
            real.Add("Nov/2012", 2.13);
            month.Add("Dez/2012", new double[] { 30.66, -18.91, 0.27, 7.54, -1.37, -2.37, -5.39, -0.08, 2.12, 0.79, 2.8, 2.13 });
            real.Add("Dez/2012", 29.73);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double[] inputs = { -27.75, 1.47, 4.29, 6.51, -4.22, -2.79, 7.76, -1.24, -1.45, 7.99, -3.24, 36.31,
                                -25.97, -4.22, 16.09, -8.23, -0.35, -3.85, 6.63, -3.3, -2.1, 7.13, -3.01, 35.6,
                                -25.38, -3.99, 6.14, 5.23, -8.54, -2.07, 4.78, -1.12, 1.12, 2.32, 1.18, 31.79,
                                -23.29, -2.41, 10.79, -0.77, -4.51, -0.9, 1.56, 2.37, 0.47, 1.63, 1.88, 33.56,
                                -22.84, -2.35, 16.76, -12.13, 8.66, -5.65, 4.4, 3.57, -5.45, 7.22, 1.19, 27.86,
                                -22.59, -4.12, 6.68, 7.2, -3.67, -5.25, 5.92, 0.95, -3.77, 8.33, -2.24, 31.68,
                                -20.96, -5.54, 10.74, -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71 };

            // Setting domain with security margin
            double marginMin = inputs.Min();
            double marginMax = inputs.Max();

            double value = (int.Parse(textBox2.Text) / 100.0) * (marginMax - marginMin);
            marginMin -= value;
            marginMax += value;

            // Sets
            Set[] sets = new Set[int.Parse(textBox1.Text)];

            // Determining sets
            double sizeOfSet = (marginMax - marginMin) / (sets.Length - 1);    // 2*N + 1
            sets[0] = new Set("set0", marginMin - sizeOfSet, marginMin, marginMin + sizeOfSet);
            for (int i = 1; i < sets.Length; i++)
                sets[i] = new Set("set" + i, sets[i - 1].Peak, sets[i - 1].End, sets[i - 1].End + sizeOfSet);

            // Getting the base of rules
            WangMendelAlgorithm algorithm = new WangMendelAlgorithm(inputs, sets);

            // Initializing the fuzzy system
            fuzzySystem = new FuzzyInferenceSystem(sets, algorithm.BaseRules);

            for(int i = 0; i < algorithm.BaseRules.Count; i++)
                richTextBox1.Text += (i+1) + ") " + algorithm.BaseRules[i].ToString() + "\n\n";

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = fuzzySystem.DoIt(month[comboBox1.SelectedItem.ToString()]).ToString();
            textBox3.Visible = true;
            textBox4.Text = real[comboBox1.SelectedItem.ToString()].ToString();
            textBox4.Visible = true;
        }
    }
}
