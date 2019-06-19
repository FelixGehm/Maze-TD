using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.Shakespear
{
	public class DNA
	{
		public float Fitness { get; set; }
		public char[] Genes { get; private set; }

		public DNA(int num)
		{
			//init first population with random genes
			Genes = new char[num];
			for (int i = 0; i < Genes.Length; i++)
			{
				Genes[i] = (char)Random.Range(32, 128);
			}
		}

		public DNA(char[] newGenes)
		{
			Genes = newGenes;
		}

		public DNA Crossover(DNA partner)
		{
			char[] child = new char[Genes.Length];
			int crossover = UnityEngine.Random.Range(0, Genes.Length);
			for (int i = 0; i < Genes.Length; i++)
			{
				if (i > crossover)
					child[i] = Genes[i];
				else
					child[i] = partner.Genes[i];
			}
			DNA newDNA = new DNA(child);
			return newDNA;
		}

		public void Mutate(float m)
		{
			for (int i = 0; i < Genes.Length; i++)
			{
				if (UnityEngine.Random.Range(0f, 1f) < m)
					Genes[i] = (char)Random.Range(32, 128);
			}
		}

		public void CalcFitness(string target)
		{
			int score = 0;
			for (int i = 0; i < Genes.Length; i++)
			{
				if (Genes[i] == target[i])
					score++;
			}
			Fitness = (float)score / (float)target.Length;
		}

		public string GetPhrase()
		{
			return new string(Genes);
		}
	}
}
