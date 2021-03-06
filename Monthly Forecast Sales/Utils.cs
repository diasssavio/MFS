﻿using System;
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
        /// <summary>
        /// Construtor com valores padrão
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public Pair(T first = default(T), U second = default(U))
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }

        public override string ToString()
        {
            return "{ " + First.ToString() + " ; " + Second.ToString() + " }";
        }
    };

    /// <summary>
    /// Representação de uma regra
    /// </summary>
    class Rule : IComparable<Rule>
    {
        public string[] Conditions { get; set; }
        public string Conclusion { get; set; }
        public double Degree { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="conclusion"></param>
        /// <param name="degree"></param>
        public Rule(string[] conditions, string conclusion, double degree)
        {
            Conditions = conditions;
            Conclusion = conclusion;
            Degree = degree;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="rule1"></param>
        /// <param name="rule2"></param>
        /// <returns></returns>
        public bool Equals(Rule rule1)
        {
            bool value = true;
            for (int i = 0; i < this.Conditions.Length; i++)
                if (this.Conditions[i] != rule1.Conditions[i])
                    return false;

            return value;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="label"></param>
        /// <param name="begin"></param>
        /// <param name="peak"></param>
        /// <param name="end"></param>
        public Set(string label, double begin, double peak, double end)
        {
            Label = label;
            Begin = begin;
            Peak = peak;
            End = end;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double getPertinenceDegree( double value )
        {
            if (value > Begin && value <= Peak)
                return 1 - (Peak - value) / (Peak - Begin);
            else if (value > Peak && value <= End)
                return (End - value) / (End - Peak);
            else return 0.0;
        }
    }
}
