﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GeneticAlgorithmAdvanced
{
	public class DNA
	{

		public float Fitness { get; set; }
		public int[] Genes { get; private set; }

		//For first Population
		public DNA(int towers, int nodes)
		{
			//init first population with random genes
			Genes = new int[towers];
			for (int i = 0; i < Genes.Length; i++)
			{
				int randomIndex = UnityEngine.Random.Range(0, nodes);
				Genes[i] = randomIndex;
			}
		}

		//For further Pupulations
		public DNA(int[] newGenes)
		{
			Genes = newGenes;
		}

		public DNA Crossover(DNA partner)
		{
			this.SortGenes();
			partner.SortGenes();
			int[] child = new int[Genes.Length];
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
					Genes[i] = UnityEngine.Random.Range(0, Genes.Length);
			}
		}

		public void SortGenes()
		{
			//genes.Sort((x, y) => x.Index.CompareTo(y.Index));
			Array.Sort(Genes, (x, y) => x.CompareTo(y));
		}
	}
}

