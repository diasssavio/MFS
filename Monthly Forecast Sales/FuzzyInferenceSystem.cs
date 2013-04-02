using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monthly_Forecast_Sales
{
    class FuzzyInferenceSystem
    {
        public double[] Inputs { get; set; }
        public Set[] Sets { get; set; }
        public List<Rule> BaseRules { get; set; }

        public Pair<double, double>[] PertinenceDegrees { get; set; }
        public Pair<string, string>[] Labels { get; set; }
        public string[][] Combinations { get; set; }

        private List<Pair<string, double>> Outputs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="setsAmount"></param>
        /// <param name="securityMargin"></param>
        public FuzzyInferenceSystem(Set[] sets, List<Rule> baseRules)
        {
            // Initializing variables
            Sets = sets;
            BaseRules = baseRules;
        }

        /// <summary>
        /// Executa o sistema de inferência fuzzy com todas suas etapas
        /// </summary>
        /// <param name="inputs">Entrada da janela do sistema</param>
        /// <returns>Saída inferida pelo sistema</returns>
        public double DoIt(double[] inputs)
        {
            // Defining inputs
            Inputs = inputs;
            
            // Running fuzzy inference System
            Fuzzification();
            Inference();
            return Defuzzification();
        }

        /// <summary>
        /// Realiza o processo de fuzzificação das entradas do sistema
        /// </summary>
        private void Fuzzification()
        {
            // Storing degrees and labels for each two sets possible
            PertinenceDegrees = new Pair<double, double>[Inputs.Length];
            Labels = new Pair<string, string>[Inputs.Length];

            // Calculating inputs' pertinence in each set
            for (int i = 0; i < Inputs.Length; i++)
            {
                PertinenceDegrees[i] = new Pair<double, double>();
                Labels[i] = new Pair<string, string>();

                // Getting the pairs of sets that is being activated and its pertinence degrees
                for (int j = 0; j < Sets.Length; j++)
                {
                    if (Sets[j].getPertinenceDegree(Inputs[i]) > 0.0)
                        if (PertinenceDegrees[i].First == default(double))
                        {
                            PertinenceDegrees[i].First = Sets[j].getPertinenceDegree(Inputs[i]);
                            Labels[i].First = Sets[j].Label;
                        }
                        else
                        {
                            PertinenceDegrees[i].Second = Sets[j].getPertinenceDegree(Inputs[i]);
                            Labels[i].Second = Sets[j].Label;
                        }
                }
            }

            // Combination of months to define which rules will be activated
            doCombinations();
        }

        /// <summary>
        /// Realiza a inferência, aplicando as regras possiveis para as entradas
        /// Determina e aplica a quais regras serão ativadas
        /// </summary>
        private void Inference()
        {
            Outputs = new List<Pair<string, double>>();

            // Determining which rules will be activated and with which combination of entries
            List<Pair<int, int>> toActivate = new List<Pair<int, int>>();
            for (int i = 0; i < Combinations.Length; i++)
                for (int j = 0; j < BaseRules.Count; j++)
                    if (Compare(BaseRules[j].Conditions, Combinations[i]))
                        toActivate.Add( new Pair<int, int>(i, j) );

            // Activating rules and applying fuzzy operators to get conclusion of rule
            foreach (Pair<int, int> activated in toActivate)
            {
                // Calculating pertinence of each set in conditions
                List<double> pertinences = new List<double>();
                for (int i = 0; i < Inputs.Length; i++)
                    pertinences.Add(Combinations[activated.First][i] == Labels[i].First ? PertinenceDegrees[i].First : PertinenceDegrees[i].Second);
                    
                // Storing label and pertinence of conclusion
                Outputs.Add(new Pair<string, double>(BaseRules[activated.Second].Conclusion, pertinences.Min()));
            }
        }

        /// <summary>
        /// Realiza o processo de defuzzificação das saídas obtidas na inferência
        /// </summary>
        /// <returns>Saída real do sistema de inferência Fuzzy</returns>
        private double Defuzzification()
        {
            // Defuzzification by the height method
            double output = 0.0;
            double toDivide = 0.0;
            foreach (Pair<string, double> pair in Outputs)
            {
                output += pair.Second * getSet(pair.First).Peak;
                toDivide += pair.Second;
            }
            return output / toDivide;
        }

        /// <summary>
        /// Faz a combinação dos conjuntos possíveis de cada mês
        /// </summary>
        private void doCombinations()
        { 
            Combinations = new string[(int)Math.Pow(2, Inputs.Length)][];
            for (int i = 0; i < Combinations.Length; i++)
                Combinations[i] = new string[Inputs.Length];

            int flag = Combinations.Length / 2;
            for (int i = 0; i < Inputs.Length; i++)
            {
                bool first = true;
                for (int j = 0; j < Combinations.Length; j += flag)
                {
                    for(int k = j; k < j + flag; k++)
                        if (first)
                            Combinations[k][i] = Labels[i].First;
                        else
                            Combinations[k][i] = Labels[i].Second;

                    first = !first;
                }
                
                flag /= 2;
            }
        }

        /// <summary>
        /// Retorna uma regra da base de regras que contenha a condição passada como parâmetro
        /// </summary>
        /// <param name="conditions">condição para checagem</param>
        /// <returns>Regra na base de regras com conditions de condição</returns>
        private Rule getRule(string[] conditions)
        {
            foreach (Rule rule in BaseRules)
                if (rule.Conditions == conditions)
                    return rule;

            return null;
        }

        /// <summary>
        /// Retorna o conjunto de rótulo label
        /// </summary>
        /// <param name="label">rótulo do conjunto a ser encontrado</param>
        /// <returns>conjunto</returns>
        private Set getSet(string label)
        {
            foreach (Set set in Sets)
                if (set.Label == label)
                    return set;
            return null;
        }

        private bool Compare(string[] var1, string[] var2)
        {
            if (var1.Length == var2.Length)
            {
                for (int i = 0; i < var1.Length; i++)
                    if (!var1[i].Equals(var2[i]))
                        return false;
                return true;
            }
            else
                return false;
        }
    }
}
