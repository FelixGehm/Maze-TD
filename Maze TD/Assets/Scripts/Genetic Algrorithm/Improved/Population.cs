using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneticAlgorithmAdvanced
{
	public class Population
	{
		private float _mutationRate;


		private float _fitnessSum;
		private System.Random _random;

		public List<DNA> Pop { get; private set; }
		public int Generation { get; private set; }
		public float BestFitness { get; private set; }
		public DNA BestGenes { get; private set; }
		public DNA SecondBestGenes { get; private set; }

		public Population(float mutationRate, int populationSize, int towers, int nodes)
		{
			_mutationRate = mutationRate;
			Pop = new List<DNA>();
			Generation = 1;
			BestFitness = 0;

			for (int i = 0; i < populationSize; i++)
			{
				Pop.Add(new DNA(towers, nodes));
			}

			_random = new System.Random();
		}

		public void NewGeneration()
		{
			if (Pop.Count <= 0)
				return;


			CalculateFitnessSum();
			List<DNA> newPopulation = new List<DNA>();

			newPopulation.Add(BestGenes);
			newPopulation.Add(SecondBestGenes);

			for (int i = 2; i < Pop.Count; i++)
			{
				DNA parent1 = ChooseParent();
				DNA parent2 = ChooseParent();
				DNA child = parent1.Crossover(parent2);

				child.Mutate(_mutationRate);

				newPopulation.Add(child);
			}

			Pop = newPopulation;
			Generation++;
		}

		private void CalculateFitnessSum()
		{
			_fitnessSum = 0;
			DNA best = Pop[0];
			DNA secondBest = Pop[1];

			foreach (DNA e in Pop)
			{
				_fitnessSum += e.Fitness;
				if (e.Fitness > best.Fitness)
				{
					best = e;
					if (e == secondBest)
						secondBest = Pop[0];
				}

				if (e.Fitness > secondBest.Fitness && e != best)
					secondBest = e;
			}

			BestFitness = best.Fitness;
			BestGenes = best;
			SecondBestGenes = secondBest;
		}

		private DNA ChooseParent()
		{
			double randomNumber = _random.NextDouble() * _fitnessSum;

			foreach (DNA e in Pop)
			{
				if (randomNumber < e.Fitness)
				{
					return e;
				}

				randomNumber -= e.Fitness;
			}

			return null;
		}
	}
}

