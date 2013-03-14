using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monthly_Forecast_Sales
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
             */
            double[] inputs = { -27.75, 1.47, 4.29, 6.51, -4.22, -2.79, 7.76, -1.24, -1.45, 7.99, -3.24, 36.31,
                                -25.97, -4.22, 16.09, -8.23, -0.35, -3.85, 6.63, -3.3, -2.1, 7.13, -3.01, 35.6,
                                -25.38, -3.99, 6.14, 5.23, -8.54, -2.07, 4.78, -1.12, 1.12, 2.32, 1.18, 31.79,
                                -23.29, -2.41, 10.79, -0.77, -4.51, -0.9, 1.56, 2.37, 0.47, 1.63, 1.88, 33.56,
                                -22.84, -2.35, 16.76, -12.13, 8.66, -5.65, 4.4, 3.57, -5.45, 7.22, 1.19, 27.86,
                                -22.59, -4.12, 6.68, 7.2, -3.67, -5.25, 5.92, 0.95, -3.77, 8.33, -2.24, 31.68,
                                -20.96, -5.54, 10.74, -3.28, 0.46, -4.59, 4.21, -1.37, -0.11, 7.82, -4.43, 34.71};

            WangMendelAlgorithm algorithm = new WangMendelAlgorithm( inputs, 5 );

            Console.WriteLine("Regras geradas({0}):", algorithm.GeneratedRules.Count);
            foreach (Rule rule in algorithm.GeneratedRules)
                Console.WriteLine(rule.ToString());
            Console.WriteLine("------------------------------------------------");

            //algorithm.GeneratedRules.Sort();

            //foreach (Rule rule in algorithm.GeneratedRules)
            //    Console.WriteLine(rule.ToString());
            //Console.WriteLine("------------------------------------------------");

            //algorithm.GenerateBaseRules();
            Console.WriteLine("Base de Regras:({0})", algorithm.BaseRules.Count);
            foreach (Rule rule in algorithm.BaseRules)
                Console.WriteLine(rule.ToString());
            Console.WriteLine("------------------------------------------------");
        }
    }
}
