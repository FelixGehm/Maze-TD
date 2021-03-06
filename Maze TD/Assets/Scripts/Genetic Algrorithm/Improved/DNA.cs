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
				int randomIndex = UnityEngine.Random.Range(1, nodes + 1);
				while (DoesGeneExist(Genes, randomIndex))
				{
					randomIndex = UnityEngine.Random.Range(1, nodes + 1);
					//Debug.Log("while");
				}
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
			//this.SortGenes();
			//partner.SortGenes();
			int[] child = new int[Genes.Length];
			int crossover = UnityEngine.Random.Range(1, Genes.Length + 1);
			for (int i = 0; i < Genes.Length; i++)
			{
				int newGene;
				if (i > crossover)
					newGene = Genes[i];
				else
					newGene = partner.Genes[i];

				while (DoesGeneExist(child, newGene))
				{
					newGene = UnityEngine.Random.Range(1, Genes.Length + 1);
					//Debug.Log("while");
				}

				child[i] = newGene;
			}
			DNA newDNA = new DNA(child);
			return newDNA;
		}

		public void Mutate(float m)
		{
			for (int i = 0; i < Genes.Length; i++)
			{
				if (UnityEngine.Random.Range(0f, 1f) < m)
				{
					int newGene = UnityEngine.Random.Range(1, Genes.Length + 1);
					while (DoesGeneExist(Genes, newGene))
					{
						newGene = UnityEngine.Random.Range(1, Genes.Length + 1);
						//Debug.Log("while");
					}
					Genes[i] = newGene;
				}
			}
		}

		public void SortGenes()
		{
			//genes.Sort((x, y) => x.Index.CompareTo(y.Index));
			Array.Sort(Genes, (x, y) => x.CompareTo(y));
		}

		private bool DoesGeneExist(int[] genes, int gene)
		{
			foreach (var g in genes)
			{
				if (g == gene)
					return true;
			}
			return false;
		}
	}
}

