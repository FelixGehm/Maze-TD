﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{

	public float Fitness { get; set; }
	public Node[] Genes { get; private set; }
	private List<Node> _allNodes;

	//For first Population
	public DNA(int num, List<Node> allNodes)
	{
		//init first population with random genes
		_allNodes = allNodes;
		Genes = new Node[num];
		for (int i = 0; i < Genes.Length; i++)
		{
			int randomIndex = UnityEngine.Random.Range(0, _allNodes.Count);
			while(DoesGeneExist(Genes, _allNodes[randomIndex]))
			{
				randomIndex = UnityEngine.Random.Range(0, _allNodes.Count);
			}

			Genes[i] = _allNodes[randomIndex];
		}
	}

	//For further Pupulations
	public DNA(Node[] newGenes, List<Node> allNodes)
	{
		_allNodes = allNodes;
		Genes = newGenes;
	}

	public DNA Crossover(DNA partner)
	{
		this.SortGenes();
		partner.SortGenes();
		Node[] child = new Node[Genes.Length];
		int crossover = UnityEngine.Random.Range(0, Genes.Length);
		for (int i = 0; i < Genes.Length; i++)
		{
			Node newGene;
			if (i > crossover)
				newGene = Genes[i];
			else
				newGene = partner.Genes[i];

			while(DoesGeneExist(child, newGene))
			{
				newGene = _allNodes[UnityEngine.Random.Range(0, _allNodes.Count)];
			}
			child[i] = newGene;
		}
		DNA newDNA = new DNA(child, _allNodes);
		return newDNA;
	}

	public void Mutate(float m)
	{
		for (int i = 0; i < Genes.Length; i++)
		{
			if (UnityEngine.Random.Range(0f, 1f) < m)
				Genes[i] = _allNodes[UnityEngine.Random.Range(0, _allNodes.Count)];
		}
	}

	public void SortGenes()
	{
		//genes.Sort((x, y) => x.Index.CompareTo(y.Index));
		Array.Sort(Genes, (x, y) => x.Index.CompareTo(y.Index));
	}

	private bool DoesGeneExist(Node[] genes, Node gene)
	{
		foreach (var g in genes)
		{
			if (g == gene)
				return true;
		}
		return false;
	}
}
