using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monthly_Forecast_Sales
{
    class WangMendelAlgorithm
    {
        public double[] Inputs { get; set; }

        // Rules
        public List<Rule> GeneratedRules { get; set; }
        public List<Rule> BaseRules { get; set; }

        // Fuzzy Sets
        public Set[] Sets { get; set; }

        /// <summary>
        /// Construtor do algoritmo Wang-Mendel
        /// </summary>
        /// <param name="inputs">Entradas para o algoritmo</param>
        /// <param name="sets">Quantidade de conjuntos</param>
        public WangMendelAlgorithm(double[] inputs, Set[] sets)
        {
            // Initializing Variables
            Inputs = inputs;
            GeneratedRules = new List<Rule>();
            BaseRules = new List<Rule>();
            Sets = sets;

            // Generating Rules //
            GenerateRules();

            // Determining base of rules //
            GenerateBaseRules();
        }

        /// <summary>
        /// Verifica o mês passado como parametro e retorna uma string com o nome do mês
        /// </summary>
        /// <param name="input">mês representado por um inteiro (entradas)</param>
        /// <returns>nome do mês</returns>
        public string MapInput(int input)
        {
            string[] months = { "janeiro", "fevereiro", "março", "abril", "maio", "junho", "julho", "agosto", "setembro", "outubro", "novembro", "dezembro" };
            return months[input % 12];
        }

        /// <summary>
        /// Gera todas as regras possíveis com as entradas
        /// </summary>
        public void GenerateRules()
        {
            // Iterating through entries in the determined window (12 months)
            for (int month = 12; month < Inputs.Length; month++)
            {
                string[] conditions = new string[12];
                double[] degrees = new double[13];

                // Calculating conditions of rule
                for (int i = month - 12, k = 0; i < month; i++, k++)
                {
                    double[] pertinence = new double[Sets.Length];
                    for (int j = 0; j < Sets.Length; j++)
                        pertinence[j] = Sets[j].getPertinenceDegree(Inputs[i]);

                    int maxIndex = 0;
                    for(maxIndex = 0; maxIndex < pertinence.Length; maxIndex++)
                        if(pertinence[maxIndex] == pertinence.Max())
                            break;

                    conditions[k] = Sets[maxIndex].Label;
                    degrees[k] = pertinence[maxIndex];
                }
                
                // Calculating conclusion of rule
                double[] pertinenceOfConclusion = new double[Sets.Length];
                for(int j = 0; j < Sets.Length; j++)
                    pertinenceOfConclusion[j] = Sets[j].getPertinenceDegree(Inputs[month]);

                int index = 0;
                for(index = 0; index < pertinenceOfConclusion.Length; index++)
                    if(pertinenceOfConclusion[index] == pertinenceOfConclusion.Max())
                        break;

                degrees[12] = pertinenceOfConclusion[index];
                string conclusion = Sets[index].Label;

                // Calculating Degree of rule
                double degree = 1.0;
                foreach(double value in degrees)
                    degree *= value;

                // Adding rule to generated rules
                GeneratedRules.Add( new Rule(conditions, conclusion, degree) );
            }
        }

        /// <summary>
        /// Gera a base de regras e as armazena em BaseRules
        /// </summary>
        public void GenerateBaseRules() {
            BaseRules = new List<Rule>(GeneratedRules);

            BaseRules.Sort();

            // Eliminating redundant and conflitant rules
            for (int i = 0; i < BaseRules.Count - 1; i++)
            {
                for (int j = i + 1; j < BaseRules.Count; j++)
                {
                    if (BaseRules[i].CompareTo(BaseRules[j]) == 0)
                    {
                        if (BaseRules[i].Degree >= BaseRules[j].Degree)
                            BaseRules[j].Degree = -1;
                        else
                        {
                            BaseRules[i].Degree = -1;
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < BaseRules.Count; i++)
                if (BaseRules[i].Degree == -1)
                    BaseRules.Remove(BaseRules.ElementAt(i));
        }
    }
}
