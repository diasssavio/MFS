using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monthly_Forecast_Sales
{
    /// <summary>
    /// Classe para representar um par de valores
    /// </summary>
    /// <typeparam name="T">Valor 1</typeparam>
    /// <typeparam name="U">Valor 2</typeparam>
    public class Pair<T, U>
    {
        public Pair() {}

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

    /// <summary>
    /// Representação de uma regra
    /// </summary>
    class Rule : IComparable<Rule>
    {
        public string[] Conditions { get; set; }
        public string Conclusion { get; set; }
        public double Degree { get; set; }

        public Rule(string[] conditions, string conclusion, double degree)
        {
            Conditions = conditions;
            Conclusion = conclusion;
            Degree = degree;
        }
        
        public override string ToString()
        {
            string toReturn = "SE ";
            for (int i = 0; i < Conditions.Length - 1; i++)
                toReturn += Conditions[i] + " E ";
            toReturn += Conditions[Conditions.Length - 1];
            toReturn += " ENTÃO " + Conclusion;
            toReturn += " = " + Degree;

            return toReturn;
        }

        /// <summary>
        /// Compara as condições das regras
        /// </summary>
        /// <param name="toCompare">Regra para ser comparada</param>
        /// <returns>
        /// Retorna menor que zero se condição da instância é menor que a da regra passada, 
        /// Zero se condição da instância é igual ao da regra passada,
        /// maior que zero se condição da instância é maior que a da regra passada
        /// </returns>
        public int CompareTo(Rule toCompare)
        {
            for (int i = 0; i < Conditions.Length; i++)
            {
                if (Conditions[i].CompareTo(toCompare.Conditions[i]) < 0)
                    return -1;
                else if (Conditions[i].CompareTo(toCompare.Conditions[i]) > 0)
                    return 1;
                else if (Conditions[i].CompareTo(toCompare.Conditions[i]) == 0)
                    continue;
            }

            return 0;
        }
    }

    /// <summary>
    /// Representação do Conjunto Triangular
    /// </summary>
    class Set
    {
        public string Label { get; set; }
        public double Begin { get; set; }
        public double Peak { get; set; }
        public double End { get; set; }

        public Set(string label, double begin, double peak, double end)
        {
            Label = label;
            Begin = begin;
            Peak = peak;
            End = end;
        }

        public double getPertinenceDegree( double value )
        {
            if (value <= Begin || value > End)
                return 0.0;
            else if (value > Begin && value <= Peak)
                return 1 - (Peak - value) / (Peak - Begin);
            else
                return (End - value) / (End - Peak);
        }
    }
}
